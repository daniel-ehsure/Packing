using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Packing
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            try
            {
                Application.Run(new FrmMain());
            }
            catch (Exception ex)
            {
                MessageBox.Show("程序异常，请重新启动！\r\n 详细信息：" + ex.Message);
            }
        }
    }
}
