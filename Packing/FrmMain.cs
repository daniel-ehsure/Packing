using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace Packing
{
    public partial class FrmMain : Form
    {
        private AxActUtlTypeLib.AxActUtlType axActUtlType = null;

        public FrmMain()
        {
            InitializeComponent();
            this.axActUtlType = new AxActUtlTypeLib.AxActUtlType();
            this.Controls.Add(this.axActUtlType);
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "03excel|*.xls|07excel|*.xlsx";
            openFileDialog.RestoreDirectory = true;
            openFileDialog.FilterIndex = 1;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                String fName = openFileDialog.FileName;
                Dictionary<string, List<PackingType>> dicPacking = ExcelHelper.ToDataTable(fName);

                StartWork(dicPacking);
            }
        }

        /// <summary>
        /// 开始工作
        /// </summary>
        /// <param name="dicPacking">包装信息</param>
        private void StartWork(Dictionary<string, List<PackingType>> dicPacking)
        {
            Plc plc = new Plc(new Linker(), txtInfo);

            foreach (var packerNo in dicPacking.Keys)
            {
                Thread t = new Thread(new ParameterizedThreadStart(plc.execute));
                t.Start(dicPacking[packerNo]);
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            string bar = "1S243-024-030-36-1-068-A";
            string name = "图号：1S243-024-030-36-1-068-A\r\n合格证号：804002015A003143\r\n名称：垫圈1\r\n数量：5\r\n";
            Barcode.Print(bar, name);
        }

        private void connect()
        {
            try
            {
                int iStation = Convert.ToInt32(this.txtStationNo.Text.Trim());
                this.axActUtlType.ActLogicalStationNumber = iStation;
                this.axActUtlType.ActPassword = this.txtPassword.Text.Trim();
                int rtn = this.axActUtlType.Open();
                if (rtn == 0)
                {
                    ShowMsg("连接成功！");
                    this.txtStationNo.Enabled = false;
                    this.txtPassword.Enabled = false;
                    this.btnConn.Enabled = false;
                }
                else
                {
                    ShowMsg("连接失败");
                }
            }
            catch (Exception ex)
            {
                ShowMsg(ex.Message);
            }
        }

        private void read()
        {
            try
            {
                string strAddr = this.txtAddr.Text.Trim();
                int num = Convert.ToInt32(this.txtNum.Text.Trim());
                short[] arr = new short[num];

                int rtn = this.axActUtlType.ReadDeviceBlock2(strAddr, num, out arr[0]);
                if (rtn == 0)
                {
                    ShowMsg("读取数据成功！");
                    for (int i = 0; i < arr.Length; i++)
                    {
                        ShowMsg(string.Format("{0:X4}", arr[i]));
                    }
                }
                else
                {
                    ShowMsg("读取数据失败");
                }
            }
            catch (Exception ex)
            {
                ShowMsg(ex.Message);
            }
        }

        private void write()
        {
            try
            {
                string strAddr = this.txtAddr.Text.Trim();
                string[] strData = this.txtData.Lines;
                int num = strData.Length;
                short[] arr = new short[num];
                for (int i = 0; i < num; i++)
                {
                    arr[i] = Convert.ToInt16(strData[i]);
                }

                int rtn = this.axActUtlType.WriteDeviceBlock2(strAddr, num, ref arr[0]);
                if (rtn == 0)
                {
                    ShowMsg("写入数据成功！");
                    this.txtNum.Text = num.ToString();
                }
                else
                {
                    ShowMsg("写入数据失败");
                }

            }
            catch (Exception ex)
            {
                ShowMsg(ex.Message);
            }
        }

        private void ShowMsg(string msg)
        {
            string str = string.Format(DateTime.Now.ToString("HH:mm:ss") + "_" + msg);
            this.richTextBox1.SelectionStart = this.richTextBox1.Text.Length;
            this.richTextBox1.SelectedText += (str + Environment.NewLine);
            this.richTextBox1.ScrollToCaret();
        }
    }
}

//1、构建一个总调度类，负责plc操作
//2、设置两个plc连接的参数，站号和密码
//3、初始化调度类
//4、调度类连接plc
//5、读取plc状态，是否有未完成操作
//5、有，从本地化命令中继续执行；没有，加载excel文件
//6、起线程执行
//6、每一个命令为最小粒度，等plc反馈后，再执行下一个（先判断是否暂停、写plc、等反馈、写本地化状态、打印）
//7、暂停（是否需要考虑两个设备不同时暂停）
//8、继续，重新起线程执行
//8、执行结束