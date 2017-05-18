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
                }
            }
            catch (Exception ex)
            {
                ShowInfo(ex.Message, Key);
            }

            if (result == 1)
            {
                SendHeartbeat();


            }
            //连接成功后，建立timer，发心跳包

            //上位运行选择，on继续

            //读紧急报警	10位

            //读完成总数,是否需要继续执行

            //

            return result;
        }

        public int Start()
        {
            //循环任务
            //判断是否已加载任务
            if (Status == 0)
            {
                DoTask();
            }



            return 0;
        }

        private void DoTask()
        {
            //最小粒度命令
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
                axActUtlType.WriteDeviceBlock2("M300", 1, ref data);
                heartbeatFlag = !heartbeatFlag;
            }

        }
    }
}
