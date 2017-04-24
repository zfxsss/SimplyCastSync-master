using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimplyCastSync.CompareEngine.RdOperation
{
    /// <summary>
    /// record event handler
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="U"></typeparam>
    public class RdHandler<T, U>
    {
        /// <summary>
        /// 
        /// </summary>
        public Dictionary<string, Action<T, U>> Handlers { get; private set; }

        public RdHandler(Action<T, U> h1, Action<T, U> h2, Action<T, U> h3)
        {
            Handlers = new Dictionary<string, Action<T, U>>();
            Handlers.Add("AddRdHandler", h1);
            Handlers.Add("UpdateRdHandler", h2);
            Handlers.Add("RemoveRdHandler", h3);
        }
    }
}
