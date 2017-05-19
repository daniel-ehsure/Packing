using System;
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
        public ShowInfo ShowInfo { get; set; }

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

        private List<PackingType> listTask;

        public List<PackingType> ListTask
        {
            get { return listTask; }
            set { listTask = value; OnTaskChanged(); }
        }

        private AxActUtlTypeLib.AxActUtlType axActUtlType = null;

        System.Timers.Timer timer;
        bool heartbeatFlag = true;

        public Dispatcher(AxActUtlTypeLib.AxActUtlType axActUtlType)
        {
            this.axActUtlType = axActUtlType;
        }

        /// <summary>
        /// 连接
        /// </summary>
        /// <returns></returns>
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
                        res = GetDoneCount();
                        if (res == 1)
                        {
                            //判断完成数是否为0
                            result = 1;
                        }
                        else
                        {
                            result = res;
                        }
                    }
                    else
                    {
                        result = res;
                    }
                }
                else
                {
                    result = res;
                }
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
                short data;
                int rtn = axActUtlType.ReadDeviceBlock2("M200", 1, out data);
                if (rtn == 1)
                {
                    //data == 1;
                    return 1;
                }
                else
                {
                    return 2;
                }
            }
            catch (Exception ex)
            {
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
                short data;
                int rtn = axActUtlType.ReadDeviceBlock2("M100", 10, out data);
                if (rtn == 1)
                {
                    //data == 1;
                    return 1;
                }
                else
                {
                    return 2;
                }
            }
            catch (Exception ex)
            {
                return 10;
            }
        }

        /// <summary>
        /// 获取完成数
        /// </summary>
        /// <returns></returns>
        private int GetWarning()
        {
            try
            {
                short data;
                int rtn = axActUtlType.ReadDeviceBlock2("R200", 10, out data);
                if (rtn == 1)
                {
                    //data == 1;
                    return 1;
                }
                else
                {
                    return 2;
                }
            }
            catch (Exception ex)
            {
                return 10;
            }
        }
    }
}
