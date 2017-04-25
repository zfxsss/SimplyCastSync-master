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
            this.rdconfig_btn = new System.Windows.Forms.Button();
            this.sync_btn = new System.Windows.Forms.Button();
            this.syncsimple_btn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // rdconfig_btn
            // 
            this.rdconfig_btn.Location = new System.Drawing.Point(31, 24);
            this.rdconfig_btn.Name = "rdconfig_btn";
            this.rdconfig_btn.Size = new System.Drawing.Size(76, 57);
            this.rdconfig_btn.TabIndex = 0;
            this.rdconfig_btn.Text = "Reading Config";
            this.rdconfig_btn.UseVisualStyleBackColor = true;
            this.rdconfig_btn.Click += new System.EventHandler(this.rdconfig_btn_Click);
            // 
            // sync_btn
            // 
            this.sync_btn.Location = new System.Drawing.Point(32, 107);
            this.sync_btn.Name = "sync_btn";
            this.sync_btn.Size = new System.Drawing.Size(75, 25);
            this.sync_btn.TabIndex = 1;
            this.sync_btn.Text = "Sync";
            this.sync_btn.UseVisualStyleBackColor = true;
            this.sync_btn.Click += new System.EventHandler(this.sync_btn_Click);
            // 
            // syncsimple_btn
            // 
            this.syncsimple_btn.Location = new System.Drawing.Point(32, 185);
            this.syncsimple_btn.Name = "syncsimple_btn";
            this.syncsimple_btn.Size = new System.Drawing.Size(88, 50);
            this.syncsimple_btn.TabIndex = 2;
            this.syncsimple_btn.Text = "Sync_Simple";
            this.syncsimple_btn.UseVisualStyleBackColor = true;
            this.syncsimple_btn.Click += new System.EventHandler(this.syncsimple_btn_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(669, 354);
            this.Controls.Add(this.syncsimple_btn);
            this.Controls.Add(this.sync_btn);
            this.Controls.Add(this.rdconfig_btn);
            this.Name = "Form1";
            this.Text = "Sunix_SimplyCast";
            this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button rdconfig_btn;
        private System.Windows.Forms.Button sync_btn;
        private System.Windows.Forms.Button syncsimple_btn;
    }
}

