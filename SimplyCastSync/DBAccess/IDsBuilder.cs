
namespace SimplyCastSync.DBAccess
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDsBuilder
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IQuery<T> GetQuery<T>(string queryname, string connstr);
    }
}
