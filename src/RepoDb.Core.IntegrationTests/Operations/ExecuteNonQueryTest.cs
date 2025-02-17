﻿using Microsoft.Data.SqlClient;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RepoDb.IntegrationTests.Models;
using RepoDb.IntegrationTests.Setup;
using System.Data;

namespace RepoDb.IntegrationTests.Operations;

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

    #region ExecuteNonQuery

    [TestMethod]
    public void TestSqlConnectionExecuteNonQueryWithNoAffectedTableRows()
    {
        using (var connection = new SqlConnection(Database.ConnectionStringForRepoDb))
        {
            // Act
            var result = connection.ExecuteNonQuery("SELECT * FROM (SELECT 1 * 100 AS Value) TMP;");

            // Assert
            Assert.AreEqual(-1, result);
        }
    }

    [TestMethod]
    public void TestSqlConnectionExecuteNonQueryDeleteSingle()
    {
        // Setup
        var tables = Helper.CreateIdentityTables(10);

        using (var connection = new SqlConnection(Database.ConnectionStringForRepoDb))
        {
            // Act
            connection.InsertAll(tables);

            // Act
            var result = connection.ExecuteNonQuery("DELETE FROM [sc].[IdentityTable] WHERE ColumnInt = 10;");

            // Assert
            Assert.AreEqual(1, result);
        }
    }

    [TestMethod]
    public void TestSqlConnectionExecuteNonQueryDeleteWithSingleParameter()
    {
        // Setup
        var tables = Helper.CreateIdentityTables(10);

        using (var connection = new SqlConnection(Database.ConnectionStringForRepoDb))
        {
            // Act
            connection.InsertAll(tables);

            // Act
            var result = connection.ExecuteNonQuery("DELETE FROM [sc].[IdentityTable] WHERE ColumnInt = @ColumnInt;",
                new { ColumnInt = 10 });

            // Assert
            Assert.AreEqual(1, result);
        }
    }

    [TestMethod]
    public void TestSqlConnectionExecuteNonQueryDeleteWithMultipleParameters()
    {
        // Setup
        var tables = Helper.CreateIdentityTables(10);

        using (var connection = new SqlConnection(Database.ConnectionStringForRepoDb))
        {
            // Act
            connection.InsertAll(tables);

            // Act
            var result = connection.ExecuteNonQuery("DELETE FROM [sc].[IdentityTable] WHERE ColumnInt = @ColumnInt AND ColumnBit = @ColumnBit;",
                new { ColumnInt = 10, ColumnBit = true });

            // Assert
            Assert.AreEqual(1, result);
        }
    }

    [TestMethod]
    public void TestSqlConnectionExecuteNonQueryDeleteAll()
    {
        // Setup
        var tables = Helper.CreateIdentityTables(10);

        using (var connection = new SqlConnection(Database.ConnectionStringForRepoDb))
        {
            // Act
            connection.InsertAll(tables);

            // Act
            var result = connection.ExecuteNonQuery("DELETE FROM [sc].[IdentityTable];");

            // Assert
            Assert.AreEqual(tables.Count, 10);
        }
    }

    [TestMethod]
    public void TestSqlConnectionExecuteNonQueryUpdateSingle()
    {
        // Setup
        var tables = Helper.CreateIdentityTables(10);

        using (var connection = new SqlConnection(Database.ConnectionStringForRepoDb))
        {
            // Act
            connection.InsertAll(tables);

            // Act
            var result = connection.ExecuteNonQuery("UPDATE [sc].[IdentityTable] SET ColumnInt = 100 WHERE ColumnInt = 10;");

            // Assert
            Assert.AreEqual(1, result);
        }
    }

    [TestMethod]
    public void TestSqlConnectionExecuteNonQueryUpdateWithSigleParameter()
    {
        // Setup
        var tables = Helper.CreateIdentityTables(10);

        using (var connection = new SqlConnection(Database.ConnectionStringForRepoDb))
        {
            // Act
            connection.InsertAll(tables);

            // Act
            var result = connection.ExecuteNonQuery("UPDATE [sc].[IdentityTable] SET ColumnInt = 100 WHERE ColumnInt = @ColumnInt;",
                new { ColumnInt = 10 });

            // Assert
            Assert.AreEqual(1, result);
        }
    }

    [TestMethod]
    public void TestSqlConnectionExecuteNonQueryUpdateWithMultipleParameters()
    {
        // Setup
        var tables = Helper.CreateIdentityTables(10);

        using (var connection = new SqlConnection(Database.ConnectionStringForRepoDb))
        {
            // Act
            connection.InsertAll(tables);

            // Act
            var result = connection.ExecuteNonQuery("UPDATE [sc].[IdentityTable] SET ColumnInt = 100 WHERE ColumnInt = @ColumnInt AND ColumnBit = @ColumnBit;",
                new { ColumnInt = 10, ColumnBit = true });

            // Assert
            Assert.AreEqual(1, result);
        }
    }

    [TestMethod]
    public void TestSqlConnectionExecuteNonQueryUpdateAll()
    {
        // Setup
        var tables = Helper.CreateIdentityTables(10);

        using (var connection = new SqlConnection(Database.ConnectionStringForRepoDb))
        {
            // Act
            connection.InsertAll(tables);

            // Act
            var result = connection.ExecuteNonQuery("UPDATE [sc].[IdentityTable] SET ColumnInt = 100;");

            // Assert
            Assert.AreEqual(tables.Count, result);
        }
    }

    [TestMethod]
    public void TestSqlConnectionExecuteNonQueryWithMultipleSqlStatementsWithoutParameter()
    {
        // Setup
        var tables = Helper.CreateIdentityTables(10);

        using (var connection = new SqlConnection(Database.ConnectionStringForRepoDb))
        {
            // Act
            connection.InsertAll(tables);

            // Act
            var result = connection.ExecuteNonQuery("UPDATE [sc].[IdentityTable] SET ColumnInt = 100 WHERE ColumnInt = 10;" +
                "UPDATE [sc].[IdentityTable] SET ColumnInt = 90 WHERE ColumnInt = 9;" +
                "DELETE FROM [sc].[IdentityTable] WHERE ColumnInt = 1;");

            // Assert
            Assert.AreEqual(3, result);
        }
    }

    [TestMethod]
    public void TestSqlConnectionExecuteNonQueryWithMultipleSqlStatementsWithParameters()
    {
        // Setup
        var tables = Helper.CreateIdentityTables(10);

        using (var connection = new SqlConnection(Database.ConnectionStringForRepoDb))
        {
            // Act
            connection.InsertAll(tables);

            // Act
            var result = connection.ExecuteNonQuery("UPDATE [sc].[IdentityTable] SET ColumnInt = 100 WHERE ColumnInt = @Value1;" +
                "UPDATE [sc].[IdentityTable] SET ColumnInt = 90 WHERE ColumnInt = @Value2;" +
                "DELETE FROM [sc].[IdentityTable] WHERE ColumnInt = @Value3;",
                new { Value1 = 10, Value2 = 9, Value3 = 1 });

            // Assert
            Assert.AreEqual(3, result);
        }
    }

    [TestMethod]
    public void TestSqlConnectionExecuteNonQueryByExecutingAStoredProcedureWithSingleParameter()
    {
        // Setup
        var tables = Helper.CreateIdentityTables(10);

        using (var connection = new SqlConnection(Database.ConnectionStringForRepoDb))
        {
            // Act
            connection.InsertAll(tables);

            // Act
            var result = connection.ExecuteNonQuery("[dbo].[sp_get_identity_table_by_id]",
                param: new { tables.Last().Id },
                commandType: CommandType.StoredProcedure);

            // Assert
            Assert.AreEqual(-1, result);
        }
    }

    [TestMethod]
    public void TestSqlConnectionExecuteNonQueryByExecutingAStoredProcedureWithMultipleParameters()
    {
        using (var connection = new SqlConnection(Database.ConnectionStringForRepoDb))
        {
            // Act
            var result = connection.ExecuteNonQuery("[dbo].[sp_multiply]",
                param: new { Value1 = 100, Value2 = 200 },
                commandType: CommandType.StoredProcedure);

            // Assert
            Assert.AreEqual(-1, result);
        }
    }

    [TestMethod]
    public void TestSqlConnectionExecuteNonQueryByExecutingAStoredProcedureWithMultipleParametersAndWithOuputParameter()
    {
        using (var connection = new SqlConnection(Database.ConnectionStringForRepoDb))
        {
            // Setup
            var output = new DirectionalQueryField("Output", ParameterDirection.Output, 16, DbType.Int32);
            var param = new[]
            {
                new QueryField("Value1", 100),
                new QueryField("Value2", 200),
                output
            };

            // Act
            var result = connection.ExecuteNonQuery("[dbo].[sp_multiply_with_output]",
                param: param,
                commandType: CommandType.StoredProcedure);

            // Assert
            Assert.AreEqual(-1, result);
            Assert.AreEqual(20000, output.Parameter.Value);
        }
    }

    [TestMethod]
    public void TestSqlConnectionExecuteNonQueryByExecutingAStoredProcedureWithMultipleOutputParameters()
    {
        using (var connection = new SqlConnection(Database.ConnectionStringForRepoDb))
        {
            // Setup
            var userId = new DirectionalQueryField("UserId", null, ParameterDirection.Output, 16);
            var serverName = new DirectionalQueryField("ServerName", null, ParameterDirection.Output, 256);
            var dateTimeUtc = new DirectionalQueryField("DateTimeUtc", null, ParameterDirection.Output, 16, DbType.DateTime2);
            var param = new[]
            {
                userId,
                serverName,
                dateTimeUtc
            };

            // Act
            var result = connection.ExecuteNonQuery("[dbo].[sp_get_server_info_with_output]",
                param: param,
                commandType: CommandType.StoredProcedure);

            // Assert
            Assert.AreEqual(1000, userId.GetValue<int>());
            Assert.AreEqual("ServerName", serverName.GetValue<string>());
            Assert.AreEqual(DateTime.Parse("1970-01-01 23:59:59.999"), dateTimeUtc.GetValue<DateTime>());
        }
    }

    [TestMethod, ExpectedException(typeof(SqlException))]
    public void ThrowExceptionOnTestSqlConnectionExecuteNonQueryIfTheParametersAreNotDefined()
    {
        using (var connection = new SqlConnection(Database.ConnectionStringForRepoDb))
        {
            // Act
            connection.ExecuteQuery<IdentityTable>("SELECT * FROM [sc].[IdentityTable] WHERE (Id = @Id);");
        }
    }

    [TestMethod, ExpectedException(typeof(SqlException))]
    public void ThrowExceptionOnTestSqlConnectionExecuteNonQueryIfThereAreSqlStatementProblems()
    {
        using (var connection = new SqlConnection(Database.ConnectionStringForRepoDb))
        {
            // Act
            connection.ExecuteQuery<IdentityTable>("SELECT FROM [sc].[IdentityTable] WHERE (Id = @Id);");
        }
    }

    #endregion

    #region ExecuteNonQueryAsync

    [TestMethod]
    public async Task TestSqlConnectionExecuteNonQueryAsyncWithNoAffectedTableRows()
    {
        using (var connection = new SqlConnection(Database.ConnectionStringForRepoDb))
        {
            // Act
            var result = await connection.ExecuteNonQueryAsync("SELECT * FROM (SELECT 1 * 100 AS Value) TMP;");

            // Assert
            Assert.AreEqual(-1, result);
        }
    }

    [TestMethod]
    public async Task TestSqlConnectionExecuteNonQueryAsyncDeleteSingle()
    {
        // Setup
        var tables = Helper.CreateIdentityTables(10);

        using (var connection = new SqlConnection(Database.ConnectionStringForRepoDb))
        {
            // Act
            connection.InsertAll(tables);

            // Act
            var result = await connection.ExecuteNonQueryAsync("DELETE FROM [sc].[IdentityTable] WHERE ColumnInt = 10;");

            // Assert
            Assert.AreEqual(1, result);
        }
    }

    [TestMethod]
    public async Task TestSqlConnectionExecuteNonQueryAsyncDeleteWithSingleParameter()
    {
        // Setup
        var tables = Helper.CreateIdentityTables(10);

        using (var connection = new SqlConnection(Database.ConnectionStringForRepoDb))
        {
            // Act
            connection.InsertAll(tables);

            // Act
            var result = await connection.ExecuteNonQueryAsync("DELETE FROM [sc].[IdentityTable] WHERE ColumnInt = @ColumnInt;",
                new { ColumnInt = 10 });

            // Assert
            Assert.AreEqual(1, result);
        }
    }

    [TestMethod]
    public async Task TestSqlConnectionExecuteNonQueryAsyncDeleteWithMultipleParameters()
    {
        // Setup
        var tables = Helper.CreateIdentityTables(10);

        using (var connection = new SqlConnection(Database.ConnectionStringForRepoDb))
        {
            // Act
            connection.InsertAll(tables);

            // Act
            var result = await connection.ExecuteNonQueryAsync("DELETE FROM [sc].[IdentityTable] WHERE ColumnInt = @ColumnInt AND ColumnBit = @ColumnBit;",
                new { ColumnInt = 10, ColumnBit = true });

            // Assert
            Assert.AreEqual(1, result);
        }
    }

    [TestMethod]
    public async Task TestSqlConnectionExecuteNonQueryAsyncDeleteAll()
    {
        // Setup
        var tables = Helper.CreateIdentityTables(10);

        using (var connection = new SqlConnection(Database.ConnectionStringForRepoDb))
        {
            // Act
            connection.InsertAll(tables);

            // Act
            var result = await connection.ExecuteNonQueryAsync("DELETE FROM [sc].[IdentityTable];");

            // Assert
            Assert.AreEqual(tables.Count, 10);
        }
    }

    [TestMethod]
    public async Task TestSqlConnectionExecuteNonQueryAsyncUpdateSingle()
    {
        // Setup
        var tables = Helper.CreateIdentityTables(10);

        using (var connection = new SqlConnection(Database.ConnectionStringForRepoDb))
        {
            // Act
            connection.InsertAll(tables);

            // Act
            var result = await connection.ExecuteNonQueryAsync("UPDATE [sc].[IdentityTable] SET ColumnInt = 100 WHERE ColumnInt = 10;");

            // Assert
            Assert.AreEqual(1, result);
        }
    }

    [TestMethod]
    public async Task TestSqlConnectionExecuteNonQueryAsyncUpdateWithSigleParameter()
    {
        // Setup
        var tables = Helper.CreateIdentityTables(10);

        using (var connection = new SqlConnection(Database.ConnectionStringForRepoDb))
        {
            // Act
            connection.InsertAll(tables);

            // Act
            var result = await connection.ExecuteNonQueryAsync("UPDATE [sc].[IdentityTable] SET ColumnInt = 100 WHERE ColumnInt = @ColumnInt;",
                new { ColumnInt = 10 });

            // Assert
            Assert.AreEqual(1, result);
        }
    }

    [TestMethod]
    public async Task TestSqlConnectionExecuteNonQueryAsyncUpdateWithMultipleParameters()
    {
        // Setup
        var tables = Helper.CreateIdentityTables(10);

        using (var connection = new SqlConnection(Database.ConnectionStringForRepoDb))
        {
            // Act
            connection.InsertAll(tables);

            // Act
            var result = await connection.ExecuteNonQueryAsync("UPDATE [sc].[IdentityTable] SET ColumnInt = 100 WHERE ColumnInt = @ColumnInt AND ColumnBit = @ColumnBit;",
                new { ColumnInt = 10, ColumnBit = true });

            // Assert
            Assert.AreEqual(1, result);
        }
    }

    [TestMethod]
    public async Task TestSqlConnectionExecuteNonQueryAsyncUpdateAll()
    {
        // Setup
        var tables = Helper.CreateIdentityTables(10);

        using (var connection = new SqlConnection(Database.ConnectionStringForRepoDb))
        {
            // Act
            connection.InsertAll(tables);

            // Act
            var result = await connection.ExecuteNonQueryAsync("UPDATE [sc].[IdentityTable] SET ColumnInt = 100;");

            // Assert
            Assert.AreEqual(tables.Count, result);
        }
    }

    [TestMethod]
    public async Task TestSqlConnectionExecuteNonQueryAsyncWithMultipleSqlStatementsWithoutParameter()
    {
        // Setup
        var tables = Helper.CreateIdentityTables(10);

        using (var connection = new SqlConnection(Database.ConnectionStringForRepoDb))
        {
            // Act
            connection.InsertAll(tables);

            // Act
            var result = await connection.ExecuteNonQueryAsync("UPDATE [sc].[IdentityTable] SET ColumnInt = 100 WHERE ColumnInt = 10;" +
                "UPDATE [sc].[IdentityTable] SET ColumnInt = 90 WHERE ColumnInt = 9;" +
                "DELETE FROM [sc].[IdentityTable] WHERE ColumnInt = 1;");

            // Assert
            Assert.AreEqual(3, result);
        }
    }

    [TestMethod]
    public async Task TestSqlConnectionExecuteNonQueryAsyncWithMultipleSqlStatementsWithParameters()
    {
        // Setup
        var tables = Helper.CreateIdentityTables(10);

        using (var connection = new SqlConnection(Database.ConnectionStringForRepoDb))
        {
            // Act
            connection.InsertAll(tables);

            // Act
            var result = await connection.ExecuteNonQueryAsync("UPDATE [sc].[IdentityTable] SET ColumnInt = 100 WHERE ColumnInt = @Value1;" +
                "UPDATE [sc].[IdentityTable] SET ColumnInt = 90 WHERE ColumnInt = @Value2;" +
                "DELETE FROM [sc].[IdentityTable] WHERE ColumnInt = @Value3;",
                new { Value1 = 10, Value2 = 9, Value3 = 1 });

            // Assert
            Assert.AreEqual(3, result);
        }
    }

    [TestMethod]
    public async Task TestSqlConnectionExecuteNonQueryAsyncByExecutingAStoredProcedureWithSingleParameter()
    {
        // Setup
        var tables = Helper.CreateIdentityTables(10);

        using (var connection = new SqlConnection(Database.ConnectionStringForRepoDb))
        {
            // Act
            connection.InsertAll(tables);

            // Act
            var result = await connection.ExecuteNonQueryAsync("[dbo].[sp_get_identity_table_by_id]",
                param: new { tables.Last().Id },
                commandType: CommandType.StoredProcedure);

            // Assert
            Assert.AreEqual(-1, result);
        }
    }

    [TestMethod]
    public async Task TestSqlConnectionExecuteNonQueryAsyncByExecutingAStoredProcedureWithMultipleParameters()
    {
        using (var connection = new SqlConnection(Database.ConnectionStringForRepoDb))
        {
            // Act
            var result = await connection.ExecuteNonQueryAsync("[dbo].[sp_multiply]",
                param: new { Value1 = 100, Value2 = 200 },
                commandType: CommandType.StoredProcedure);

            // Assert
            Assert.AreEqual(-1, result);
        }
    }

    [TestMethod]
    public async Task TestSqlConnectionExecuteNonQueryAsyncByExecutingAStoredProcedureWithMultipleParametersAndWithOuputParameter()
    {
        using (var connection = new SqlConnection(Database.ConnectionStringForRepoDb))
        {
            // Setup
            var output = new DirectionalQueryField("Output", ParameterDirection.Output, 16, DbType.Int32);
            var param = new[]
            {
                new QueryField("Value1", 100),
                new QueryField("Value2", 200),
                output
            };

            // Act
            var result = await connection.ExecuteNonQueryAsync("[dbo].[sp_multiply_with_output]",
                param: param,
                commandType: CommandType.StoredProcedure);

            // Assert
            Assert.AreEqual(-1, result);
            Assert.AreEqual(20000, output.Parameter.Value);
        }
    }

    [TestMethod, ExpectedException(typeof(SqlException))]
    public async Task ThrowExceptionOnTestSqlConnectionExecuteNonQueryAsyncIfTheParametersAreNotDefined()
    {
        using (var connection = new SqlConnection(Database.ConnectionStringForRepoDb))
        {
            // Act
            var result = await connection.ExecuteQueryAsync<IdentityTable>("SELECT * FROM [sc].[IdentityTable] WHERE (Id = @Id);");
        }
    }

    [TestMethod, ExpectedException(typeof(SqlException))]
    public async Task ThrowExceptionOnTestSqlConnectionExecuteNonQueryAsyncIfThereAreSqlStatementProblems()
    {
        using (var connection = new SqlConnection(Database.ConnectionStringForRepoDb))
        {
            // Act
            var result = await connection.ExecuteQueryAsync<IdentityTable>("SELECT FROM [sc].[IdentityTable] WHERE (Id = @Id);");
        }
    }

    #endregion
}
