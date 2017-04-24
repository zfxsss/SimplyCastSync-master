using System;
using System.Data;
using System.Data.OleDb;

namespace SimplyCastSync.DBAccess
{
    /// <summary>
    /// use OLEDB Driver to access Foxpro tables
    /// </summary>
    public class FoxproDataQuery : IQuery<DataSet>, IDisposable
    {
        /// <summary>
        /// 
        /// </summary>
        private string _connstr;

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public DataSet GetData(string querystr)
        {
            DataSet ds = new DataSet();
            using (OleDbConnection conn = new OleDbConnection(_connstr))
            using (OleDbCommand comm = new OleDbCommand(querystr, conn))
            using (OleDbDataAdapter adapter = new OleDbDataAdapter(comm))
            {
                try
                {
                    adapter.Fill(ds);
                    return ds;
                }
                catch (Exception ex)
                {
                    // add log

                    return null;
                }
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        public void UpdateData(DataSet ds, params string[] extras)
        {
            //return 0;
        }

        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        public FoxproDataQuery(string connstr)
        {
            _connstr = connstr;
        }

    }
}
