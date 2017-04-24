using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimplyCastSync.Runtime
{
    /// <summary>
    /// 
    /// </summary>
    public interface IEngine
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="doit"></param>
        /// <returns></returns>
        TaskStatus Run(Action doit);

        /// <summary>
        /// 
        /// </summary>
        bool Exit { get; set; }
    }
}
