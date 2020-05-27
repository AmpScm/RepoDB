﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using MySql.Data.MySqlClient;
using RepoDb.MySql.IntegrationTests.Setup;
using System.Linq;

namespace RepoDb.MySql.IntegrationTests.Operations
{
    [TestClass]
    public class ExecuteNonQueryTest
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
        public void TestMySqlConnectionExecuteNonQuery()
        {
            // Setup
            var tables = Database.CreateCompleteTables(10);

            using (var connection = new MySqlConnection(Database.ConnectionString))
            {
                // Act
                var result = connection.ExecuteNonQuery("DELETE FROM `CompleteTable`;");

                // Assert
                Assert.AreEqual(tables.Count(), result);
            }
        }

        [TestMethod]
        public void TestMySqlConnectionExecuteNonQueryWithParameters()
        {
            // Setup
            var tables = Database.CreateCompleteTables(10);

            using (var connection = new MySqlConnection(Database.ConnectionString))
            {
                // Act
                var result = connection.ExecuteNonQuery("DELETE FROM `CompleteTable` WHERE Id = @Id;",
                    new { tables.Last().Id });

                // Assert
                Assert.AreEqual(1, result);
            }
        }

        [TestMethod]
        public void TestMySqlConnectionExecuteNonQueryWithMultipleStatement()
        {
            // Setup
            var tables = Database.CreateCompleteTables(10);

            using (var connection = new MySqlConnection(Database.ConnectionString))
            {
                // Act
                var result = connection.ExecuteNonQuery("DELETE FROM `CompleteTable`; DELETE FROM `CompleteTable`;");

                // Assert
                Assert.AreEqual(tables.Count(), result);
            }
        }

        #endregion

        #region Async

        [TestMethod]
        public void TestMySqlConnectionExecuteNonQueryAsync()
        {
            // Setup
            var tables = Database.CreateCompleteTables(10);

            using (var connection = new MySqlConnection(Database.ConnectionString))
            {
                // Act
                var result = connection.ExecuteNonQueryAsync("DELETE FROM `CompleteTable`;").Result;

                // Assert
                Assert.AreEqual(tables.Count(), result);
            }
        }

        [TestMethod]
        public void TestMySqlConnectionExecuteNonQueryAsyncWithParameters()
        {
            // Setup
            var tables = Database.CreateCompleteTables(10);

            using (var connection = new MySqlConnection(Database.ConnectionString))
            {
                // Act
                var result = connection.ExecuteNonQueryAsync("DELETE FROM `CompleteTable` WHERE Id = @Id;",
                    new { tables.Last().Id }).Result;

                // Assert
                Assert.AreEqual(1, result);
            }
        }

        [TestMethod]
        public void TestMySqlConnectionExecuteNonQueryAsyncWithMultipleStatement()
        {
            // Setup
            var tables = Database.CreateCompleteTables(10);

            using (var connection = new MySqlConnection(Database.ConnectionString))
            {
                // Act
                var result = connection.ExecuteNonQueryAsync("DELETE FROM `CompleteTable`; DELETE FROM `CompleteTable`;").Result;

                // Assert
                Assert.AreEqual(tables.Count(), result);
            }
        }

        #endregion
    }
}
