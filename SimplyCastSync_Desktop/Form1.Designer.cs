namespace SimplyCastSync_Desktop
{
    partial class Form1
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
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.syncsimple_btn = new System.Windows.Forms.Button();
            this.stopsync_btn = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.loggingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.currentLoggingFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openLoggingDirectoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutUsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dBDriverToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.foxproDriverToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // syncsimple_btn
            // 
            this.syncsimple_btn.Location = new System.Drawing.Point(58, 81);
            this.syncsimple_btn.Name = "syncsimple_btn";
            this.syncsimple_btn.Size = new System.Drawing.Size(88, 46);
            this.syncsimple_btn.TabIndex = 2;
            this.syncsimple_btn.Text = "Start Sync";
            this.syncsimple_btn.UseVisualStyleBackColor = true;
            this.syncsimple_btn.Click += new System.EventHandler(this.syncsimple_btn_Click);
            // 
            // stopsync_btn
            // 
            this.stopsync_btn.Location = new System.Drawing.Point(205, 81);
            this.stopsync_btn.Name = "stopsync_btn";
            this.stopsync_btn.Size = new System.Drawing.Size(88, 46);
            this.stopsync_btn.TabIndex = 3;
            this.stopsync_btn.Text = "Stop Sync";
            this.stopsync_btn.UseVisualStyleBackColor = true;
            this.stopsync_btn.Click += new System.EventHandler(this.stopsync_btn_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loggingToolStripMenuItem,
            this.aboutUsToolStripMenuItem,
            this.dBDriverToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(351, 25);
            this.menuStrip1.TabIndex = 4;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // loggingToolStripMenuItem
            // 
            this.loggingToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.currentLoggingFileToolStripMenuItem,
            this.openLoggingDirectoryToolStripMenuItem});
            this.loggingToolStripMenuItem.Name = "loggingToolStripMenuItem";
            this.loggingToolStripMenuItem.Size = new System.Drawing.Size(48, 21);
            this.loggingToolStripMenuItem.Text = "Logs";
            // 
            // currentLoggingFileToolStripMenuItem
            // 
            this.currentLoggingFileToolStripMenuItem.Name = "currentLoggingFileToolStripMenuItem";
            this.currentLoggingFileToolStripMenuItem.Size = new System.Drawing.Size(191, 22);
            this.currentLoggingFileToolStripMenuItem.Text = "Open Log File";
            this.currentLoggingFileToolStripMenuItem.Click += new System.EventHandler(this.currentLoggingFileToolStripMenuItem_Click);
            // 
            // openLoggingDirectoryToolStripMenuItem
            // 
            this.openLoggingDirectoryToolStripMenuItem.Name = "openLoggingDirectoryToolStripMenuItem";
            this.openLoggingDirectoryToolStripMenuItem.Size = new System.Drawing.Size(191, 22);
            this.openLoggingDirectoryToolStripMenuItem.Text = "Open Log Directory";
            this.openLoggingDirectoryToolStripMenuItem.Click += new System.EventHandler(this.openLoggingDirectoryToolStripMenuItem_Click);
            // 
            // aboutUsToolStripMenuItem
            // 
            this.aboutUsToolStripMenuItem.Name = "aboutUsToolStripMenuItem";
            this.aboutUsToolStripMenuItem.Size = new System.Drawing.Size(74, 21);
            this.aboutUsToolStripMenuItem.Text = "About Us";
            this.aboutUsToolStripMenuItem.Visible = false;
            this.aboutUsToolStripMenuItem.Click += new System.EventHandler(this.aboutUsToolStripMenuItem_Click);
            // 
            // dBDriverToolStripMenuItem
            // 
            this.dBDriverToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.foxproDriverToolStripMenuItem});
            this.dBDriverToolStripMenuItem.Name = "dBDriverToolStripMenuItem";
            this.dBDriverToolStripMenuItem.Size = new System.Drawing.Size(76, 21);
            this.dBDriverToolStripMenuItem.Text = "DB Driver";
            // 
            // foxproDriverToolStripMenuItem
            // 
            this.foxproDriverToolStripMenuItem.Name = "foxproDriverToolStripMenuItem";
            this.foxproDriverToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.foxproDriverToolStripMenuItem.Text = "Foxpro Driver";
            this.foxproDriverToolStripMenuItem.Click += new System.EventHandler(this.foxproDriverToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(351, 197);
            this.Controls.Add(this.stopsync_btn);
            this.Controls.Add(this.syncsimple_btn);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Sunix_ValuedPatient";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button syncsimple_btn;
        private System.Windows.Forms.Button stopsync_btn;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem loggingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem currentLoggingFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openLoggingDirectoryToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutUsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dBDriverToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem foxproDriverToolStripMenuItem;
    }
}

