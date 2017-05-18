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
        public FrmDebug()
        {
            InitializeComponent();
        }


        private void btnPrint_Click(object sender, EventArgs e)
        {
            string bar = "1S243-024-030-36-1-068-A";
            string name = "图号：1S243-024-030-36-1-068-A\r\n合格证号：804002015A003143\r\n名称：垫圈1\r\n数量：5\r\n";
            Barcode.Print(bar, name);
        }
    }
}
