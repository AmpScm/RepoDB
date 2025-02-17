﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using MySql.Data.MySqlClient;
using RepoDb.Extensions;
using RepoDb.MySql.IntegrationTests.Models;
using RepoDb.MySql.IntegrationTests.Setup;
using System.Linq;
using System.Threading.Tasks;

namespace RepoDb.MySql.IntegrationTests.Operations;

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
    public void TestMySqlConnectionExecuteQuery()
    {
        // Setup
        var tables = Database.CreateCompleteTables(10);

        using (var connection = new MySqlConnection(Database.ConnectionString))
        {
            // Act
            var result = connection.ExecuteQuery<CompleteTable>("SELECT * FROM `CompleteTable`;");

            // Assert
            Assert.AreEqual(tables.Count(), result.Count());
            tables.AsList().ForEach(table => Helper.AssertPropertiesEquality(table, result.First(e => e.Id == table.Id)));
        }
    }

    [TestMethod]
    public void TestMySqlConnectionExecuteQueryWithParameters()
    {
        // Setup
        var tables = Database.CreateCompleteTables(10);

        using (var connection = new MySqlConnection(Database.ConnectionString))
        {
            // Act
            var result = connection.ExecuteQuery<CompleteTable>("SELECT * FROM `CompleteTable` WHERE Id = @Id;",
                new { tables.Last().Id });

            // Assert
            Assert.AreEqual(1, result.Count());
            Helper.AssertPropertiesEquality(tables.Last(), result.First());
        }
    }

    #endregion

    #region Async

    [TestMethod]
    public async Task TestMySqlConnectionExecuteQueryAsync()
    {
        // Setup
        var tables = Database.CreateCompleteTables(10);

        using (var connection = new MySqlConnection(Database.ConnectionString))
        {
            // Act
            var result = await connection.ExecuteQueryAsync<CompleteTable>("SELECT * FROM `CompleteTable`;");

            // Assert
            Assert.AreEqual(tables.Count(), result.Count());
            tables.AsList().ForEach(table => Helper.AssertPropertiesEquality(table, result.First(e => e.Id == table.Id)));
        }
    }

    [TestMethod]
    public async Task TestMySqlConnectionExecuteQueryAsyncWithParameters()
    {
        // Setup
        var tables = Database.CreateCompleteTables(10);

        using (var connection = new MySqlConnection(Database.ConnectionString))
        {
            // Act
            var result = await connection.ExecuteQueryAsync<CompleteTable>("SELECT * FROM `CompleteTable` WHERE Id = @Id;",
                new { tables.Last().Id });

            // Assert
            Assert.AreEqual(1, result.Count());
            Helper.AssertPropertiesEquality(tables.Last(), result.First());
        }
    }

    #endregion
}
