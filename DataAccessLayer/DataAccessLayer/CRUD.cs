using System;
using System.Data;
using System.Data.Common;
using DataAccessLayer.Interfaces;

namespace DataAccessLayer
{
    public class Crud : ICrud
    {
        #region Class Variables

        private readonly IDataConnection _dataConnection;
        private readonly DataSet _dataSet;
        private readonly IDbConnection _dbConnection;

        #endregion

        #region Constriuctor

        /// <summary>
        ///     Pre-Condition:  A data connection that implements the interface IDataConnection.
        ///     Description:    This constructor will save a local instance of the data connection object.
        ///     Extract and save a local instance of the Database connection stored in the data connection object.
        ///     Create a new instance of a dataset to be used locally.
        /// </summary>
        /// <param name="pDataconnection"></param>
        public Crud(IDataConnection pDataconnection)
        {
            _dataConnection = pDataconnection;
            _dbConnection = _dataConnection.DbConnection();
            _dataSet = new DataSet();
        }

        #endregion

        #region Properties

        public Boolean IsConnectionOpen
        {
            get { return _dbConnection.State == ConnectionState.Open; }
        }

        #endregion

        #region Accessors

        /// <summary>
        ///     Queries database with passed query string.
        ///     Returns <seealso cref="System.Data.DataTable" />DataTable with a name of the passed table name string
        /// </summary>
        /// <param name="pStrQuery"></param>
        /// <param name="pStrTableName"></param>
        /// <returns>
        ///     <seealso cref="System.Data.DataTable" />
        /// </returns>
        public DataTable GetDataTable(string pStrQuery, string pStrTableName)
        {
            if (pStrQuery == null) throw new ArgumentNullException("pStrQuery");
            return FillDataSet(_dataSet, pStrQuery, pStrTableName).Tables[pStrTableName];
        }

        /// <summary>
        ///     Queries database with 'SELECT * query string.
        ///     Returns <seealso cref="System.Data.DataTable" />DataTable with a name of the passed table 'name string'
        /// </summary>
        /// <param name="pStrTableName"></param>
        /// <returns>
        ///     <seealso cref="System.Data.DataTable" />
        /// </returns>
        public DataTable GetDataTable(string pStrTableName)
        {
            string strQuery = "SELECT * FROM [" + pStrTableName + "]";
            return FillDataSet(_dataSet, strQuery, pStrTableName).Tables[pStrTableName];
        }

        /// <summary>
        ///     Queries database with passed query string.
        ///     Returns <seealso cref="System.Data.DataSet" />DataSet containing a Datatable with the name of the passed 'table
        ///     name' string
        /// </summary>
        /// <param name="pDataSet"></param>
        /// <param name="pStrQuery"></param>
        /// <param name="pStrTableName"></param>
        /// <returns>
        ///     <seealso cref="System.Data.DataSet" />
        /// </returns>
        public DataSet FillDataSet(DataSet pDataSet, string pStrQuery, string pStrTableName)
        {
            try
            {
                IDbDataAdapter dbDa = _dataConnection.DbDataAdapter();
                IDbCommand dbCmd = _dataConnection.DbCommand();
                dbCmd.CommandText = pStrQuery;
                dbCmd.Connection = _dbConnection;
                dbDa.SelectCommand = dbCmd;
                //Check if the connection is opened. Open connection if false
                if (!IsConnectionOpen)
                    _dbConnection.Open();
                //Fill the Datatable using the Data addaptor fill method
                dbDa.Fill(pDataSet);
                //Name the tabel with the table name spercified by the parameter
                pDataSet.Tables["Table"].TableName = pStrTableName;
                //Set the primary key constraints of the table
                pDataSet.Tables[pStrTableName].Constraints.Add("primaryKey", pDataSet.Tables[pStrTableName].Columns[0],
                    true);
                pDataSet.Tables[pStrTableName].Columns[0].AutoIncrement = true;
                pDataSet.Tables[pStrTableName].Columns[0].AutoIncrementSeed = -1;
                pDataSet.Tables[pStrTableName].Columns[0].AutoIncrementStep = -10;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                _dbConnection.Close();
            }
            return pDataSet;
        }

        #endregion

        #region Mutators

        /// <summary>
        ///     Uses <seealso cref="System.Data.Common.DbDataAdapter.Update(System.Data.DataSet)" /> DbDataAdaptor class to update
        ///     database table.
        ///     Calls the respective INSERT, UPDATE, or DELETE statements for each inserted,
        ///     updated, or deleted row in the specified System.Data.DataSet.
        /// </summary>
        /// <param name="pDataSet"></param>
        /// <param name="pStrTableName"></param>
        /// <returns>Int Number of affected records</returns>
        public int SaveData(DataSet pDataSet, string pStrTableName)
        {
            int rowsAffected = 0;
            string strQuery = "SELECT * FROM [" + pStrTableName + "]";

            DbDataAdapter dbDa = _dataConnection.DbDataAdapter(strQuery, _dbConnection);
            try
            {
                //Setup command Builders
                DbCommandBuilder dbBld = _dataConnection.DbCommandBuilder(dbDa);
                dbDa.InsertCommand = dbBld.GetInsertCommand();
                dbDa.UpdateCommand = dbBld.GetUpdateCommand();
                dbDa.DeleteCommand = dbBld.GetDeleteCommand();


                if (!IsConnectionOpen)
                    _dbConnection.Open();
                //Calls the respective INSERT, UPDATE, or DELETE statements for each inserted,
                //updated, or deleted row in the specified System.Data.DataSet.
                rowsAffected = dbDa.Update(pDataSet, pStrTableName);

                pDataSet.Tables[pStrTableName].AcceptChanges();

                _dbConnection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                _dbConnection.Close();
            }
            return rowsAffected;
        }

        #endregion
    }
}