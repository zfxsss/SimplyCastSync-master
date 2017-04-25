using System;
using System.Data;
using System.Data.OleDb;
using SimplyCastSync.PubLib.Log;

using static SimplyCastSync.PubLib.Log.Log;
using SimplyCastSync.Exceptions;

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
            try
            {
                DataSet ds = new DataSet();
                using (OleDbConnection conn = new OleDbConnection(_connstr))
                using (OleDbCommand comm = new OleDbCommand(querystr, conn))
                using (OleDbDataAdapter adapter = new OleDbDataAdapter(comm))
                {

                    adapter.Fill(ds);
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        for (int i = 0; i < ds.Tables[0].Columns.Count; i++)
                        {
                            if (row[i].GetType() == typeof(string))
                            {
                                row[i] = row[i] == null ? "" : ((string)row[i]).Trim();
                            }
                        }
                    }
                    ds.AcceptChanges();

                    return ds;
                }
            }
            catch (Exception ex)
            {
                // add log
                throw new DomainException(ex.Message, ExceptionSrc.Processing, ExceptionType.Error);

                //return null;
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

            // add log
            new DomainException("Foxpro Connection String Loaded", ExceptionSrc.Processing, ExceptionType.Message);
        }

    }
}
