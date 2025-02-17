﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using MySqlConnector;
using RepoDb.MySqlConnector.IntegrationTests.Setup;

namespace RepoDb.MySqlConnector.IntegrationTests.Operations;

[TestClass]
public class ExecuteScalarTest
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
    public void TestMySqlConnectionExecuteScalar()
    {
        // Setup
        var tables = Database.CreateCompleteTables(10);

        using (var connection = new MySqlConnection(Database.ConnectionString))
        {
            // Act
            var result = connection.ExecuteScalar("SELECT COUNT(*) FROM `CompleteTable`;");

            // Assert
            Assert.AreEqual(tables.Count(), Convert.ToInt32(result));
        }
    }

    [TestMethod]
    public void TestMySqlConnectionExecuteScalarWithReturnType()
    {
        // Setup
        var tables = Database.CreateCompleteTables(10);

        using (var connection = new MySqlConnection(Database.ConnectionString))
        {
            // Act
            var result = connection.ExecuteScalar<int>("SELECT COUNT(*) FROM `CompleteTable`;");

            // Assert
            Assert.AreEqual(tables.Count(), result);
        }
    }

    #endregion

    #region Async

    [TestMethod]
    public async Task TestMySqlConnectionExecuteScalarAsync()
    {
        // Setup
        var tables = Database.CreateCompleteTables(10);

        using (var connection = new MySqlConnection(Database.ConnectionString))
        {
            // Act
            var result = await connection.ExecuteScalarAsync("SELECT COUNT(*) FROM `CompleteTable`;");

            // Assert
            Assert.AreEqual(tables.Count(), Convert.ToInt32(result));
        }
    }

    [TestMethod]
    public async Task TestMySqlConnectionExecuteScalarAsyncWithReturnType()
    {
        // Setup
        var tables = Database.CreateCompleteTables(10);

        using (var connection = new MySqlConnection(Database.ConnectionString))
        {
            // Act
            var result = await connection.ExecuteScalarAsync<int>("SELECT COUNT(*) FROM `CompleteTable`;");

            // Assert
            Assert.AreEqual(tables.Count(), result);
        }
    }

    #endregion
}
