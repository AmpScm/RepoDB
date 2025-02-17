﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Data.SqlClient;
using RepoDb.Extensions;
using RepoDb.SqlServer.IntegrationTests.Models;
using RepoDb.SqlServer.IntegrationTests.Setup;

namespace RepoDb.SqlServer.IntegrationTests.Operations;

[TestClass]
public class QueryMultipleTest
{
    [TestInitialize]
    public void Initialize()
    {
        Database.Initialize();
        Cleanup();
    }

    [TestCleanup]
    public void Cleanup()
    {
        Database.Cleanup();
    }

    #region DataEntity

    #region Sync

    [TestMethod]
    public void TestSqlServerConnectionQueryMultipleForT2()
    {
        // Setup
        var tables = Database.CreateCompleteTables(10);

        using (var connection = new SqlConnection(Database.ConnectionString))
        {
            // Act
            var result = connection.QueryMultiple<CompleteTable, CompleteTable>(e => e.Id > 0,
                e => e.Id > 0,
                top1: 1,
                top2: 2);

            // Assert
            Assert.AreEqual(1, result.Item1.Count());
            Assert.AreEqual(2, result.Item2.Count());
            result.Item1.AsList().ForEach(item => Helper.AssertPropertiesEquality(tables.First(e => e.Id == item.Id), item));
            result.Item2.AsList().ForEach(item => Helper.AssertPropertiesEquality(tables.First(e => e.Id == item.Id), item));
        }
    }

    [TestMethod]
    public void TestSqlServerConnectionQueryMultipleForT3()
    {
        // Setup
        var tables = Database.CreateCompleteTables(10);

        using (var connection = new SqlConnection(Database.ConnectionString))
        {
            // Act
            var result = connection.QueryMultiple<CompleteTable, CompleteTable, CompleteTable>(e => e.Id > 0,
                e => e.Id > 0,
                e => e.Id > 0,
                top1: 1,
                top2: 2,
                top3: 3);

            // Assert
            Assert.AreEqual(1, result.Item1.Count());
            Assert.AreEqual(2, result.Item2.Count());
            Assert.AreEqual(3, result.Item3.Count());
            result.Item1.AsList().ForEach(item => Helper.AssertPropertiesEquality(tables.First(e => e.Id == item.Id), item));
            result.Item2.AsList().ForEach(item => Helper.AssertPropertiesEquality(tables.First(e => e.Id == item.Id), item));
            result.Item3.AsList().ForEach(item => Helper.AssertPropertiesEquality(tables.First(e => e.Id == item.Id), item));
        }
    }

    [TestMethod]
    public void TestSqlServerConnectionQueryMultipleForT4()
    {
        // Setup
        var tables = Database.CreateCompleteTables(10);

        using (var connection = new SqlConnection(Database.ConnectionString))
        {
            // Act
            var result = connection.QueryMultiple<CompleteTable, CompleteTable, CompleteTable, CompleteTable>(e => e.Id > 0,
                e => e.Id > 0,
                e => e.Id > 0,
                e => e.Id > 0,
                top1: 1,
                top2: 2,
                top3: 3,
                top4: 4);

            // Assert
            Assert.AreEqual(1, result.Item1.Count());
            Assert.AreEqual(2, result.Item2.Count());
            Assert.AreEqual(3, result.Item3.Count());
            Assert.AreEqual(4, result.Item4.Count());
            result.Item1.AsList().ForEach(item => Helper.AssertPropertiesEquality(tables.First(e => e.Id == item.Id), item));
            result.Item2.AsList().ForEach(item => Helper.AssertPropertiesEquality(tables.First(e => e.Id == item.Id), item));
            result.Item3.AsList().ForEach(item => Helper.AssertPropertiesEquality(tables.First(e => e.Id == item.Id), item));
            result.Item4.AsList().ForEach(item => Helper.AssertPropertiesEquality(tables.First(e => e.Id == item.Id), item));
        }
    }

    [TestMethod]
    public void TestSqlServerConnectionQueryMultipleForT5()
    {
        // Setup
        var tables = Database.CreateCompleteTables(10);

        using (var connection = new SqlConnection(Database.ConnectionString))
        {
            // Act
            var result = connection.QueryMultiple<CompleteTable, CompleteTable, CompleteTable, CompleteTable, CompleteTable>(e => e.Id > 0,
                e => e.Id > 0,
                e => e.Id > 0,
                e => e.Id > 0,
                e => e.Id > 0,
                top1: 1,
                top2: 2,
                top3: 3,
                top4: 4,
                top5: 5);

            // Assert
            Assert.AreEqual(1, result.Item1.Count());
            Assert.AreEqual(2, result.Item2.Count());
            Assert.AreEqual(3, result.Item3.Count());
            Assert.AreEqual(4, result.Item4.Count());
            Assert.AreEqual(5, result.Item5.Count());
            result.Item1.AsList().ForEach(item => Helper.AssertPropertiesEquality(tables.First(e => e.Id == item.Id), item));
            result.Item2.AsList().ForEach(item => Helper.AssertPropertiesEquality(tables.First(e => e.Id == item.Id), item));
            result.Item3.AsList().ForEach(item => Helper.AssertPropertiesEquality(tables.First(e => e.Id == item.Id), item));
            result.Item4.AsList().ForEach(item => Helper.AssertPropertiesEquality(tables.First(e => e.Id == item.Id), item));
            result.Item5.AsList().ForEach(item => Helper.AssertPropertiesEquality(tables.First(e => e.Id == item.Id), item));
        }
    }

    [TestMethod]
    public void TestSqlServerConnectionQueryMultipleForT6()
    {
        // Setup
        var tables = Database.CreateCompleteTables(10);

        using (var connection = new SqlConnection(Database.ConnectionString))
        {
            // Act
            var result = connection.QueryMultiple<CompleteTable, CompleteTable, CompleteTable, CompleteTable, CompleteTable, CompleteTable>(e => e.Id > 0,
                e => e.Id > 0,
                e => e.Id > 0,
                e => e.Id > 0,
                e => e.Id > 0,
                e => e.Id > 0,
                top1: 1,
                top2: 2,
                top3: 3,
                top4: 4,
                top5: 5,
                top6: 6);

            // Assert
            Assert.AreEqual(1, result.Item1.Count());
            Assert.AreEqual(2, result.Item2.Count());
            Assert.AreEqual(3, result.Item3.Count());
            Assert.AreEqual(4, result.Item4.Count());
            Assert.AreEqual(5, result.Item5.Count());
            Assert.AreEqual(6, result.Item6.Count());
            result.Item1.AsList().ForEach(item => Helper.AssertPropertiesEquality(tables.First(e => e.Id == item.Id), item));
            result.Item2.AsList().ForEach(item => Helper.AssertPropertiesEquality(tables.First(e => e.Id == item.Id), item));
            result.Item3.AsList().ForEach(item => Helper.AssertPropertiesEquality(tables.First(e => e.Id == item.Id), item));
            result.Item4.AsList().ForEach(item => Helper.AssertPropertiesEquality(tables.First(e => e.Id == item.Id), item));
            result.Item5.AsList().ForEach(item => Helper.AssertPropertiesEquality(tables.First(e => e.Id == item.Id), item));
            result.Item6.AsList().ForEach(item => Helper.AssertPropertiesEquality(tables.First(e => e.Id == item.Id), item));
        }
    }

    [TestMethod]
    public void TestSqlServerConnectionQueryMultipleForT7()
    {
        // Setup
        var tables = Database.CreateCompleteTables(10);

        using (var connection = new SqlConnection(Database.ConnectionString))
        {
            // Act
            var result = connection.QueryMultiple<CompleteTable, CompleteTable, CompleteTable, CompleteTable, CompleteTable, CompleteTable, CompleteTable>(e => e.Id > 0,
                e => e.Id > 0,
                e => e.Id > 0,
                e => e.Id > 0,
                e => e.Id > 0,
                e => e.Id > 0,
                e => e.Id > 0,
                top1: 1,
                top2: 2,
                top3: 3,
                top4: 4,
                top5: 5,
                top6: 6,
                top7: 7);

            // Assert
            Assert.AreEqual(1, result.Item1.Count());
            Assert.AreEqual(2, result.Item2.Count());
            Assert.AreEqual(3, result.Item3.Count());
            Assert.AreEqual(4, result.Item4.Count());
            Assert.AreEqual(5, result.Item5.Count());
            Assert.AreEqual(6, result.Item6.Count());
            Assert.AreEqual(7, result.Item7.Count());
            result.Item1.AsList().ForEach(item => Helper.AssertPropertiesEquality(tables.First(e => e.Id == item.Id), item));
            result.Item2.AsList().ForEach(item => Helper.AssertPropertiesEquality(tables.First(e => e.Id == item.Id), item));
            result.Item3.AsList().ForEach(item => Helper.AssertPropertiesEquality(tables.First(e => e.Id == item.Id), item));
            result.Item4.AsList().ForEach(item => Helper.AssertPropertiesEquality(tables.First(e => e.Id == item.Id), item));
            result.Item5.AsList().ForEach(item => Helper.AssertPropertiesEquality(tables.First(e => e.Id == item.Id), item));
            result.Item6.AsList().ForEach(item => Helper.AssertPropertiesEquality(tables.First(e => e.Id == item.Id), item));
            result.Item7.AsList().ForEach(item => Helper.AssertPropertiesEquality(tables.First(e => e.Id == item.Id), item));
        }
    }

    [TestMethod]
    public void TestSqlServerConnectionQueryMultipleForT2WithHints()
    {
        // Setup
        var tables = Database.CreateCompleteTables(10);

        using (var connection = new SqlConnection(Database.ConnectionString))
        {
            // Act
            var result = connection.QueryMultiple<CompleteTable, CompleteTable>(e => e.Id > 0,
                e => e.Id > 0,
                top1: 1,
                top2: 2,
                hints1: SqlServerTableHints.NoLock,
                hints2: SqlServerTableHints.NoLock);

            // Assert
            Assert.AreEqual(1, result.Item1.Count());
            Assert.AreEqual(2, result.Item2.Count());
            result.Item1.AsList().ForEach(item => Helper.AssertPropertiesEquality(tables.First(e => e.Id == item.Id), item));
            result.Item2.AsList().ForEach(item => Helper.AssertPropertiesEquality(tables.First(e => e.Id == item.Id), item));
        }
    }

    #endregion

    #region Async

    [TestMethod]
    public async Task TestSqlServerConnectionQueryMultipleAsyncForT2()
    {
        // Setup
        var tables = Database.CreateCompleteTables(10);

        using (var connection = new SqlConnection(Database.ConnectionString))
        {
            // Act
            var result = await connection.QueryMultipleAsync<CompleteTable, CompleteTable>(e => e.Id > 0,
                e => e.Id > 0,
                top1: 1,
                top2: 2);

            // Assert
            Assert.AreEqual(1, result.Item1.Count());
            Assert.AreEqual(2, result.Item2.Count());
            result.Item1.AsList().ForEach(item => Helper.AssertPropertiesEquality(tables.First(e => e.Id == item.Id), item));
            result.Item2.AsList().ForEach(item => Helper.AssertPropertiesEquality(tables.First(e => e.Id == item.Id), item));
        }
    }

    [TestMethod]
    public async Task TestSqlServerConnectionQueryMultipleAsyncForT3()
    {
        // Setup
        var tables = Database.CreateCompleteTables(10);

        using (var connection = new SqlConnection(Database.ConnectionString))
        {
            // Act
            var result = await connection.QueryMultipleAsync<CompleteTable, CompleteTable, CompleteTable>(e => e.Id > 0,
                e => e.Id > 0,
                e => e.Id > 0,
                top1: 1,
                top2: 2,
                top3: 3);

            // Assert
            Assert.AreEqual(1, result.Item1.Count());
            Assert.AreEqual(2, result.Item2.Count());
            Assert.AreEqual(3, result.Item3.Count());
            result.Item1.AsList().ForEach(item => Helper.AssertPropertiesEquality(tables.First(e => e.Id == item.Id), item));
            result.Item2.AsList().ForEach(item => Helper.AssertPropertiesEquality(tables.First(e => e.Id == item.Id), item));
            result.Item3.AsList().ForEach(item => Helper.AssertPropertiesEquality(tables.First(e => e.Id == item.Id), item));
        }
    }

    [TestMethod]
    public async Task TestSqlServerConnectionQueryMultipleAsyncForT4()
    {
        // Setup
        var tables = Database.CreateCompleteTables(10);

        using (var connection = new SqlConnection(Database.ConnectionString))
        {
            // Act
            var result = await connection.QueryMultipleAsync<CompleteTable, CompleteTable, CompleteTable, CompleteTable>(e => e.Id > 0,
                e => e.Id > 0,
                e => e.Id > 0,
                e => e.Id > 0,
                top1: 1,
                top2: 2,
                top3: 3,
                top4: 4);

            // Assert
            Assert.AreEqual(1, result.Item1.Count());
            Assert.AreEqual(2, result.Item2.Count());
            Assert.AreEqual(3, result.Item3.Count());
            Assert.AreEqual(4, result.Item4.Count());
            result.Item1.AsList().ForEach(item => Helper.AssertPropertiesEquality(tables.First(e => e.Id == item.Id), item));
            result.Item2.AsList().ForEach(item => Helper.AssertPropertiesEquality(tables.First(e => e.Id == item.Id), item));
            result.Item3.AsList().ForEach(item => Helper.AssertPropertiesEquality(tables.First(e => e.Id == item.Id), item));
            result.Item4.AsList().ForEach(item => Helper.AssertPropertiesEquality(tables.First(e => e.Id == item.Id), item));
        }
    }

    [TestMethod]
    public async Task TestSqlServerConnectionQueryMultipleAsyncForT5()
    {
        // Setup
        var tables = Database.CreateCompleteTables(10);

        using (var connection = new SqlConnection(Database.ConnectionString))
        {
            // Act
            var result = await connection.QueryMultipleAsync<CompleteTable, CompleteTable, CompleteTable, CompleteTable, CompleteTable>(e => e.Id > 0,
                e => e.Id > 0,
                e => e.Id > 0,
                e => e.Id > 0,
                e => e.Id > 0,
                top1: 1,
                top2: 2,
                top3: 3,
                top4: 4,
                top5: 5);

            // Assert
            Assert.AreEqual(1, result.Item1.Count());
            Assert.AreEqual(2, result.Item2.Count());
            Assert.AreEqual(3, result.Item3.Count());
            Assert.AreEqual(4, result.Item4.Count());
            Assert.AreEqual(5, result.Item5.Count());
            result.Item1.AsList().ForEach(item => Helper.AssertPropertiesEquality(tables.First(e => e.Id == item.Id), item));
            result.Item2.AsList().ForEach(item => Helper.AssertPropertiesEquality(tables.First(e => e.Id == item.Id), item));
            result.Item3.AsList().ForEach(item => Helper.AssertPropertiesEquality(tables.First(e => e.Id == item.Id), item));
            result.Item4.AsList().ForEach(item => Helper.AssertPropertiesEquality(tables.First(e => e.Id == item.Id), item));
            result.Item5.AsList().ForEach(item => Helper.AssertPropertiesEquality(tables.First(e => e.Id == item.Id), item));
        }
    }

    [TestMethod]
    public async Task TestSqlServerConnectionQueryMultipleAsyncForT6()
    {
        // Setup
        var tables = Database.CreateCompleteTables(10);

        using (var connection = new SqlConnection(Database.ConnectionString))
        {
            // Act
            var result = await connection.QueryMultipleAsync<CompleteTable, CompleteTable, CompleteTable, CompleteTable, CompleteTable, CompleteTable>(e => e.Id > 0,
                e => e.Id > 0,
                e => e.Id > 0,
                e => e.Id > 0,
                e => e.Id > 0,
                e => e.Id > 0,
                top1: 1,
                top2: 2,
                top3: 3,
                top4: 4,
                top5: 5,
                top6: 6);

            // Assert
            Assert.AreEqual(1, result.Item1.Count());
            Assert.AreEqual(2, result.Item2.Count());
            Assert.AreEqual(3, result.Item3.Count());
            Assert.AreEqual(4, result.Item4.Count());
            Assert.AreEqual(5, result.Item5.Count());
            Assert.AreEqual(6, result.Item6.Count());
            result.Item1.AsList().ForEach(item => Helper.AssertPropertiesEquality(tables.First(e => e.Id == item.Id), item));
            result.Item2.AsList().ForEach(item => Helper.AssertPropertiesEquality(tables.First(e => e.Id == item.Id), item));
            result.Item3.AsList().ForEach(item => Helper.AssertPropertiesEquality(tables.First(e => e.Id == item.Id), item));
            result.Item4.AsList().ForEach(item => Helper.AssertPropertiesEquality(tables.First(e => e.Id == item.Id), item));
            result.Item5.AsList().ForEach(item => Helper.AssertPropertiesEquality(tables.First(e => e.Id == item.Id), item));
            result.Item6.AsList().ForEach(item => Helper.AssertPropertiesEquality(tables.First(e => e.Id == item.Id), item));
        }
    }

    [TestMethod]
    public async Task TestSqlServerConnectionQueryMultipleAsyncForT7()
    {
        // Setup
        var tables = Database.CreateCompleteTables(10);

        using (var connection = new SqlConnection(Database.ConnectionString))
        {
            // Act
            var result = await connection.QueryMultipleAsync<CompleteTable, CompleteTable, CompleteTable, CompleteTable, CompleteTable, CompleteTable, CompleteTable>(e => e.Id > 0,
                e => e.Id > 0,
                e => e.Id > 0,
                e => e.Id > 0,
                e => e.Id > 0,
                e => e.Id > 0,
                e => e.Id > 0,
                top1: 1,
                top2: 2,
                top3: 3,
                top4: 4,
                top5: 5,
                top6: 6,
                top7: 7);

            // Assert
            Assert.AreEqual(1, result.Item1.Count());
            Assert.AreEqual(2, result.Item2.Count());
            Assert.AreEqual(3, result.Item3.Count());
            Assert.AreEqual(4, result.Item4.Count());
            Assert.AreEqual(5, result.Item5.Count());
            Assert.AreEqual(6, result.Item6.Count());
            Assert.AreEqual(7, result.Item7.Count());
            result.Item1.AsList().ForEach(item => Helper.AssertPropertiesEquality(tables.First(e => e.Id == item.Id), item));
            result.Item2.AsList().ForEach(item => Helper.AssertPropertiesEquality(tables.First(e => e.Id == item.Id), item));
            result.Item3.AsList().ForEach(item => Helper.AssertPropertiesEquality(tables.First(e => e.Id == item.Id), item));
            result.Item4.AsList().ForEach(item => Helper.AssertPropertiesEquality(tables.First(e => e.Id == item.Id), item));
            result.Item5.AsList().ForEach(item => Helper.AssertPropertiesEquality(tables.First(e => e.Id == item.Id), item));
            result.Item6.AsList().ForEach(item => Helper.AssertPropertiesEquality(tables.First(e => e.Id == item.Id), item));
            result.Item7.AsList().ForEach(item => Helper.AssertPropertiesEquality(tables.First(e => e.Id == item.Id), item));
        }
    }

    [TestMethod]
    public async Task TestSqlServerConnectionQueryMultipleAsyncForT2WithHints()
    {
        // Setup
        var tables = Database.CreateCompleteTables(10);

        using (var connection = new SqlConnection(Database.ConnectionString))
        {
            // Act
            var result = await connection.QueryMultipleAsync<CompleteTable, CompleteTable>(e => e.Id > 0,
                e => e.Id > 0,
                top1: 1,
                top2: 2,
                hints1: SqlServerTableHints.NoLock,
                hints2: SqlServerTableHints.NoLock);

            // Assert
            Assert.AreEqual(1, result.Item1.Count());
            Assert.AreEqual(2, result.Item2.Count());
            result.Item1.AsList().ForEach(item => Helper.AssertPropertiesEquality(tables.First(e => e.Id == item.Id), item));
            result.Item2.AsList().ForEach(item => Helper.AssertPropertiesEquality(tables.First(e => e.Id == item.Id), item));
        }
    }

    #endregion

    #endregion
}
