﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Timers;

namespace Packing
{
    public delegate void ShowInfo(string info, int key);

    /// <summary>
    /// 调度器类
    /// 负责与设备交互
    /// </summary>
    public class Dispatcher
    {
        /// <summary>
        /// 在消息框里显示
        /// </summary>
        public ShowInfo ShowInfo { get; set; }

        /// <summary>
        /// 设备标识（设备1、设备2）
        /// </summary>
        public int Key { get; set; }

        /// <summary>
        /// 状态
        /// 0：未连接
        /// 1：连接成功
        /// 10：停止
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 当前完成数
        /// </summary>
        public int DoneCount { get; set; }

        /// <summary>
        /// 任务列表
        /// </summary>
        private List<PackingType> listTask;

        /// <summary>
        /// 任务列表
        /// </summary>
        public List<PackingType> ListTask
        {
            get { return listTask; }
            set { listTask = value; OnTaskChanged(); }
        }

        /// <summary>
        /// PLC连接
        /// </summary>
        private AxActUtlTypeLib.AxActUtlType axActUtlType = null;

        /// <summary>
        /// 发送心跳包的timer
        /// </summary>
        System.Timers.Timer timer;

        /// <summary>
        /// 心跳包标志
        /// </summary>
        bool heartbeatFlag = true;

        public Dispatcher(AxActUtlTypeLib.AxActUtlType axActUtlType)
        {
            this.axActUtlType = axActUtlType;
        }

        /// <summary>
        /// 连接
        /// </summary>
        /// <returns>1、成功；2、失败；3、非上位运行；4、存在警报；5、完成总数不为0；6、读取完成数失败；10、异常</returns>
        public int Connect()
        {
            ShowInfo("开始连接...", Key);
            int result = 0;

            try
            {
                int rtn = axActUtlType.Open();
                if (rtn == 0)
                {
                    ShowInfo("连接成功！", Key);
                    result = 1;
                }
                else
                {
                    ShowInfo("连接失败!", Key);
                    result = 2;
                    return result;
                }
            }
            catch (Exception ex)
            {
                ShowInfo(ex.Message, Key);
                result = 10;
                return result;
            }

            if (result == 1)
            {
                //连接成功后，建立timer，发心跳包
                SendHeartbeat();

                //上位运行选择
                int res = GetHostFlag();

                if (res == 1)
                {
                    //读报警
                    res = GetWarning();

                    if (res == 1)
                    {
                        //读完成总数
                        result = GetDoneCount();
                    }
                }

                result = res;
            }

            return result;
        }

        public int Start()
        {
            //循环任务
            //起线程去搞
            for (int i = 0; i < ListTask.Count; i++)
            {
                for (int j = 0; j < ListTask[i].Item.Count; j++)
                {
                    if (Status == 0)
                    {
                        DoTask();
                    }
                }
            }
            
            ShowInfo("执行成功!", Key);

            //任务清零

            return 0;
        }

        private void DoTask()
        {
            //最小粒度命令
            /* 1、写命令
             * 2、开始执行
             * 3、等待n秒
             * 4、读4完成数，+1则成功，否则失败*/
        }

        public int Pause()
        {
            Status = 1;
            return 0;
        }

        public int Stop()
        {
            Status = 2;
            //清理任务，准备下次执行

            return 0;
        }

        /// <summary>
        /// 任务列表变化时，显示待执行任务
        /// </summary>
        private void OnTaskChanged()
        {
            for (int i = 0; i < ListTask.Count; i++)
            {
                for (int j = ListTask[i].DoneNumber; j < ListTask[i].Item.Count; j++)
                {
                    ShowInfo("待执行任务：" + listTask[i].Name + " 数量：" + ListTask[i].Item[j], Key);
                }
            }
        }

        /// <summary>
        /// 发送心跳包
        /// </summary>
        private void SendHeartbeat()
        {
            timer = new System.Timers.Timer(500);
            timer.Elapsed += new ElapsedEventHandler(timer_Elapsed);
            timer.Start();
        }

        /// <summary>
        /// 执行心跳包timer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (Status == 10)
            {
                timer.Stop();
            }
            else
            {
                short data = heartbeatFlag ? (short)1 : (short)0;

                try
                {
                    axActUtlType.WriteDeviceBlock2("M300", 1, ref data);
                }
                catch (Exception)
                {
                }
                finally
                {
                    heartbeatFlag = !heartbeatFlag;
                }
            }
        }

        /// <summary>
        /// 获取上位运行标志
        /// </summary>
        /// <returns></returns>
        private int GetHostFlag()
        {
            try
            {
                short[] data;
                if (Read("M200", 1, out data))
                {
                    if (data[0] == 1)
                    {
                        return 1;
                    }
                    else
                    {
                        ShowInfo("非上位运行!", Key);
                    }
                }
                else
                {
                    ShowInfo("读取上位运行失败!", Key);
                }
                
                return 3;
            }
            catch (Exception ex)
            {
                ShowInfo(ex.Message, Key);
                return 10;
            }
        }

        /// <summary>
        /// 获取报警
        /// </summary>
        /// <returns></returns>
        private int GetWarning()
        {
            try
            {
                short[] data;
                if (Read("M100", 11, out data))
                {
                    for (int i = 0; i < data.Length; i++)
                    {
                        if (data[i] != 0)
                        {
                            ShowInfo(Utility.DIC_WARNING[i], Key);
                            return 4;
                        }
                    }
                    return 1;
                }
                else
                {
                    ShowInfo("读取报警失败!", Key);
                    return 4;
                }
            }
            catch (Exception ex)
            {
                ShowInfo(ex.Message, Key);
                return 10;
            }
        }

        /// <summary>
        /// 获取完成数
        /// </summary>
        /// <returns></returns>
        private int GetDoneCount()
        {
            try
            {
                short[] data;
                if (Read("R200", 2, out data))
                {
                    DoneCount = data[1];//取低位数据
                    if (DoneCount == 0)
                    {
                        return 1;
                    }
                    else
                    {
                        return 5;
                    }
                }
                else
                {
                    ShowInfo("读取完成数失败!", Key);
                    return 6;
                }
            }
            catch (Exception ex)
            {
                ShowInfo(ex.Message, Key);
                return 10;
            }
        }

        /// <summary>
        /// 读数据
        /// </summary>
        /// <param name="address">地址</param>
        /// <param name="size">长度</param>
        /// <param name="data">数据</param>
        /// <returns></returns>
        public bool Read(string address, int size, out short[] data)
        {
            data = new short[size];
            return axActUtlType.ReadDeviceBlock2(address, size, out data[0]) == 0;
        }

        /// <summary>
        /// 写数据
        /// </summary>
        /// <param name="address">地址</param>
        /// <param name="size">长度</param>
        /// <param name="data">数据</param>
        /// <returns></returns>
        public bool Write(string address, int size, out short[] data)
        {
            data = new short[size];
            return axActUtlType.WriteDeviceBlock2(address, size, ref data[0]) == 0;
        }
    }
}
