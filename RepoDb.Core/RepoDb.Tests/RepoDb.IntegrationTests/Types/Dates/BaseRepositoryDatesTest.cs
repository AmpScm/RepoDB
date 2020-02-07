﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using RepoDb.IntegrationTests.Models;
using RepoDb.IntegrationTests.Setup;
using System;
using Microsoft.Data.SqlClient;
using System.Linq;

namespace RepoDb.IntegrationTests.Types.Dates
{
    [TestClass]
    public class BaseRepositoryDatesTest
    {
        private class DatesClassRepository : BaseRepository<DatesClass, SqlConnection>
        {
            public DatesClassRepository(string connectionString) : base(connectionString, (int?)0) { }
        }

        private class DatesMapClassRepository : BaseRepository<DatesMapClass, SqlConnection>
        {
            public DatesMapClassRepository(string connectionString) : base(connectionString, (int?)0) { }
        }

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

        #region <TEntity>

        [TestMethod]
        public void TestBaseRepositoryDatesCrud()
        {
            // Setup
            var dateTime = new DateTime(1970, 1, 1, 12, 50, 30, DateTimeKind.Utc);
            var dateTime2 = dateTime.AddMilliseconds(100);
            var entity = new DatesClass
            {
                SessionId = Guid.NewGuid(),
                ColumnDate = dateTime.Date,
                ColumnDateTime = dateTime,
                ColumnDateTime2 = dateTime2,
                ColumnSmallDateTime = dateTime,
                ColumnDateTimeOffset = new DateTimeOffset(dateTime.Date).ToOffset(TimeSpan.FromHours(2)),
                ColumnTime = dateTime.TimeOfDay
            };

            using (var repository = new DatesClassRepository(Database.ConnectionStringForRepoDb))
            {
                // Act Insert
                var id = repository.Insert(entity);

                // Act Query
                var data = repository.Query(e => e.SessionId == (Guid)id).FirstOrDefault();

                // Assert
                Assert.IsNotNull(data);
                Assert.AreEqual(entity.ColumnDate, data.ColumnDate);
                Assert.AreEqual(entity.ColumnDateTime, data.ColumnDateTime);
                Assert.AreEqual(entity.ColumnDateTime2, data.ColumnDateTime2);
                Assert.AreEqual(dateTime.AddSeconds(30), data.ColumnSmallDateTime); // Always in a fraction of minutes, round (off/up)
                Assert.AreEqual(entity.ColumnDateTimeOffset, data.ColumnDateTimeOffset);
                Assert.AreEqual(entity.ColumnTime, data.ColumnTime);
            }
        }

        [TestMethod]
        public void TestBaseRepositoryDatesNullCrud()
        {
            // Setup
            var entity = new DatesClass
            {
                SessionId = Guid.NewGuid(),
                ColumnDate = null,
                ColumnDateTime = null,
                ColumnDateTime2 = null,
                ColumnSmallDateTime = null,
                ColumnDateTimeOffset = null,
                ColumnTime = null
            };

            using (var repository = new DatesClassRepository(Database.ConnectionStringForRepoDb))
            {
                // Act Insert
                var id = repository.Insert(entity);

                // Act Query
                var data = repository.Query(e => e.SessionId == (Guid)id).FirstOrDefault();

                // Assert
                Assert.IsNotNull(data);
                Assert.IsNull(data.ColumnDate);
                Assert.IsNull(data.ColumnDateTime);
                Assert.IsNull(data.ColumnDateTime2);
                Assert.IsNull(data.ColumnSmallDateTime);
                Assert.IsNull(data.ColumnDateTimeOffset);
                Assert.IsNull(data.ColumnTime);
            }
        }

        [TestMethod]
        public void TestBaseRepositoryDatesMappedCrud()
        {
            // Setup
            var dateTime = new DateTime(1970, 1, 1, 12, 50, 30, DateTimeKind.Utc);
            var dateTime2 = dateTime.AddMilliseconds(100);
            var entity = new DatesMapClass
            {
                SessionId = Guid.NewGuid(),
                ColumnDateMapped = dateTime.Date,
                ColumnDateTimeMapped = dateTime,
                ColumnDateTime2Mapped = dateTime2,
                ColumnSmallDateTimeMapped = dateTime,
                ColumnDateTimeOffsetMapped = new DateTimeOffset(dateTime.Date).ToOffset(TimeSpan.FromHours(2)),
                ColumnTimeMapped = dateTime.TimeOfDay
            };

            using (var repository = new DatesMapClassRepository(Database.ConnectionStringForRepoDb))
            {
                // Act Insert
                var id = repository.Insert(entity);

                // Act Query
                var data = repository.Query(e => e.SessionId == (Guid)id).FirstOrDefault();

                // Assert
                Assert.IsNotNull(data);
                Assert.AreEqual(entity.ColumnDateMapped, data.ColumnDateMapped);
                Assert.AreEqual(entity.ColumnDateTimeMapped, data.ColumnDateTimeMapped);
                Assert.AreEqual(entity.ColumnDateTime2Mapped, data.ColumnDateTime2Mapped);
                Assert.AreEqual(dateTime.AddSeconds(30), data.ColumnSmallDateTimeMapped); // Always in a fraction of minutes, round (off/up)
                Assert.AreEqual(entity.ColumnDateTimeOffsetMapped, data.ColumnDateTimeOffsetMapped);
                Assert.AreEqual(entity.ColumnTimeMapped, data.ColumnTimeMapped);
            }
        }

        [TestMethod]
        public void TestBaseRepositoryDatesMappedNullCrud()
        {
            // Setup
            var entity = new DatesMapClass
            {
                SessionId = Guid.NewGuid(),
                ColumnDateMapped = null,
                ColumnDateTimeMapped = null,
                ColumnDateTime2Mapped = null,
                ColumnSmallDateTimeMapped = null,
                ColumnDateTimeOffsetMapped = null,
                ColumnTimeMapped = null
            };

            using (var repository = new DatesMapClassRepository(Database.ConnectionStringForRepoDb))
            {
                // Act Insert
                var id = repository.Insert(entity);

                // Act Query
                var data = repository.Query(e => e.SessionId == (Guid)id).FirstOrDefault();

                // Assert
                Assert.IsNotNull(data);
                Assert.IsNull(data.ColumnDateMapped);
                Assert.IsNull(data.ColumnDateTimeMapped);
                Assert.IsNull(data.ColumnDateTime2Mapped);
                Assert.IsNull(data.ColumnSmallDateTimeMapped);
                Assert.IsNull(data.ColumnDateTimeOffsetMapped);
                Assert.IsNull(data.ColumnTimeMapped);
            }
        }

        [TestMethod]
        public void TestBaseRepositoryDatesCrudAsync()
        {
            // Setup
            var dateTime = new DateTime(1970, 1, 1, 12, 50, 30, DateTimeKind.Utc);
            var dateTime2 = dateTime.AddMilliseconds(100);
            var entity = new DatesClass
            {
                SessionId = Guid.NewGuid(),
                ColumnDate = dateTime.Date,
                ColumnDateTime = dateTime,
                ColumnDateTime2 = dateTime2,
                ColumnSmallDateTime = dateTime,
                ColumnDateTimeOffset = new DateTimeOffset(dateTime.Date).ToOffset(TimeSpan.FromHours(2)),
                ColumnTime = dateTime.TimeOfDay
            };

            using (var repository = new DatesClassRepository(Database.ConnectionStringForRepoDb))
            {
                // Act Insert
                var insertResult = repository.InsertAsync(entity);
                var id = insertResult.Result;

                // Act Query
                var queryResult = repository.QueryAsync(e => e.SessionId == (Guid)id);
                var data = queryResult.Result.FirstOrDefault();

                // Assert
                Assert.IsNotNull(data);
                Assert.AreEqual(entity.ColumnDate, data.ColumnDate);
                Assert.AreEqual(entity.ColumnDateTime, data.ColumnDateTime);
                Assert.AreEqual(entity.ColumnDateTime2, data.ColumnDateTime2);
                Assert.AreEqual(dateTime.AddSeconds(30), data.ColumnSmallDateTime); // Always in a fraction of minutes, round (off/up)
                Assert.AreEqual(entity.ColumnDateTimeOffset, data.ColumnDateTimeOffset);
                Assert.AreEqual(entity.ColumnTime, data.ColumnTime);
            }
        }

        [TestMethod]
        public void TestBaseRepositoryDatesNullCrudAsync()
        {
            // Setup
            var entity = new DatesClass
            {
                SessionId = Guid.NewGuid(),
                ColumnDate = null,
                ColumnDateTime = null,
                ColumnDateTime2 = null,
                ColumnSmallDateTime = null,
                ColumnDateTimeOffset = null,
                ColumnTime = null
            };

            using (var repository = new DatesClassRepository(Database.ConnectionStringForRepoDb))
            {
                // Act Insert
                var insertResult = repository.InsertAsync(entity);
                var id = insertResult.Result;

                // Act Query
                var queryResult = repository.QueryAsync(e => e.SessionId == (Guid)id);
                var data = queryResult.Result.FirstOrDefault();

                // Assert
                Assert.IsNotNull(data);
                Assert.IsNull(data.ColumnDate);
                Assert.IsNull(data.ColumnDateTime);
                Assert.IsNull(data.ColumnDateTime2);
                Assert.IsNull(data.ColumnSmallDateTime);
                Assert.IsNull(data.ColumnDateTimeOffset);
                Assert.IsNull(data.ColumnTime);
            }
        }

        [TestMethod]
        public void TestBaseRepositoryDatesMappedCrudAsync()
        {
            // Setup
            var dateTime = new DateTime(1970, 1, 1, 12, 50, 30, DateTimeKind.Utc);
            var dateTime2 = dateTime.AddMilliseconds(100);
            var entity = new DatesMapClass
            {
                SessionId = Guid.NewGuid(),
                ColumnDateMapped = dateTime.Date,
                ColumnDateTimeMapped = dateTime,
                ColumnDateTime2Mapped = dateTime2,
                ColumnSmallDateTimeMapped = dateTime,
                ColumnDateTimeOffsetMapped = new DateTimeOffset(dateTime.Date).ToOffset(TimeSpan.FromHours(2)),
                ColumnTimeMapped = dateTime.TimeOfDay
            };

            using (var repository = new DatesMapClassRepository(Database.ConnectionStringForRepoDb))
            {
                // Act Insert
                var insertResult = repository.InsertAsync(entity);
                var id = insertResult.Result;

                // Act Query
                var queryResult = repository.QueryAsync(e => e.SessionId == (Guid)id);
                var data = queryResult.Result.FirstOrDefault();

                // Assert
                Assert.IsNotNull(data);
                Assert.AreEqual(entity.ColumnDateMapped, data.ColumnDateMapped);
                Assert.AreEqual(entity.ColumnDateTimeMapped, data.ColumnDateTimeMapped);
                Assert.AreEqual(entity.ColumnDateTime2Mapped, data.ColumnDateTime2Mapped); Assert.AreEqual(dateTime.AddSeconds(30), data.ColumnSmallDateTimeMapped); // Always in a fraction of minutes, round (off/up)
                Assert.AreEqual(entity.ColumnDateTimeOffsetMapped, data.ColumnDateTimeOffsetMapped);
                Assert.AreEqual(entity.ColumnTimeMapped, data.ColumnTimeMapped);
            }
        }

        [TestMethod]
        public void TestBaseRepositoryDatesMappedNullCrudAsync()
        {
            // Setup
            var entity = new DatesMapClass
            {
                SessionId = Guid.NewGuid(),
                ColumnDateMapped = null,
                ColumnDateTimeMapped = null,
                ColumnDateTime2Mapped = null,
                ColumnSmallDateTimeMapped = null,
                ColumnDateTimeOffsetMapped = null,
                ColumnTimeMapped = null
            };

            using (var repository = new DatesMapClassRepository(Database.ConnectionStringForRepoDb))
            {
                // Act Insert
                var insertResult = repository.InsertAsync(entity);
                var id = insertResult.Result;

                // Act Query
                var queryResult = repository.QueryAsync(e => e.SessionId == (Guid)id);
                var data = queryResult.Result.FirstOrDefault();

                // Assert
                Assert.IsNotNull(data);
                Assert.IsNull(data.ColumnDateMapped);
                Assert.IsNull(data.ColumnDateTimeMapped);
                Assert.IsNull(data.ColumnDateTime2Mapped);
                Assert.IsNull(data.ColumnSmallDateTimeMapped);
                Assert.IsNull(data.ColumnDateTimeOffsetMapped);
                Assert.IsNull(data.ColumnTimeMapped);
            }
        }

        #endregion
    }
}
