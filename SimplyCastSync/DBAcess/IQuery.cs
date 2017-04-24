
namespace SimplyCastSync.DBAccess
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IQuery<T>
    {
        /// <summary>
        /// 
        /// </summary>k
        /// <returns></returns>
        T GetData(string querystr);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ds"></param>
        /// <param name="extras"></param>
        /// <returns></returns>
        void UpdateData(T ds, params string[] extras);

    }
}
