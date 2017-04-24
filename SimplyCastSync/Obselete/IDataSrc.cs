using System;
using SimplyCastSync.DBAccess;
using SimplyCastSync.Config;
using System.Collections.Generic;

namespace SimplyCastSync.DataSource
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDataSrc<T> : IDisposable
    {
        /// <summary>
        /// 
        /// </summary>
        IQuery<T> Q { get; }
        /// <summary>
        /// 
        /// </summary>
        ConfigItems<KeyValuePair<string, string>> Config { get; }
    }
}
