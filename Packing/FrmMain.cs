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
        //连接信息存储位置
        const string CONN_INFO_FILE_NAME = "connect.xml";
        //任务信息存储位置
        const string TASK_LIST_FILE_NAME1 = "TaskList1.xml";
        const string TASK_LIST_FILE_NAME2 = "TaskList2.xml";
        //分别存储两个plc相关的各种字典
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
            if (dicDispatcher.Count == 0)
            {
                MessageBox.Show("请先连接设备！");
                return;
            }
            else
            {
                foreach (var key in dicDispatcher.Keys)
                {
                    if (dicDispatcher[key].Status != 1)
                    {
                        MessageBox.Show("设备连接成功才能加载任务！");
                        return;
                    }
                }
            }

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
                }
            }
        }

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
                dicDispatcher[key].Status = 0;
            }

            //清空任务列表
            dicDispatcher[key].ListTask = null;

            if (dicDispatcher[key].Status == 3)
            {
                //重新连接
                dicDispatcher[key].Close();
                dicDispatcher[key].Status = 0;
            }
            else if (dicDispatcher[key].Status != 0)
            {
                MessageBox.Show("当前状态不能重新连接");
            }

            int res = dicDispatcher[key].Connect();

            switch (res)
            {
                case 1://连接成功
                    dicDispatcher[key].Status = 1;
                    break;
                case 2://连接失败
                    break;
                case 3://非上位运行
                    dicDispatcher[key].Status = 2;
                    break;
                case 4://报警
                    dicDispatcher[key].Status = 2;
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
                    dicDispatcher[key].Status = 2;
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

        /// <summary>
        /// 开始
        /// </summary>
        /// <param name="key"></param>
        private void Start(int key)
        {
            if (dicDispatcher[key].Status==1||dicDispatcher[key].Status==5)
            {
                dicDispatcher[key].Start();
            }
            else
            {
                MessageBox.Show("连接成功或暂停时才能开始！");
            }
        }

        private void btnEmp1Pause_Click(object sender, EventArgs e)
        {
            Pause(1);
        }

        private void btnEmp2Pause_Click(object sender, EventArgs e)
        {
            Pause(2);
        }

        /// <summary>
        /// 暂停
        /// </summary>
        /// <param name="key"></param>
        private void Pause(int key)
        {
            if (dicDispatcher[key].Status == 3)
            {
                dicDispatcher[key].Pause();
            }
            else
            {
                MessageBox.Show("执行时才能暂停！");
            }
        }

        private void btnEmp1Stop_Click(object sender, EventArgs e)
        {
            Stop(1);
        }

        private void btnEmp2Stop_Click(object sender, EventArgs e)
        {
            Stop(2);
        }

        /// <summary>
        /// 断开连接
        /// </summary>
        /// <param name="key"></param>
        private void Stop(int key)
        {
            if (dicDispatcher[key].Status == 0 || dicDispatcher[key].Status == 3 || dicDispatcher[key].Status == 4)
            {
                MessageBox.Show("连接成功或暂停时才能开始！");
            }
            else
            {
                dicDispatcher[key].Close(); 
            }
        }

        private void btnDebug_Click(object sender, EventArgs e)
        {
            new FrmDebug().ShowDialog();
        }

        private void btnQuit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnEmp1Claer_Click(object sender, EventArgs e)
        {
            txtEmp1Info.Clear();
        }

        private void btnEmp2Claer_Click(object sender, EventArgs e)
        {
            txtEmp2Info.Clear();
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