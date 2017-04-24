
namespace SimplyCastSync.CompareEngine
{
    /// <summary>
    /// 
    /// </summary>
    public interface IComparer
    {

        /// <summary>
        /// 
        /// </summary>
        void InitializeS(string initstrategy, params object[] p);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="p"></param>
        void InitializeD(string initstrategy, params object[] p);

        /// <summary>
        /// 
        /// </summary>
        void Mark(string markstrategy, params object[] p);

        /// <summary>
        /// 
        /// </summary>
        void Commit();
    }
}
