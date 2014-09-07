using System;
using System.Data;
using System.Data.Common;

namespace DataAccessLayer.Interfaces
{
    internal interface IUpdate
    {
        /// <summary>
        ///     Uses <seealso cref="DbDataAdapter.Update(System.Data.DataSet)" /> DbDataAdaptor class to update database table.
        ///     Calls the respective INSERT, UPDATE, or DELETE statements for each inserted,
        ///     updated, or deleted row in the specified System.Data.DataSet.
        /// </summary>
        /// <param name="pDataSet"></param>
        /// <param name="pStrTableName"></param>
        /// <returns>Int Number of affected records</returns>
        int SaveData(DataSet pDataSet, String pStrTableName);
    }
}