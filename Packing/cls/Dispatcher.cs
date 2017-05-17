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

        private AxActUtlTypeLib.AxActUtlType axActUtlType = null;
        
        public Dispatcher(AxActUtlTypeLib.AxActUtlType axActUtlType)
        {
            this.axActUtlType = axActUtlType;
        }

        public void Connect()
        {
            ShowInfo("开始连接...", Key);
        }
    }
}
