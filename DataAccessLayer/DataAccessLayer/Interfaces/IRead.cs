using System;
using System.Data;

namespace DataAccessLayer.Interfaces
{
    internal interface IRead
    {
        /// <summary>
        ///     Queries database with passed query string.
        ///     Returns <seealso cref="DataTable" />DataTable with a name of the passed table name string
        /// </summary>
        /// <param name="pStrQuery"></param>
        /// <param name="pStrTableName"></param>
        /// <returns>
        ///     <seealso cref="DataTable" />
        /// </returns>
        DataTable GetDataTable(String pStrQuery, String pStrTableName);

        /// <summary>
        ///     Queries database with 'SELECT * query string.
        ///     Returns <seealso cref="DataTable" />DataTable with a name of the passed table 'name string'
        /// </summary>
        /// <param name="pStrTableName"></param>
        /// <returns>
        ///     <seealso cref="DataTable" />
        /// </returns>
        DataTable GetDataTable(String pStrTableName);

        /// <summary>
        ///     Queries database with passed query string.
        ///     Returns <seealso cref="DataSet" />DataSet containing a Datatable with the name of the passed 'table name' string
        /// </summary>
        /// <param name="pDataset"></param>
        /// <param name="pStrQuery"></param>
        /// <param name="pStrTableName"></param>
        /// <returns>
        ///     <seealso cref="DataSet" />
        /// </returns>
        DataSet FillDataSet(DataSet pDataset, String pStrQuery, String pStrTableName);
    }
}