using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Packing
{
    public partial class FrmDebug : Form
    {
        private AxActUtlTypeLib.AxActUtlType axActUtlType = null;

        public FrmDebug()
        {
            InitializeComponent();

            axActUtlType = new AxActUtlTypeLib.AxActUtlType();
            Controls.Add(this.axActUtlType);
        }

        /// <summary>
        /// 打印
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPrint_Click(object sender, EventArgs e)
        {
            //string bar = "1S243-024-030-36-1-068-A+804002015A003143";
            //string name = "图号：1S243-024-030-36-1-068-A\r\n合格证号：804002015A003143\r\n名称：垫圈1\r\n数量：5\r\n";
            string bar = txtBar.Text.Trim();
            string name = txtName.Text.Trim();

            Barcode.Print(bar, name);
        }

        private void btnConn_Click(object sender, EventArgs e)
        {
            try
            {
                int iStation = Convert.ToInt32(txtEmpNo.Text.Trim());
                this.axActUtlType.ActLogicalStationNumber = iStation;
                this.axActUtlType.ActPassword = txtEmpPwd.Text.Trim();
                int rtn = this.axActUtlType.Open();
                if (rtn == 0)
                {
                    ShowMsg("连接成功！");
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

        private void btnRead_Click(object sender, EventArgs e)
        {
            try
            {
                string strAddr = txtAddress.Text.Trim();
                int num = Convert.ToInt32(txtNum.Text.Trim());
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

        private void btnWrite_Click(object sender, EventArgs e)
        {
            try
            {
                string strAddr = txtAddress.Text.Trim();
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
            txtResult.SelectionStart = txtResult.Text.Length;
            txtResult.SelectedText += (str + Environment.NewLine);
            txtResult.ScrollToCaret();
        }

        private void btnQuit_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
