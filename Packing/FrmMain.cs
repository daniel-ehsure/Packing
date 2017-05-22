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
        const string TASK_LIST_FILE_NAME1 = "TaskList1.xml";
        const string TASK_LIST_FILE_NAME2 = "TaskList2.xml";
        Dictionary<int, AxActUtlTypeLib.AxActUtlType> dicAxActUtlType = new Dictionary<int, AxActUtlTypeLib.AxActUtlType>(2);
        Dictionary<int, Dispatcher> dicDispatcher = new Dictionary<int, Dispatcher>(2);
        Dictionary<int, TextBox> dicInfoBox = new Dictionary<int, TextBox>(2);
        Dictionary<int, Button> dicBtnConn = new Dictionary<int, Button>(2);
        Dictionary<int, Button> dicBtnStart = new Dictionary<int, Button>(2);
        Dictionary<int, Button> dicBtnPause = new Dictionary<int, Button>(2);
        Dictionary<int, Button> dicBtnStop = new Dictionary<int, Button>(2);
        Dictionary<int, List<PackingType>> dicPacking;
        Dictionary<int, string> dicTask = new Dictionary<int, string>(2);

        public FrmMain()
        {
            InitializeComponent();

            Init();
        }

        /// <summary>
        /// 初始化
        /// </summary>
        private void Init()
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

            dicTask.Add(1, TASK_LIST_FILE_NAME1);
            dicTask.Add(21, TASK_LIST_FILE_NAME2);

            InitBtn();
        }

        /// <summary>
        /// 初始化按钮
        /// </summary>
        private void InitBtn()
        {
            dicBtnConn.Add(1, btnEmp1Conn);
            dicBtnConn.Add(2, btnEmp2Conn);

            dicBtnStart.Add(1, btnEmp1Start);
            dicBtnStart.Add(2, btnEmp2Start);

            dicBtnPause.Add(1, btnEmp1Pause);
            dicBtnPause.Add(2, btnEmp2Pause);

            dicBtnStop.Add(1, btnEmp1Stop);
            dicBtnStop.Add(2, btnEmp2Stop);

            foreach (var key in dicInfoBox.Keys)
            {
                dicInfoBox[key].Enabled = true;
                dicBtnStart[key].Enabled = false;
                dicBtnPause[key].Enabled = false;
                dicBtnStop[key].Enabled = false;
            }

            btnOpen.Enabled = false;
        }

        /// <summary>
        /// 保存连接信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            //xml文件的方式持久化连接信息
            Utility.WriteFile<List<string>>(CONN_INFO_FILE_NAME, new List<string>() { txtEmp1No.Text.Trim(), txtEmp1Pwd.Text.Trim(), txtEmp2No.Text.Trim(), txtEmp2Pwd.Text.Trim() });//序列化对象

            MessageBox.Show("保存成功！");
        }

        /// <summary>
        /// 初始化连接信息
        /// </summary>
        private void InitConnInfo()
        {
            //从xml文件中加载连接信息
            List<string> list = Utility.RaadFile<List<string>>(CONN_INFO_FILE_NAME);

            txtEmp1No.Text = list[0];
            txtEmp1Pwd.Text = list[1];
            txtEmp2No.Text = list[2];
            txtEmp2Pwd.Text = list[3];
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



                foreach (var key in dicDispatcher.Keys)
                {
                    //调度器获得对应的任务列表
                    dicDispatcher[key].ListTask = dicPacking[key];
                    //存储到文件
                    Utility.WriteFile<List<PackingType>>(dicTask[key], dicPacking[key]);
                    //设置按钮
                    dicBtnStart[key].Enabled = true;
                    btnOpen.Enabled = false;
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

        /// <summary>
        /// 连接
        /// </summary>
        /// <param name="key"></param>
        /// <param name="stationNo"></param>
        /// <param name="pwd"></param>
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

            int res = dicDispatcher[key].Connect();

            switch (res)
            {
                case 1://连接成功
                    dicBtnConn[key].Enabled = false;
                    btnOpen.Enabled = true;
                    break;
                case 2://连接失败
                    break;
                case 3://非上位运行
                    break;
                case 4://报警
                    break;
                case 5://完成总数不为0
                    //判断是否有未完成任务列表，有，是否继续执行，否，清零
                    List<PackingType> taskList = Utility.RaadFile<List<PackingType>>(dicTask[key]);
                    if (taskList.Count > 0)
                    {
                        if (MessageBox.Show("有未完成任务，是否继续?", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            Start(key);
                            return;
                        }
                    }

                    //不继续执行，完成数清零
                    if (dicDispatcher[key].SetDoneCount(0) == 0)
                    {
                        dicBtnConn[key].Enabled = false;
                        btnOpen.Enabled = true;
                    }

                    break;
                case 6://读取完成数失败
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 在消息框里显示
        /// </summary>
        /// <param name="info"></param>
        /// <param name="key"></param>
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