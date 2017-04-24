using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimplyCastSync.CompareEngine.Strategy.Attributes
{
    /// <summary>
    /// 
    /// </summary>
    public class StrategyParams
    {
        /// <summary>
        /// 
        /// </summary>
        public string StrategyName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool UsingParams { get; set; }

    }

    /// <summary>
    /// 
    /// </summary>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = false)]
    public class StrategyValidation : Attribute
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="providername"></param>
        public StrategyValidation(string strategysetname)
        {
            Collection = new List<StrategyParams>();
            if (strategysetname == "Default")
            {
                Collection.Add(new StrategyParams { StrategyName = "", UsingParams = true });
                Collection.Add(new StrategyParams { StrategyName = "", UsingParams = true });
                Collection.Add(new StrategyParams { StrategyName = "", UsingParams = true });
                Collection.Add(new StrategyParams { StrategyName = "", UsingParams = true });
                Collection.Add(new StrategyParams { StrategyName = "", UsingParams = true });
                Collection.Add(new StrategyParams { StrategyName = "", UsingParams = true });
                Collection.Add(new StrategyParams { StrategyName = "", UsingParams = true });
            }

        }

        /// <summary>
        /// 
        /// </summary>
        public IList<StrategyParams> Collection { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public void test()
        {
            if (Collection.First(x => x.StrategyName == "").UsingParams) { }
        }
    }
}
