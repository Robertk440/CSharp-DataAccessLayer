using System.Data;
using System.Data.Common;

namespace DataAccessLayer.Interfaces
{
    public interface IDataConnection
    {
        /// <summary>
        ///     Represents an open connection to a data source, and is implemented by .NET Framework data providers that access
        ///     relational databases.
        /// </summary>
        IDbConnection DbConnection();

        /// <summary>
        ///     Represents a set of command-related properties that are used to fill the <see cref="T:System.Data.DataSet" /> and
        ///     update a data source, and is implemented by .NET Framework data providers that access relational databases.
        /// </summary>
        IDbDataAdapter DbDataAdapter();

        /// <summary>
        ///     Aids implementation of the <see cref="T:System.Data.IDbDataAdapter" /> interface. Inheritors of
        ///     <see cref="T:System.Data.Common.DbDataAdapter" /> implement a set of functions to provide strong typing, but
        ///     inherit most of the functionality needed to fully implement a DataAdapter.
        /// </summary>
        DbDataAdapter DbDataAdapter(string selectCommandText, IDbConnection selectConnection);

        /// <summary>
        ///     Represents an SQL statement that is executed while connected to a data source, and is implemented by .NET Framework
        ///     data providers that access relational databases.
        /// </summary>
        IDbCommand DbCommand();

        /// <summary>
        ///     Automatically generates single-table commands used to reconcile changes made to a
        ///     <see cref="T:System.Data.DataSet" /> with the associated database. This is an abstract class that can only be
        ///     inherited.
        /// </summary>
        DbCommandBuilder DbCommandBuilder(IDbDataAdapter dataAdapter);
    }
}