using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Xml.Serialization;
using System.IO;
using System.Xml;

namespace Packing
{
    public partial class FrmMain : Form
    {
        const string CONN_INFO_FILE_NAME = "connect.xml";
        Dictionary<int, AxActUtlTypeLib.AxActUtlType> dicAxActUtlType = new Dictionary<int,AxActUtlTypeLib.AxActUtlType>(2);
        Dictionary<int, Dispatcher> dicDispatcher = new Dictionary<int, Dispatcher>(2);
        Dictionary<int, TextBox> dicInfoBox = new Dictionary<int, TextBox>(2);
        Dictionary<int, List<PackingType>> dicPacking;

        public FrmMain()
        {
            InitializeComponent();

            init();
        }

        /// <summary>
        /// 初始化
        /// </summary>
        private void init()
        {
            InitConnInfo();

            //初始化PLC连接
            dicAxActUtlType.Add(1, new AxActUtlTypeLib.AxActUtlType());
            dicAxActUtlType.Add(2, new AxActUtlTypeLib.AxActUtlType());

            foreach (var key in dicAxActUtlType.Keys)
            {
                this.Controls.Add(dicAxActUtlType[key]);
            }

            dicInfoBox.Add(1, txtEmp1Info);
            dicInfoBox.Add(2, txtEmp2Info);
        }

        /// <summary>
        /// 保存连接信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            //xml文件的方式持久化连接信息
            XmlSerializer fommatter = new XmlSerializer(typeof(List<string>));
            using (Stream fs = new FileStream(CONN_INFO_FILE_NAME, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                fommatter.Serialize(fs, new List<string>() { txtEmp1No.Text.Trim(), txtEmp1Pwd.Text.Trim(), txtEmp2No.Text.Trim(), txtEmp2Pwd.Text.Trim() });//序列化对象
            }

            MessageBox.Show("保存成功！");
        }

        /// <summary>
        /// 初始化连接信息
        /// </summary>
        private void InitConnInfo()
        {
            //从xml文件中加载连接信息
            XmlSerializer fommatter = new XmlSerializer(typeof(List<string>));
            using (XmlReader xr = XmlReader.Create(CONN_INFO_FILE_NAME))
            {
                List<string> list = (List<string>)fommatter.Deserialize(xr);//反序列化对象 
                txtEmp1No.Text = list[0];
                txtEmp1Pwd.Text = list[1];
                txtEmp2No.Text = list[2];
                txtEmp2Pwd.Text = list[3];
            }
        }

        /// <summary>
        /// 打开表格
        /// 加载任务
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "03excel|*.xls|07excel|*.xlsx";
            openFileDialog.RestoreDirectory = true;
            openFileDialog.FilterIndex = 1;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                String fName = openFileDialog.FileName;
                dicPacking = ExcelHelper.ToDataTable(fName);

                //调度器获得对应的任务列表
                foreach (var key in dicDispatcher.Keys)
                {
                    dicDispatcher[key].ListTask = dicPacking[key];
                }
            }
        }

        ///// <summary>
        ///// 开始工作
        ///// </summary>
        ///// <param name="dicPacking">包装信息</param>
        //private void StartWork(Dictionary<string, List<PackingType>> dicPacking)
        //{
        //    Plc plc = new Plc(new Linker(), txtEmp1Info);

        //    foreach (var packerNo in dicPacking.Keys)
        //    {
        //        Thread t = new Thread(new ParameterizedThreadStart(plc.execute));
        //        t.Start(dicPacking[packerNo]);
        //    }
        //}


        //private void connect()
        //{
        //    try
        //    {
        //        int iStation = Convert.ToInt32(this.txtStationNo.Text.Trim());
        //        this.axActUtlType.ActLogicalStationNumber = iStation;
        //        this.axActUtlType.ActPassword = this.txtPassword.Text.Trim();
        //        int rtn = this.axActUtlType.Open();
        //        if (rtn == 0)
        //        {
        //            ShowMsg("连接成功！");
        //            this.txtStationNo.Enabled = false;
        //            this.txtPassword.Enabled = false;
        //            this.btnConn.Enabled = false;
        //        }
        //        else
        //        {
        //            ShowMsg("连接失败");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ShowMsg(ex.Message);
        //    }
        //}

        //private void read()
        //{
        //    try
        //    {
        //        string strAddr = this.txtAddr.Text.Trim();
        //        int num = Convert.ToInt32(this.txtNum.Text.Trim());
        //        short[] arr = new short[num];

        //        int rtn = this.axActUtlType.ReadDeviceBlock2(strAddr, num, out arr[0]);
        //        if (rtn == 0)
        //        {
        //            ShowMsg("读取数据成功！");
        //            for (int i = 0; i < arr.Length; i++)
        //            {
        //                ShowMsg(string.Format("{0:X4}", arr[i]));
        //            }
        //        }
        //        else
        //        {
        //            ShowMsg("读取数据失败");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ShowMsg(ex.Message);
        //    }
        //}

        //private void write()
        //{
        //    try
        //    {
        //        string strAddr = this.txtAddr.Text.Trim();
        //        string[] strData = this.txtData.Lines;
        //        int num = strData.Length;
        //        short[] arr = new short[num];
        //        for (int i = 0; i < num; i++)
        //        {
        //            arr[i] = Convert.ToInt16(strData[i]);
        //        }

        //        int rtn = this.axActUtlType.WriteDeviceBlock2(strAddr, num, ref arr[0]);
        //        if (rtn == 0)
        //        {
        //            ShowMsg("写入数据成功！");
        //            this.txtNum.Text = num.ToString();
        //        }
        //        else
        //        {
        //            ShowMsg("写入数据失败");
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        ShowMsg(ex.Message);
        //    }
        //}

        //private void ShowMsg(string msg)
        //{
        //    string str = string.Format(DateTime.Now.ToString("HH:mm:ss") + "_" + msg);
        //    this.richTextBox1.SelectionStart = this.richTextBox1.Text.Length;
        //    this.richTextBox1.SelectedText += (str + Environment.NewLine);
        //    this.richTextBox1.ScrollToCaret();
        //}

        private void btnEmp1Conn_Click(object sender, EventArgs e)
        {
            int stationNo = Convert.ToInt32(txtEmp1No.Text.Trim());
            string pwd = txtEmp1Pwd.Text.Trim();
            Connect(1, stationNo, pwd);

        }

        private void btnEmp2Conn_Click(object sender, EventArgs e)
        {
            int stationNo = Convert.ToInt32(txtEmp2No.Text.Trim());
            string pwd = txtEmp2Pwd.Text.Trim();
            Connect(2, stationNo, pwd);
        }

        private void Connect(int key, int stationNo, string pwd)
        {
            if (!dicDispatcher.ContainsKey(key))
            {
                dicAxActUtlType[key].ActLogicalStationNumber = stationNo;
                dicAxActUtlType[key].ActPassword = pwd;
                dicDispatcher.Add(key, new Dispatcher(dicAxActUtlType[key]));
                dicDispatcher[key].ShowInfo = ShowInfo;
                dicDispatcher[key].Key = key;
                
            }
            dicDispatcher[key].Connect();
        }

        private void ShowInfo(string info, int key)
        {
            string str = string.Format(DateTime.Now.ToString("HH:mm:ss") + "_" + info);
            this.dicInfoBox[key].SelectionStart = this.dicInfoBox[key].Text.Length;
            this.dicInfoBox[key].SelectedText += (str + Environment.NewLine);
            this.dicInfoBox[key].ScrollToCaret();
        }

        private void btnEmp1Start_Click(object sender, EventArgs e)
        {
            Start(1);
        }

        private void btnEmp2Start_Click(object sender, EventArgs e)
        {
            Start(2);
        }

        private void Start(int key)
        {
            dicDispatcher[key].Start();
        }

        private void btnEmp1Pause_Click(object sender, EventArgs e)
        {
            Pause(1);
        }

        private void btnEmp2Pause_Click(object sender, EventArgs e)
        {
            Pause(2);
        }

        private void Pause(int key)
        {
            dicDispatcher[key].Pause();
        }

        private void btnEmp1Stop_Click(object sender, EventArgs e)
        {
            Stop(1);
        }

        private void btnEmp2Stop_Click(object sender, EventArgs e)
        {
            Stop(2);
        }

        private void Stop(int key)
        {
            dicDispatcher[key].Stop();
        }

        private void btnDebug_Click(object sender, EventArgs e)
        {
            new FrmDebug().ShowDialog();
        }

        private void btnQuit_Click(object sender, EventArgs e)
        {
            Close();
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

//http://blog.csdn.net/urovo2017/article/details/71122872