using Newtonsoft.Json.Linq;
using SimplyCastSync.CompareEngine.Strategy;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimplyCastSync.CompareEngine
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="S"></typeparam>
    /// <typeparam name="D"></typeparam>
    public interface IComparerT<S, D> : IComparer, ISyncStrategy<S, D> //where S : IEnumerable where D : IEnumerable
    {
        /// <summary>
        /// 
        /// </summary>
        S Source { get; }

        /// <summary>
        /// 
        /// </summary>
        D Destination { get; }

        /// <summary>
        /// 
        /// </summary>
        JObject Config { get; }
    }

}
