using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using static SimplyCastSync.Runtime.EngineRuntime;
using SimplyCastSync.SimplyCast_Sunix;
using SimplyCastSync.Runtime;
using System.IO;
using System.Diagnostics;

namespace SimplyCastSync_Desktop
{
    public partial class Form1 : Form
    {
        /// <summary>
        /// 
        /// </summary>
        private Task background_sync;

        /// <summary>
        /// 
        /// </summary>
        public Form1()
        {
            InitializeComponent();
            syncsimple_btn.Enabled = true;
            stopsync_btn.Enabled = false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void syncsimple_btn_Click(object sender, EventArgs e)
        {
            background_sync = Runtime.Run(Comparer.Sync);
            if (!Runtime.Exit)
            {
                syncsimple_btn.Enabled = false;
                stopsync_btn.Enabled = true;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void stopsync_btn_Click(object sender, EventArgs e)
        {
            Runtime.Exit = true;
            if (background_sync != null)
                background_sync.Wait();
            if (Runtime.Exit)
            {
                syncsimple_btn.Enabled = true;
                stopsync_btn.Enabled = false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            stopsync_btn_Click(sender, e);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void aboutUsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void currentLoggingFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start("notepad.exe", @"FileLog\log_" + DateTime.Now.ToString("yyyyMMdd") + ".txt");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void openLoggingDirectoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start(@"FileLog");
        }

        private void foxproDriverToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ProcessStartInfo info = new ProcessStartInfo(Directory.GetCurrentDirectory().ToString() + "\\VFPOLEDBSetup.msi");
            Process.Start(info);
        }
    }
}
