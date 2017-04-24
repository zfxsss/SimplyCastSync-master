using SimplyCastSync.CompareEngine.Strategy.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimplyCastSync.CompareEngine.Strategy
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISyncStrategy<S, D>
    {
        /// <summary>
        /// 
        /// </summary>
        Action<IComparerT<S, D>> StrategySync { get; }
    }
}
