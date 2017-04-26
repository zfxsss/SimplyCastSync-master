using System.Threading.Tasks;

using static SimplyCastSync.PubLib.Log.Log;

namespace SimplyCastSync.PubLib.Log
{
    /// <summary>
    /// 
    /// </summary>
    internal class LogExportAsync
    {
        /// <summary>
        /// 
        /// </summary>
        internal static Task ExportConsoleLogAsync()
        {
            return Task.Run(() =>
            {
                ExportConsoleLog();
            });
        }

        /// <summary>
        /// 
        /// </summary>
        internal static Task ExportFileLogAsync()
        {
            return Task.Run(() =>
            {
                ExportFileLog();
            });
        }
    }
}
