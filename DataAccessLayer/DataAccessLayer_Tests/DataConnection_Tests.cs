using System.Data;
using System.Data.Common;
using DataAccessLayer;
using NUnit.Framework;

namespace DbDataAccessLayer_Tests
{
    [TestFixture]
    internal class DataConnectionTests
    {
        [TestCase]
        public void OleDbConnection_onCall_returnOledbConnection_NotNull()
        {
            OleDbDataConnection dc = OleDbDataConnection.GetInstance();
            IDbConnection result = dc.DbConnection();
            Assert.NotNull(result);
        }

        [TestCase]
        public void OleDbConnection_OpenConnection()
        {
            OleDbDataConnection dc = OleDbDataConnection.GetInstance();
            IDbConnection result = dc.DbConnection();
            result.Open();
            Assert.IsTrue(result.State == ConnectionState.Open);
        }


        [TestCase]
        public void DbCommand_returnObject_NotNull()
        {
            OleDbDataConnection dc = OleDbDataConnection.GetInstance();
            IDbCommand result = dc.DbCommand();
            Assert.NotNull(result);
        }

        [TestCase]
        public void DbDataAdapter_returnObject_NotNull()
        {
            OleDbDataConnection dc = OleDbDataConnection.GetInstance();
            IDbDataAdapter result = dc.DbDataAdapter();
            Assert.NotNull(result);
        }


        [TestCase]
        public void dbCommandBuilder_returnObject_NotNull()
        {
            OleDbDataConnection dc = OleDbDataConnection.GetInstance();
            IDbDataAdapter da = dc.DbDataAdapter();
            DbCommandBuilder result = dc.DbCommandBuilder(da);

            Assert.NotNull(result);
        }
    }
}