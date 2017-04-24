
namespace SimplyCastSync.DataSource
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDsProvider
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IDataSrc<T> GetDs<T>(string dsname);
    }
}
