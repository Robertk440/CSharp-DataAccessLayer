using System;
using System.Data;
using DataAccessLayer;
using NUnit.Framework;

namespace DbDataAccessLayer_Tests
{
    [TestFixture]
    internal class CrudTests
    {
        [Test]
        public void GetDataTable_ReturnObject_NotNull()
        {
            var crud = new Crud(OleDbDataConnection.GetInstance());
            DataTable result = crud.GetDataTable("TestTable");
            Assert.NotNull(result);
        }

        [Test]
        public void GetDataTable_ReturnObject_WithRecord()
        {
            var crud = new Crud(OleDbDataConnection.GetInstance());
            DataRow result = crud.GetDataTable("TestTable").Rows[0];
            Assert.NotNull(result);
        }

        [Test]
        public void SaveData_SaveDatasetToDatabase_OneRowAffected()
        {
            const string tableName = "TestTable";
            string strQuery = String.Format("SELECT * FROM [{0}]", tableName);
            var crud = new Crud(OleDbDataConnection.GetInstance());
            var ds = new DataSet();
            ds = crud.FillDataSet(ds, strQuery, tableName);

            DataRow dr = ds.Tables[tableName].NewRow();
            dr["Name"] = "Fred";
            dr["Number"] = "046131";
            dr["Description"] = "This is a row added by the unit test";

            ds.Tables[tableName].Rows.Add(dr);
            int result = crud.SaveData(ds, tableName);
            Assert.AreEqual(1, result);
        }

        [Test]
        public void SaveData_SaveDatasetToDatabase_TwoRowsAffected()
        {
            const string tableName = "TestTable";
            string strQuery = String.Format("SELECT * FROM [{0}]", tableName);
            var crud = new Crud(OleDbDataConnection.GetInstance());
            var ds = new DataSet();
            ds = crud.FillDataSet(ds, strQuery, tableName);

            DataRow dr = ds.Tables[tableName].NewRow();
            dr["Name"] = "Fred";
            dr["Number"] = "046131";
            dr["Description"] = "This is a row added by the unit test";

            ds.Tables[tableName].Rows.Add(dr);
            dr = ds.Tables[tableName].NewRow();
            dr["Name"] = "Jill";
            dr["Number"] = "213251";
            dr["Description"] = "This is a row added by the unit test";

            ds.Tables[tableName].Rows.Add(dr);

            int result = crud.SaveData(ds, tableName);
            Assert.AreEqual(2, result);
        }

        [Test]
        public void fillDataSet_ReturnObject_containsTable()
        {
            const string tableName = "TestTable";
            String strQuery = String.Format("SELECT * FROM [{0}]", tableName);
            var crud = new Crud(OleDbDataConnection.GetInstance());
            var dataset = new DataSet();
            dataset = crud.FillDataSet(dataset, strQuery, "dbo.TestTable");
            int result = dataset.Tables.Count;
            Assert.AreEqual(1, result);
        }

        [Test]
        public void fillDataSet_ReturnObject_notNull()
        {
            const string tableName = "TestTable";
            String strQuery = String.Format("SELECT * FROM [{0}]", tableName);
            var crud = new Crud(OleDbDataConnection.GetInstance());
            var result = new DataSet();
            result = crud.FillDataSet(result, strQuery, "dbo.TestTable");
            Assert.NotNull(result);
        }
    }
}