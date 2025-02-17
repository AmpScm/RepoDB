﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using RepoDb.Attributes;
using RepoDb.Extensions;
using RepoDb.SQLite.System.IntegrationTests.Setup;
using System.Data.SQLite;

namespace RepoDb.SQLite.System.IntegrationTests;

[TestClass]
public class EnumTests
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

    #region Enumerations

    public enum Hands
    {
        Unidentified,
        Left,
        Right
    }

    #endregion

    #region SubClasses

    [Map("SdsCompleteTable")]
    public class PersonWithText
    {
        public int Id { get; set; }
        public Hands? ColumnText { get; set; }
    }

    [Map("SdsCompleteTable")]
    public class PersonWithInteger
    {
        public int Id { get; set; }
        public Hands? ColumnInteger { get; set; }
    }

    [Map("SdsCompleteTable")]
    public class PersonWithTextAsInteger
    {
        public int Id { get; set; }
        [TypeMap(global::System.Data.DbType.Int32)]
        public Hands? ColumnText { get; set; }
    }

    #endregion

    #region Helpers

    public IEnumerable<PersonWithText> GetPersonWithText(int count)
    {
        var random = new Random();
        for (var i = 0; i < count; i++)
        {
            var hand = random.Next(100) > 50 ? Hands.Right : Hands.Left;
            yield return new PersonWithText
            {
                Id = i,
                ColumnText = hand
            };
        }
    }

    public IEnumerable<PersonWithInteger> GetPersonWithInteger(int count)
    {
        var random = new Random();
        for (var i = 0; i < count; i++)
        {
            var hand = random.Next(100) > 50 ? Hands.Right : Hands.Left;
            yield return new PersonWithInteger
            {
                Id = i,
                ColumnInteger = hand
            };
        }
    }

    public IEnumerable<PersonWithTextAsInteger> GetPersonWithTextAsInteger(int count)
    {
        var random = new Random();
        for (var i = 0; i < count; i++)
        {
            var hand = random.Next(100) > 50 ? Hands.Right : Hands.Left;
            yield return new PersonWithTextAsInteger
            {
                Id = i,
                ColumnText = hand
            };
        }
    }

    #endregion

    [TestMethod]
    public void TestInsertAndQueryEnumAsTextAsNull()
    {
        using (var connection = new SQLiteConnection(Database.ConnectionString))
        {
            //  Create the table first
            Database.CreateSdsCompleteTable(connection);

            // Setup
            var person = GetPersonWithText(1).First();
            person.ColumnText = null;

            // Act
            connection.Insert(person);

            // Query
            var queryResult = connection.Query<PersonWithText>(person.Id).First();

            // Assert
            Assert.IsNull(queryResult.ColumnText);
        }
    }

    [TestMethod]
    public void TestInsertAndQueryEnumAsText()
    {
        using (var connection = new SQLiteConnection(Database.ConnectionString))
        {
            //  Create the table first
            Database.CreateSdsCompleteTable(connection);

            // Setup
            var person = GetPersonWithText(1).First();

            // Act
            connection.Insert(person);

            // Query
            var queryResult = connection.Query<PersonWithText>(person.Id).First();

            // Assert
            Assert.AreEqual(person.ColumnText, queryResult.ColumnText);
        }
    }

    [TestMethod]
    public void TestInsertAndQueryEnumAsTextByBatch()
    {
        using (var connection = new SQLiteConnection(Database.ConnectionString))
        {
            //  Create the table first
            Database.CreateSdsCompleteTable(connection);

            // Setup
            var people = GetPersonWithText(10).AsList();

            // Act
            connection.InsertAll(people);

            // Query
            var queryResult = connection.QueryAll<PersonWithText>().AsList();

            // Assert
            people.ForEach(p =>
            {
                var item = queryResult.First(e => e.Id == p.Id);
                Assert.AreEqual(p.ColumnText, item.ColumnText);
            });
        }
    }

    [TestMethod]
    public void TestInsertAndQueryEnumAsIntegerAsNull()
    {
        using (var connection = new SQLiteConnection(Database.ConnectionString))
        {
            //  Create the table first
            Database.CreateSdsCompleteTable(connection);

            // Setup
            var person = GetPersonWithInteger(1).First();
            person.ColumnInteger = null;

            // Act
            connection.Insert(person);

            // Query
            var queryResult = connection.Query<PersonWithInteger>(person.Id).First();

            // Assert
            Assert.IsNull(queryResult.ColumnInteger);
        }
    }

    [TestMethod]
    public void TestInsertAndQueryEnumAsInteger()
    {
        using (var connection = new SQLiteConnection(Database.ConnectionString))
        {
            //  Create the table first
            Database.CreateSdsCompleteTable(connection);

            // Setup
            var person = GetPersonWithInteger(1).First();

            // Act
            connection.Insert(person);

            // Query
            var queryResult = connection.Query<PersonWithInteger>(person.Id).First();

            // Assert
            Assert.AreEqual(person.ColumnInteger, queryResult.ColumnInteger);
        }
    }

    [TestMethod]
    public void TestInsertAndQueryEnumAsIntegerAsBatch()
    {
        using (var connection = new SQLiteConnection(Database.ConnectionString))
        {
            //  Create the table first
            Database.CreateSdsCompleteTable(connection);

            // Setup
            var people = GetPersonWithInteger(10).AsList();

            // Act
            connection.InsertAll(people);

            // Query
            var queryResult = connection.QueryAll<PersonWithInteger>().AsList();

            // Assert
            people.ForEach(p =>
            {
                var item = queryResult.First(e => e.Id == p.Id);
                Assert.AreEqual(p.ColumnInteger, item.ColumnInteger);
            });
        }
    }

    [TestMethod]
    public void TestInsertAndQueryEnumAsTextAsInt()
    {
        using (var connection = new SQLiteConnection(Database.ConnectionString))
        {
            //  Create the table first
            Database.CreateSdsCompleteTable(connection);

            // Setup
            var person = GetPersonWithTextAsInteger(1).First();

            // Act
            connection.Insert(person);

            // Query
            var queryResult = connection.Query<PersonWithTextAsInteger>(person.Id).First();

            // Assert
            Assert.AreEqual(person.ColumnText, queryResult.ColumnText);
        }
    }

    [TestMethod]
    public void TestInsertAndQueryEnumAsTextAsIntAsBatch()
    {
        using (var connection = new SQLiteConnection(Database.ConnectionString))
        {
            //  Create the table first
            Database.CreateSdsCompleteTable(connection);

            // Setup
            var people = GetPersonWithTextAsInteger(10).AsList();

            // Act
            connection.InsertAll(people);

            // Query
            var queryResult = connection.QueryAll<PersonWithTextAsInteger>().AsList();

            // Assert
            people.ForEach(p =>
            {
                var item = queryResult.First(e => e.Id == p.Id);
                Assert.AreEqual(p.ColumnText, item.ColumnText);
            });
        }
    }
}
