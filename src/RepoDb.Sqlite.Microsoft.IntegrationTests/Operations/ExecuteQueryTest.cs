﻿using Microsoft.Data.Sqlite;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RepoDb.Extensions;
using RepoDb.Sqlite.Microsoft.IntegrationTests.Models;
using RepoDb.Sqlite.Microsoft.IntegrationTests.Setup;

namespace RepoDb.Sqlite.Microsoft.IntegrationTests.Operations.MDS;

[TestClass]
public class ExecuteQueryTest
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

    #region Sync

    [TestMethod]
    public void TestSqLiteConnectionExecuteQuery()
    {
        using (var connection = new SqliteConnection(Database.ConnectionString))
        {
            // Setup
            var tables = Database.CreateMdsCompleteTables(10, connection);

            // Act
            var result = connection.ExecuteQuery<MdsCompleteTable>("SELECT * FROM [MdsCompleteTable];");

            // Assert
            Assert.AreEqual(tables.Count(), result.Count());
            tables.AsList().ForEach(table => Helper.AssertPropertiesEquality(table, result.First(e => e.Id == table.Id)));
        }
    }

    [TestMethod]
    public void TestSqLiteConnectionExecuteQueryWithParameters()
    {
        using (var connection = new SqliteConnection(Database.ConnectionString))
        {
            // Setup
            var tables = Database.CreateMdsCompleteTables(10, connection);

            // Act
            var result = connection.ExecuteQuery<MdsCompleteTable>("SELECT * FROM [MdsCompleteTable] WHERE Id = @Id;",
                new { tables.Last().Id });

            // Assert
            Assert.AreEqual(1, result.Count());
            Helper.AssertPropertiesEquality(tables.Last(), result.First());
        }
    }

    #endregion

    #region Async

    [TestMethod]
    public async Task TestSqLiteConnectionExecuteQueryAsync()
    {
        using (var connection = new SqliteConnection(Database.ConnectionString))
        {
            // Setup
            var tables = Database.CreateMdsCompleteTables(10, connection);

            // Act
            var result = await connection.ExecuteQueryAsync<MdsCompleteTable>("SELECT * FROM [MdsCompleteTable];");

            // Assert
            Assert.AreEqual(tables.Count(), result.Count());
            tables.AsList().ForEach(table => Helper.AssertPropertiesEquality(table, result.First(e => e.Id == table.Id)));
        }
    }

    [TestMethod]
    public async Task TestSqLiteConnectionExecuteQueryAsyncWithParameters()
    {
        using (var connection = new SqliteConnection(Database.ConnectionString))
        {
            // Setup
            var tables = Database.CreateMdsCompleteTables(10, connection);

            // Act
            var result = await connection.ExecuteQueryAsync<MdsCompleteTable>("SELECT * FROM [MdsCompleteTable] WHERE Id = @Id;",
                new { tables.Last().Id });

            // Assert
            Assert.AreEqual(1, result.Count());
            Helper.AssertPropertiesEquality(tables.Last(), result.First());
        }
    }

    #endregion
}
