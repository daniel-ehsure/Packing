namespace Packing
{
    partial class FrmMain
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.txtInfo = new System.Windows.Forms.TextBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.btnPrint = new System.Windows.Forms.Button();
            this.btnOpen = new System.Windows.Forms.Button();
            this.gbConnInfo = new System.Windows.Forms.GroupBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.txtEmp1No = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtEmp1Pwd = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtEmp2No = new System.Windows.Forms.TextBox();
            this.txtEmp2Pwd = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.gbConnInfo.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtInfo
            // 
            this.txtInfo.Location = new System.Drawing.Point(41, 207);
            this.txtInfo.Multiline = true;
            this.txtInfo.Name = "txtInfo";
            this.txtInfo.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtInfo.Size = new System.Drawing.Size(602, 425);
            this.txtInfo.TabIndex = 5;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // btnPrint
            // 
            this.btnPrint.Location = new System.Drawing.Point(649, 249);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(75, 23);
            this.btnPrint.TabIndex = 4;
            this.btnPrint.Text = "print";
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btnOpen
            // 
            this.btnOpen.Location = new System.Drawing.Point(649, 207);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(75, 23);
            this.btnOpen.TabIndex = 3;
            this.btnOpen.Text = "open";
            this.btnOpen.UseVisualStyleBackColor = true;
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // gbConnInfo
            // 
            this.gbConnInfo.Controls.Add(this.label4);
            this.gbConnInfo.Controls.Add(this.label2);
            this.gbConnInfo.Controls.Add(this.label3);
            this.gbConnInfo.Controls.Add(this.label1);
            this.gbConnInfo.Controls.Add(this.txtEmp2Pwd);
            this.gbConnInfo.Controls.Add(this.txtEmp2No);
            this.gbConnInfo.Controls.Add(this.txtEmp1Pwd);
            this.gbConnInfo.Controls.Add(this.txtEmp1No);
            this.gbConnInfo.Controls.Add(this.btnSave);
            this.gbConnInfo.Location = new System.Drawing.Point(13, 13);
            this.gbConnInfo.Name = "gbConnInfo";
            this.gbConnInfo.Size = new System.Drawing.Size(759, 55);
            this.gbConnInfo.TabIndex = 6;
            this.gbConnInfo.TabStop = false;
            this.gbConnInfo.Text = "连接信息";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(678, 20);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "保存";
            this.btnSave.UseVisualStyleBackColor = true;
            // 
            // txtEmp1No
            // 
            this.txtEmp1No.Location = new System.Drawing.Point(71, 20);
            this.txtEmp1No.Name = "txtEmp1No";
            this.txtEmp1No.Size = new System.Drawing.Size(80, 21);
            this.txtEmp1No.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "设备1站号";
            // 
            // txtEmp1Pwd
            // 
            this.txtEmp1Pwd.Location = new System.Drawing.Point(240, 20);
            this.txtEmp1Pwd.Name = "txtEmp1Pwd";
            this.txtEmp1Pwd.Size = new System.Drawing.Size(80, 21);
            this.txtEmp1Pwd.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(175, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "设备1密码";
            // 
            // txtEmp2No
            // 
            this.txtEmp2No.Location = new System.Drawing.Point(418, 20);
            this.txtEmp2No.Name = "txtEmp2No";
            this.txtEmp2No.Size = new System.Drawing.Size(80, 21);
            this.txtEmp2No.TabIndex = 1;
            // 
            // txtEmp2Pwd
            // 
            this.txtEmp2Pwd.Location = new System.Drawing.Point(580, 20);
            this.txtEmp2Pwd.Name = "txtEmp2Pwd";
            this.txtEmp2Pwd.Size = new System.Drawing.Size(80, 21);
            this.txtEmp2Pwd.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(353, 25);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "设备2站号";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(515, 25);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 12);
            this.label4.TabIndex = 2;
            this.label4.Text = "设备2密码";
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.gbConnInfo);
            this.Controls.Add(this.txtInfo);
            this.Controls.Add(this.btnPrint);
            this.Controls.Add(this.btnOpen);
            this.Name = "FrmMain";
            this.Text = "Packing";
            this.gbConnInfo.ResumeLayout(false);
            this.gbConnInfo.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtInfo;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Button btnOpen;
        private System.Windows.Forms.GroupBox gbConnInfo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtEmp1Pwd;
        private System.Windows.Forms.TextBox txtEmp1No;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtEmp2Pwd;
        private System.Windows.Forms.TextBox txtEmp2No;
    }
}

