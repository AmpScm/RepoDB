﻿using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RepoDb.IntegrationTests.Models;
using RepoDb.IntegrationTests.Setup;

namespace RepoDb.IntegrationTests.Operations;

[TestClass]
public class AverageAllTest
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

    #region AverageAll<TEntity>

    [TestMethod]
    public void TestSqlConnectionAverageAll()
    {
        // Setup
        var tables = Helper.CreateIdentityTables(10);

        using (var connection = new SqlConnection(Database.ConnectionStringForRepoDb))
        {
            // Act
            connection.InsertAll(tables);

            // Act
            var result = connection.AverageAll<IdentityTable>(e => e.ColumnInt);

            // Assert
            Assert.AreEqual(tables.Average(t => t.ColumnInt), Convert.ToDouble(result));
        }
    }

    [TestMethod]
    public void TestSqlConnectionAverageAllWithHints()
    {
        // Setup
        var tables = Helper.CreateIdentityTables(10);

        using (var connection = new SqlConnection(Database.ConnectionStringForRepoDb))
        {
            // Act
            connection.InsertAll(tables);

            // Act
            var result = connection.AverageAll<IdentityTable>(e => e.ColumnInt,
                hints: SqlServerTableHints.NoLock);

            // Assert
            Assert.AreEqual(tables.Average(t => t.ColumnInt), Convert.ToDouble(result));
        }
    }

    [TestMethod]
    public void TestSqlConnectionAverageAllTypedResult()
    {
        // Setup
        var tables = Helper.CreateIdentityTables(10);

        using (var connection = new SqlConnection(Database.ConnectionStringForRepoDb))
        {
            // Act
            connection.InsertAll(tables);

            // Act
            var result = connection.AverageAll<IdentityTable, double?>(e => e.ColumnInt);

            // Assert
            Assert.AreEqual(tables.Average(t => t.ColumnInt), result);
        }
    }

    [TestMethod]
    public void TestSqlConnectionAverageAllWithHintsTypedResult()
    {
        // Setup
        var tables = Helper.CreateIdentityTables(10);

        using (var connection = new SqlConnection(Database.ConnectionStringForRepoDb))
        {
            // Act
            connection.InsertAll(tables);

            // Act
            var result = connection.AverageAll<IdentityTable, double?>(e => e.ColumnInt,
                hints: SqlServerTableHints.NoLock);

            // Assert
            Assert.AreEqual(tables.Average(t => t.ColumnInt), result);
        }
    }

    #endregion

    #region AverageAllAsync<TEntity>

#if NET
    [TestMethod]
    public async Task TestSqlConnectionAverageAllAsync()
    {
        // Setup
        var tables = Helper.CreateIdentityTables(10);

        await using (var connection = new SqlConnection(Database.ConnectionStringForRepoDb))
        {
            // Act
            connection.InsertAll(tables);

            // Act
            var result = await connection.AverageAllAsync<IdentityTable>(e => e.ColumnInt);

            // Assert
            Assert.AreEqual(tables.Average(t => t.ColumnInt), Convert.ToDouble(result));
        }
    }
#endif

#if NET
    [TestMethod]
    public async Task TestSqlConnectionAverageAllAsyncWithHints()
    {
        // Setup
        var tables = Helper.CreateIdentityTables(10);

        await using (var connection = new SqlConnection(Database.ConnectionStringForRepoDb))
        {
            // Act
            connection.InsertAll(tables);

            // Act
            var result = await connection.AverageAllAsync<IdentityTable>(e => e.ColumnInt,
                hints: SqlServerTableHints.NoLock);

            // Assert
            Assert.AreEqual(tables.Average(t => t.ColumnInt), Convert.ToDouble(result));
        }
    }
#endif

    [TestMethod]
    public async Task TestSqlConnectionAverageAllAsyncTypedResult()
    {
        // Setup
        var tables = Helper.CreateIdentityTables(10);

        using (var connection = new SqlConnection(Database.ConnectionStringForRepoDb))
        {
            // Act
            connection.InsertAll(tables);

            // Act
            var result = await connection.AverageAllAsync<IdentityTable, double?>(e => e.ColumnInt);

            // Assert
            Assert.AreEqual(tables.Average(t => t.ColumnInt), result);
        }
    }

    [TestMethod]
    public async Task TestSqlConnectionAverageAllAsyncWithHintsTypedResult()
    {
        // Setup
        var tables = Helper.CreateIdentityTables(10);

        using (var connection = new SqlConnection(Database.ConnectionStringForRepoDb))
        {
            // Act
            connection.InsertAll(tables);

            // Act
            var result = await connection.AverageAllAsync<IdentityTable, double?>(e => e.ColumnInt,
                hints: SqlServerTableHints.NoLock);

            // Assert
            Assert.AreEqual(tables.Average(t => t.ColumnInt), result);
        }
    }

    #endregion

    #region AverageAll(TableName)

    [TestMethod]
    public void TestSqlConnectionAverageViaAllTableName()
    {
        // Setup
        var tables = Helper.CreateIdentityTables(10);

        using (var connection = new SqlConnection(Database.ConnectionStringForRepoDb))
        {
            // Act
            connection.InsertAll(tables);

            // Act
            var result = connection.AverageAll(ClassMappedNameCache.Get<IdentityTable>(),
                new Field("ColumnInt"));

            // Assert
            Assert.AreEqual(tables.Average(t => t.ColumnInt), Convert.ToDouble(result));
        }
    }

    [TestMethod]
    public void TestSqlConnectionAverageAllViaTableNameWithHints()
    {
        // Setup
        var tables = Helper.CreateIdentityTables(10);

        using (var connection = new SqlConnection(Database.ConnectionStringForRepoDb))
        {
            // Act
            connection.InsertAll(tables);

            // Act
            var result = connection.AverageAll(ClassMappedNameCache.Get<IdentityTable>(),
                new Field("ColumnInt"),
                hints: SqlServerTableHints.NoLock);

            // Assert
            Assert.AreEqual(tables.Average(t => t.ColumnInt), Convert.ToDouble(result));
        }
    }

    [TestMethod]
    public void TestSqlConnectionAverageAllTypedResultViaTableName()
    {
        // Setup
        var tables = Helper.CreateIdentityTables(10);

        using (var connection = new SqlConnection(Database.ConnectionStringForRepoDb))
        {
            // Act
            connection.InsertAll(tables);

            // Act
            var result = connection.AverageAll<double?>(ClassMappedNameCache.Get<IdentityTable>(),
                new Field("ColumnInt"));

            // Assert
            Assert.AreEqual(tables.Average(t => t.ColumnInt), result);
        }
    }

    [TestMethod]
    public void TestSqlConnectionAverageAllTypedResultViaTableNameWithHints()
    {
        // Setup
        var tables = Helper.CreateIdentityTables(10);

        using (var connection = new SqlConnection(Database.ConnectionStringForRepoDb))
        {
            // Act
            connection.InsertAll(tables);

            // Act
            var result = connection.AverageAll<double?>(ClassMappedNameCache.Get<IdentityTable>(),
                new Field("ColumnInt"),
                hints: SqlServerTableHints.NoLock);

            // Assert
            Assert.AreEqual(tables.Average(t => t.ColumnInt), result);
        }
    }

    #endregion

    #region AverageAllAsync(TableName)

    [TestMethod]
    public async Task TestSqlConnectionAverageAllAsyncViaTableName()
    {
        // Setup
        var tables = Helper.CreateIdentityTables(10);

        using (var connection = new SqlConnection(Database.ConnectionStringForRepoDb))
        {
            // Act
            connection.InsertAll(tables);

            // Act
            var result = await connection.AverageAllAsync(ClassMappedNameCache.Get<IdentityTable>(),
                new Field("ColumnInt"));

            // Assert
            Assert.AreEqual(tables.Average(t => t.ColumnInt), Convert.ToDouble(result));
        }
    }

    [TestMethod]
    public async Task TestSqlConnectionAverageAllAsyncViaTableNameWithHints()
    {
        // Setup
        var tables = Helper.CreateIdentityTables(10);

        using (var connection = new SqlConnection(Database.ConnectionStringForRepoDb))
        {
            // Act
            connection.InsertAll(tables);

            // Act
            var result = await connection.AverageAllAsync(ClassMappedNameCache.Get<IdentityTable>(),
                new Field("ColumnInt"),
                hints: SqlServerTableHints.NoLock);

            // Assert
            Assert.AreEqual(tables.Average(t => t.ColumnInt), Convert.ToDouble(result));
        }
    }

    [TestMethod]
    public async Task TestSqlConnectionAverageAllTypedResultAsyncViaTableName()
    {
        // Setup
        var tables = Helper.CreateIdentityTables(10);

        using (var connection = new SqlConnection(Database.ConnectionStringForRepoDb))
        {
            // Act
            connection.InsertAll(tables);

            // Act
            var result = await connection.AverageAllAsync<double?>(ClassMappedNameCache.Get<IdentityTable>(),
                new Field("ColumnInt"));

            // Assert
            Assert.AreEqual(tables.Average(t => t.ColumnInt), result);
        }
    }

    [TestMethod]
    public async Task TestSqlConnectionAverageAllTypedResultAsyncViaTableNameWithHints()
    {
        // Setup
        var tables = Helper.CreateIdentityTables(10);

        using (var connection = new SqlConnection(Database.ConnectionStringForRepoDb))
        {
            // Act
            connection.InsertAll(tables);

            // Act
            var result = await connection.AverageAllAsync<double?>(ClassMappedNameCache.Get<IdentityTable>(),
                new Field("ColumnInt"),
                hints: SqlServerTableHints.NoLock);

            // Assert
            Assert.AreEqual(tables.Average(t => t.ColumnInt), result);
        }
    }

    #endregion
}
