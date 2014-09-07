using System;
using System.Data;
using System.Data.Common;
using System.Data.OleDb;
using DataAccessLayer.Interfaces;

namespace DbDataAccessLayer_Tests.Mocks
{
    internal class OleDbDataConnectionMock : IDataConnection
    {
        /*
         *!!THIS CLASS IS NOT FUNCTIONAL!! 
         * ToDo: Finish this mock object before production 
         */

        public OleDbDataConnectionMock(String pStrConnection, String pStrDataFile)
        {
        }


        public string StrDbFile { get; protected set; }

        public IDbConnection DbConnection()
        {
            return new OleDbConnection(StrDbFile);
        }

        public IDbDataAdapter DbDataAdapter()
        {
            return new OleDbDataAdapter();
        }

        public IDbCommand DbCommand()
        {
            return new OleDbCommand();
        }

        public DbDataAdapter DbDataAdapter(string selectCommandText, IDbConnection selectConnection)
        {
            throw new NotImplementedException();
        }

        public DbCommandBuilder DbCommandBuilder(IDbDataAdapter dataAdapter)
        {
            throw new NotImplementedException();
        }
    }
}