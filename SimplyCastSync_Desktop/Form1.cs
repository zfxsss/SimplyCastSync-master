using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using SimplyCastSync.CompareEngine;
using static SimplyCastSync.Config.ConfigRepository;
using Newtonsoft.Json.Linq;
using static SimplyCastSync.Runtime.EngineRuntime;
using SimplyCastSync.SimplyCast_Sunix;

namespace SimplyCastSync_Desktop
{
    public partial class Form1 : Form
    {
        /// <summary>
        /// 
        /// </summary>
        public Form1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rdconfig_btn_Click(object sender, EventArgs e)
        {
            Runtime.Run(CompareEngine.Sync);
            Runtime.Exit = true;

            var src = default(JObject);
            var dest = default(JObject);
            if (Content != null)
            {
                var pairsconfig = Content["pairs"];
                if ((pairsconfig != null) && pairsconfig.GetType() == typeof(JArray))
                {
                    foreach (var pair in pairsconfig)
                    {
                        src = pair["source"] as JObject;
                        dest = pair["destination"] as JObject;
                    }
                }
            }



        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void sync_btn_Click(object sender, EventArgs e)
        {
            CompareEngine.Sync();
        }

        private void syncsimple_btn_Click(object sender, EventArgs e)
        {
            Comparer.Sync();
        }
    }
}
