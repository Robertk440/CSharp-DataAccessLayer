using System.Data;
using System.Data.Common;
using System.Data.OleDb;
using DataAccessLayer.Interfaces;

namespace DataAccessLayer
{
    /// <summary>
    ///     Using Singleton Design pattern.
    ///     Has private constructor
    ///     Has Private Static instance
    ///     Public GetInstance()
    /// </summary>
    public class OleDbDataConnection : IDataConnection
    {
        #region Class Variables

        //Private OleDb Connection String
        private const string StrConnection =
            "Provider=SQLNCLI11; Server=ROBERT-PC\\SQLEXPRESS;Database=CrudTest;Trusted_Connection=yes;";

        #endregion

        #region Constructor

        /// <summary>
        ///     Private Constructor
        /// </summary>
        private OleDbDataConnection()
        {
        }

        #endregion

        #region Properties

        /// Private static instance of class
        private static OleDbDataConnection _instance;

        /// <summary>
        ///     Represents an open connection to a data source, and is implemented by .NET Framework data providers that access
        ///     relational databases.
        /// </summary>
        /// <returns>
        ///     IDbConnection<seealso cref="IDbConnection" /> as instance of OleDbConnection
        ///     <seealso cref="OleDbConnection" />
        /// </returns>
        public IDbConnection DbConnection()
        {
            return new OleDbConnection(StrConnection);
        }

        /// <summary>
        ///     Represents a set of command-related properties that are used to fill the <see cref="T:System.Data.DataSet" /> and
        ///     update a data source, and is implemented by .NET Framework data providers that access relational databases.
        /// </summary>
        /// <returns>
        ///     IDbDataAdaptor<seealso cref="IDbDataAdapter" /> as instance of OleDbDataAdapter
        ///     <seealso cref="OleDbDataAdapter" />
        /// </returns>
        public IDbDataAdapter DbDataAdapter()
        {
            return new OleDbDataAdapter();
        }

        /// <summary>
        ///     Aids implementation of the <see cref="T:System.Data.IDbDataAdapter" /> interface. Inheritors of
        ///     <see cref="T:System.Data.Common.DbDataAdapter" /> implement a set of functions to provide strong typing, but
        ///     inherit most of the functionality needed to fully implement a DataAdapter.
        /// </summary>
        /// <param name="selectCommandText"></param>
        /// <param name="selectConnection"></param>
        /// <returns>
        ///     DbDataAdaptor<seealso cref="DbDataAdapter" /> as instance of OleDbDataAdapter
        ///     <seealso cref="OleDbDataAdapter" />
        /// </returns>
        public DbDataAdapter DbDataAdapter(string selectCommandText, IDbConnection selectConnection)
        {
            var dbConnection = (OleDbConnection) selectConnection;
            return new OleDbDataAdapter(selectCommandText, dbConnection);
        }

        /// <summary>
        ///     Represents an SQL statement that is executed while connected to a data source, and is implemented by .NET Framework
        ///     data providers that access relational databases.
        /// </summary>
        /// <returns>IDbCommand<seealso cref="IDbCommand" /> as instance of OleDbCommand<seealso cref="OleDbCommand" /></returns>
        public IDbCommand DbCommand()
        {
            return new OleDbCommand();
        }

        /// <summary>
        ///     Automatically generates single-table commands used to reconcile changes made to a
        ///     <see cref="T:System.Data.DataSet" /> with the associated database. This is an abstract class that can only be
        ///     inherited.
        /// </summary>
        /// <param name="dataAdapter"></param>
        /// <returns>
        ///     DbCommandBuilder<seealso cref="DbCommandBuilder" /> as instance of OleDbCommandBuilder
        ///     <seealso cref="OleDbCommandBuilder" />
        /// </returns>
        public DbCommandBuilder DbCommandBuilder(IDbDataAdapter dataAdapter)
        {
            var da = (OleDbDataAdapter) dataAdapter;
            return new OleDbCommandBuilder(da);
        }

        public static OleDbDataConnection GetInstance()
        {
            return _instance ?? (_instance = new OleDbDataConnection());
        }

        #endregion
    }
}