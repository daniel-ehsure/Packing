using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

        public int Status { get; set; }

        public List<PackingType> ListTask { get; set; }

        private AxActUtlTypeLib.AxActUtlType axActUtlType = null;
        
        public Dispatcher(AxActUtlTypeLib.AxActUtlType axActUtlType)
        {
            this.axActUtlType = axActUtlType;
        }

        public int Connect()
        {
            ShowInfo("开始连接...", Key);

            //连接成功后，建立timer，发心跳包

            //上位运行选择，on继续

            //读紧急报警	10位

            //读完成总数,是否需要继续执行

            //

            return 0;
        }

        public int Start()
        {
            //判断是否已加载任务
            if (Status == 0)
            {
                doTask();
            }
            


            return 0;
        }

        private void doTask()
        {

        }
    }
}
