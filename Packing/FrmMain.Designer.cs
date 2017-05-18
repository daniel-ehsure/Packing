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
            this.txtEmp1Info = new System.Windows.Forms.TextBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.btnOpen = new System.Windows.Forms.Button();
            this.gbConnInfo = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtEmp2Pwd = new System.Windows.Forms.TextBox();
            this.txtEmp2No = new System.Windows.Forms.TextBox();
            this.txtEmp1Pwd = new System.Windows.Forms.TextBox();
            this.txtEmp1No = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnEmp1Conn = new System.Windows.Forms.Button();
            this.btnEmp1Start = new System.Windows.Forms.Button();
            this.btnEmp1Pause = new System.Windows.Forms.Button();
            this.btnEmp1Stop = new System.Windows.Forms.Button();
            this.btnEmp2Stop = new System.Windows.Forms.Button();
            this.btnEmp2Pause = new System.Windows.Forms.Button();
            this.btnEmp2Start = new System.Windows.Forms.Button();
            this.btnEmp2Conn = new System.Windows.Forms.Button();
            this.txtEmp2Info = new System.Windows.Forms.TextBox();
            this.btnDebug = new System.Windows.Forms.Button();
            this.btnQuit = new System.Windows.Forms.Button();
            this.gbEmp1 = new System.Windows.Forms.GroupBox();
            this.gbEmp2 = new System.Windows.Forms.GroupBox();
            this.gbConnInfo.SuspendLayout();
            this.gbEmp1.SuspendLayout();
            this.gbEmp2.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtEmp1Info
            // 
            this.txtEmp1Info.Location = new System.Drawing.Point(6, 45);
            this.txtEmp1Info.Multiline = true;
            this.txtEmp1Info.Name = "txtEmp1Info";
            this.txtEmp1Info.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtEmp1Info.Size = new System.Drawing.Size(318, 394);
            this.txtEmp1Info.TabIndex = 5;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // btnOpen
            // 
            this.btnOpen.Location = new System.Drawing.Point(697, 76);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(75, 23);
            this.btnOpen.TabIndex = 3;
            this.btnOpen.Text = "打开表格";
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
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(515, 25);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 12);
            this.label4.TabIndex = 2;
            this.label4.Text = "设备2密码";
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
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(353, 25);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "设备2站号";
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
            // txtEmp2Pwd
            // 
            this.txtEmp2Pwd.Location = new System.Drawing.Point(580, 20);
            this.txtEmp2Pwd.Name = "txtEmp2Pwd";
            this.txtEmp2Pwd.Size = new System.Drawing.Size(80, 21);
            this.txtEmp2Pwd.TabIndex = 1;
            // 
            // txtEmp2No
            // 
            this.txtEmp2No.Location = new System.Drawing.Point(418, 20);
            this.txtEmp2No.Name = "txtEmp2No";
            this.txtEmp2No.Size = new System.Drawing.Size(80, 21);
            this.txtEmp2No.TabIndex = 1;
            // 
            // txtEmp1Pwd
            // 
            this.txtEmp1Pwd.Location = new System.Drawing.Point(240, 20);
            this.txtEmp1Pwd.Name = "txtEmp1Pwd";
            this.txtEmp1Pwd.Size = new System.Drawing.Size(80, 21);
            this.txtEmp1Pwd.TabIndex = 1;
            // 
            // txtEmp1No
            // 
            this.txtEmp1No.Location = new System.Drawing.Point(71, 20);
            this.txtEmp1No.Name = "txtEmp1No";
            this.txtEmp1No.Size = new System.Drawing.Size(80, 21);
            this.txtEmp1No.TabIndex = 1;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(678, 20);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "保存";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnEmp1Conn
            // 
            this.btnEmp1Conn.Location = new System.Drawing.Point(6, 16);
            this.btnEmp1Conn.Name = "btnEmp1Conn";
            this.btnEmp1Conn.Size = new System.Drawing.Size(75, 23);
            this.btnEmp1Conn.TabIndex = 7;
            this.btnEmp1Conn.Text = "连接";
            this.btnEmp1Conn.UseVisualStyleBackColor = true;
            this.btnEmp1Conn.Click += new System.EventHandler(this.btnEmp1Conn_Click);
            // 
            // btnEmp1Start
            // 
            this.btnEmp1Start.Location = new System.Drawing.Point(87, 16);
            this.btnEmp1Start.Name = "btnEmp1Start";
            this.btnEmp1Start.Size = new System.Drawing.Size(75, 23);
            this.btnEmp1Start.TabIndex = 7;
            this.btnEmp1Start.Text = "开始";
            this.btnEmp1Start.UseVisualStyleBackColor = true;
            this.btnEmp1Start.Click += new System.EventHandler(this.btnEmp1Start_Click);
            // 
            // btnEmp1Pause
            // 
            this.btnEmp1Pause.Location = new System.Drawing.Point(168, 16);
            this.btnEmp1Pause.Name = "btnEmp1Pause";
            this.btnEmp1Pause.Size = new System.Drawing.Size(75, 23);
            this.btnEmp1Pause.TabIndex = 7;
            this.btnEmp1Pause.Text = "暂停";
            this.btnEmp1Pause.UseVisualStyleBackColor = true;
            this.btnEmp1Pause.Click += new System.EventHandler(this.btnEmp1Pause_Click);
            // 
            // btnEmp1Stop
            // 
            this.btnEmp1Stop.Location = new System.Drawing.Point(249, 16);
            this.btnEmp1Stop.Name = "btnEmp1Stop";
            this.btnEmp1Stop.Size = new System.Drawing.Size(75, 23);
            this.btnEmp1Stop.TabIndex = 7;
            this.btnEmp1Stop.Text = "停止";
            this.btnEmp1Stop.UseVisualStyleBackColor = true;
            this.btnEmp1Stop.Click += new System.EventHandler(this.btnEmp1Stop_Click);
            // 
            // btnEmp2Stop
            // 
            this.btnEmp2Stop.Location = new System.Drawing.Point(249, 16);
            this.btnEmp2Stop.Name = "btnEmp2Stop";
            this.btnEmp2Stop.Size = new System.Drawing.Size(75, 23);
            this.btnEmp2Stop.TabIndex = 10;
            this.btnEmp2Stop.Text = "停止";
            this.btnEmp2Stop.UseVisualStyleBackColor = true;
            this.btnEmp2Stop.Click += new System.EventHandler(this.btnEmp2Stop_Click);
            // 
            // btnEmp2Pause
            // 
            this.btnEmp2Pause.Location = new System.Drawing.Point(168, 16);
            this.btnEmp2Pause.Name = "btnEmp2Pause";
            this.btnEmp2Pause.Size = new System.Drawing.Size(75, 23);
            this.btnEmp2Pause.TabIndex = 11;
            this.btnEmp2Pause.Text = "暂停";
            this.btnEmp2Pause.UseVisualStyleBackColor = true;
            this.btnEmp2Pause.Click += new System.EventHandler(this.btnEmp2Pause_Click);
            // 
            // btnEmp2Start
            // 
            this.btnEmp2Start.Location = new System.Drawing.Point(87, 16);
            this.btnEmp2Start.Name = "btnEmp2Start";
            this.btnEmp2Start.Size = new System.Drawing.Size(75, 23);
            this.btnEmp2Start.TabIndex = 12;
            this.btnEmp2Start.Text = "开始";
            this.btnEmp2Start.UseVisualStyleBackColor = true;
            this.btnEmp2Start.Click += new System.EventHandler(this.btnEmp2Start_Click);
            // 
            // btnEmp2Conn
            // 
            this.btnEmp2Conn.Location = new System.Drawing.Point(6, 16);
            this.btnEmp2Conn.Name = "btnEmp2Conn";
            this.btnEmp2Conn.Size = new System.Drawing.Size(75, 23);
            this.btnEmp2Conn.TabIndex = 9;
            this.btnEmp2Conn.Text = "连接";
            this.btnEmp2Conn.UseVisualStyleBackColor = true;
            this.btnEmp2Conn.Click += new System.EventHandler(this.btnEmp2Conn_Click);
            // 
            // txtEmp2Info
            // 
            this.txtEmp2Info.Location = new System.Drawing.Point(6, 45);
            this.txtEmp2Info.Multiline = true;
            this.txtEmp2Info.Name = "txtEmp2Info";
            this.txtEmp2Info.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtEmp2Info.Size = new System.Drawing.Size(318, 394);
            this.txtEmp2Info.TabIndex = 8;
            // 
            // btnDebug
            // 
            this.btnDebug.Location = new System.Drawing.Point(598, 526);
            this.btnDebug.Name = "btnDebug";
            this.btnDebug.Size = new System.Drawing.Size(75, 23);
            this.btnDebug.TabIndex = 13;
            this.btnDebug.Text = "调试";
            this.btnDebug.UseVisualStyleBackColor = true;
            this.btnDebug.Click += new System.EventHandler(this.btnDebug_Click);
            // 
            // btnQuit
            // 
            this.btnQuit.Location = new System.Drawing.Point(697, 526);
            this.btnQuit.Name = "btnQuit";
            this.btnQuit.Size = new System.Drawing.Size(75, 23);
            this.btnQuit.TabIndex = 14;
            this.btnQuit.Text = "退出";
            this.btnQuit.UseVisualStyleBackColor = true;
            this.btnQuit.Click += new System.EventHandler(this.btnQuit_Click);
            // 
            // gbEmp1
            // 
            this.gbEmp1.Controls.Add(this.txtEmp1Info);
            this.gbEmp1.Controls.Add(this.btnEmp1Conn);
            this.gbEmp1.Controls.Add(this.btnEmp1Start);
            this.gbEmp1.Controls.Add(this.btnEmp1Pause);
            this.gbEmp1.Controls.Add(this.btnEmp1Stop);
            this.gbEmp1.Location = new System.Drawing.Point(12, 76);
            this.gbEmp1.Name = "gbEmp1";
            this.gbEmp1.Size = new System.Drawing.Size(330, 445);
            this.gbEmp1.TabIndex = 15;
            this.gbEmp1.TabStop = false;
            this.gbEmp1.Text = "设备1";
            // 
            // gbEmp2
            // 
            this.gbEmp2.Controls.Add(this.txtEmp2Info);
            this.gbEmp2.Controls.Add(this.btnEmp2Conn);
            this.gbEmp2.Controls.Add(this.btnEmp2Start);
            this.gbEmp2.Controls.Add(this.btnEmp2Stop);
            this.gbEmp2.Controls.Add(this.btnEmp2Pause);
            this.gbEmp2.Location = new System.Drawing.Point(361, 76);
            this.gbEmp2.Name = "gbEmp2";
            this.gbEmp2.Size = new System.Drawing.Size(330, 445);
            this.gbEmp2.TabIndex = 16;
            this.gbEmp2.TabStop = false;
            this.gbEmp2.Text = "设备2";
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.gbEmp2);
            this.Controls.Add(this.gbEmp1);
            this.Controls.Add(this.btnQuit);
            this.Controls.Add(this.btnDebug);
            this.Controls.Add(this.gbConnInfo);
            this.Controls.Add(this.btnOpen);
            this.Name = "FrmMain";
            this.Text = "Packing";
            this.gbConnInfo.ResumeLayout(false);
            this.gbConnInfo.PerformLayout();
            this.gbEmp1.ResumeLayout(false);
            this.gbEmp1.PerformLayout();
            this.gbEmp2.ResumeLayout(false);
            this.gbEmp2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox txtEmp1Info;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
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
        private System.Windows.Forms.Button btnEmp1Conn;
        private System.Windows.Forms.Button btnEmp1Start;
        private System.Windows.Forms.Button btnEmp1Pause;
        private System.Windows.Forms.Button btnEmp1Stop;
        private System.Windows.Forms.Button btnEmp2Stop;
        private System.Windows.Forms.Button btnEmp2Pause;
        private System.Windows.Forms.Button btnEmp2Start;
        private System.Windows.Forms.Button btnEmp2Conn;
        private System.Windows.Forms.TextBox txtEmp2Info;
        private System.Windows.Forms.Button btnDebug;
        private System.Windows.Forms.Button btnQuit;
        private System.Windows.Forms.GroupBox gbEmp1;
        private System.Windows.Forms.GroupBox gbEmp2;
    }
}

