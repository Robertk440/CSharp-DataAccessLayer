using System;
using System.Data;
using DataAccessLayer;
using NUnit.Framework;

namespace DbDataAccessLayer_Tests
{
    [TestFixture]
    internal class CRUD_tests
    {
        [Test]
        public void GetDataTable_ReturnObject_NotNull()
        {
            var crud = new Crud(OleDbDataConnection.GetInstance());
            var result = new DataTable();
            result = crud.GetDataTable("TestTable");
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
            const string strQuery = "SELECT * FROM [dbo].[TestTable]";
            const string TableName = "TestTable";
            var crud = new Crud(OleDbDataConnection.GetInstance());
            var ds = new DataSet();
            ds = crud.FillDataSet(ds, strQuery, TableName);

            DataRow dr = ds.Tables[TableName].NewRow();
            dr["Name"] = "Fred";
            dr["Number"] = "046131";
            dr["Description"] = "This is a row added by the unit test";

            ds.Tables[TableName].Rows.Add(dr);
            int Result = crud.SaveData(ds, TableName);
            Assert.AreEqual(1, Result);
        }

        [Test]
        public void SaveData_SaveDatasetToDatabase_TwoRowsAffected()
        {
            const string strQuery = "SELECT * FROM [dbo].[TestTable]";
            const string TableName = "TestTable";
            var crud = new Crud(OleDbDataConnection.GetInstance());
            var ds = new DataSet();
            ds = crud.FillDataSet(ds, strQuery, TableName);

            DataRow dr = ds.Tables[TableName].NewRow();
            dr["Name"] = "Fred";
            dr["Number"] = "046131";
            dr["Description"] = "This is a row added by the unit test";

            ds.Tables[TableName].Rows.Add(dr);
            dr = ds.Tables[TableName].NewRow();
            dr["Name"] = "Jill";
            dr["Number"] = "213251";
            dr["Description"] = "This is a row added by the unit test";

            ds.Tables[TableName].Rows.Add(dr);

            int Result = crud.SaveData(ds, TableName);
            Assert.AreEqual(2, Result);
        }

        [Test]
        public void fillDataSet_ReturnObject_containsTable()
        {
            String strQuery = "SELECT * FROM [dbo].[TestTable]";
            var crud = new Crud(OleDbDataConnection.GetInstance());
            var dataset = new DataSet();
            dataset = crud.FillDataSet(dataset, strQuery, "dbo.TestTable");
            int result = dataset.Tables.Count;
            Assert.AreEqual(1, result);
        }

        [Test]
        public void fillDataSet_ReturnObject_notNull()
        {
            String strQuery = "SELECT * FROM [dbo].[TestTable]";
            var crud = new Crud(OleDbDataConnection.GetInstance());
            var result = new DataSet();
            result = crud.FillDataSet(result, strQuery, "dbo.TestTable");
            Assert.NotNull(result);
        }
    }
}