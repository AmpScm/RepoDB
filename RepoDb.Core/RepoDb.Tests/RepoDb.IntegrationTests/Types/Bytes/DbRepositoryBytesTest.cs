﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using RepoDb.IntegrationTests.Models;
using RepoDb.IntegrationTests.Setup;
using System;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Text;

namespace RepoDb.IntegrationTests.Types.Bytes
{
    [TestClass]
    public class DbRepositoryBytesTest
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

        #region <TEntity>

        [TestMethod]
        public void TestDbRepositoryBytesCrud()
        {
            // Setup
            var text = "RepoDb"; // Helper.GetAssemblyDescription();
            var bytes = Encoding.UTF8.GetBytes(text);
            var entity = new BytesClass
            {
                SessionId = Guid.NewGuid(),
                ColumnBinary = bytes,
                ColumnImage = bytes,
                ColumnVarBinary = bytes,
                ColumnTinyInt = 128
            };

            using (var repository = new DbRepository<SqlConnection>(Database.ConnectionStringForRepoDb))
            {
                // Act Insert
                var id = repository.Insert(entity);

                // Act Query
                var data = repository.Query<BytesClass>(e => e.SessionId == (Guid)id).FirstOrDefault();

                // Assert
                Assert.IsNotNull(data);
                Assert.AreEqual(text, Encoding.UTF8.GetString(data.ColumnBinary.Take(entity.ColumnBinary.Length).ToArray()));
                Assert.AreEqual(text, Encoding.UTF8.GetString(data.ColumnImage));
                Assert.AreEqual(text, Encoding.UTF8.GetString(data.ColumnVarBinary));
                Assert.AreEqual(entity.ColumnTinyInt, data.ColumnTinyInt);
            }
        }

        [TestMethod]
        public void TestDbRepositoryBytesNullCrud()
        {
            // Setup
            var entity = new BytesClass
            {
                SessionId = Guid.NewGuid(),
                ColumnBinary = null,
                ColumnImage = null,
                ColumnTinyInt = null,
                ColumnVarBinary = null
            };

            using (var repository = new DbRepository<SqlConnection>(Database.ConnectionStringForRepoDb))
            {
                // Act Insert
                var id = repository.Insert(entity);

                // Act Query
                var data = repository.Query<BytesClass>(e => e.SessionId == (Guid)id).FirstOrDefault();

                // Assert
                Assert.IsNotNull(data);
                Assert.IsNull(data.ColumnBinary);
                Assert.IsNull(data.ColumnImage);
                Assert.IsNull(data.ColumnTinyInt);
                Assert.IsNull(data.ColumnVarBinary);
            }
        }

        [TestMethod]
        public void TestDbRepositoryBytesMappedCrud()
        {
            // Setup
            var text = "RepoDb"; // Helper.GetAssemblyDescription();
            var bytes = Encoding.UTF8.GetBytes(text);
            var entity = new BytesMapClass
            {
                SessionId = Guid.NewGuid(),
                ColumnBinaryMapped = bytes,
                ColumnImageMapped = bytes,
                ColumnVarBinaryMapped = bytes,
                ColumnTinyIntMapped = 128
            };

            using (var repository = new DbRepository<SqlConnection>(Database.ConnectionStringForRepoDb))
            {
                // Act Insert
                var id = repository.Insert(entity);

                // Act Query
                var data = repository.Query<BytesMapClass>(e => e.SessionId == (Guid)id).FirstOrDefault();

                // Assert
                Assert.IsNotNull(data);
                Assert.AreEqual(text, Encoding.UTF8.GetString(data.ColumnBinaryMapped.Take(entity.ColumnBinaryMapped.Length).ToArray()));
                Assert.AreEqual(text, Encoding.UTF8.GetString(data.ColumnImageMapped));
                Assert.AreEqual(text, Encoding.UTF8.GetString(data.ColumnVarBinaryMapped));
                Assert.AreEqual(entity.ColumnTinyIntMapped, data.ColumnTinyIntMapped);
            }
        }

        [TestMethod]
        public void TestDbRepositoryBytesMappedNullCrud()
        {
            // Setup
            var entity = new BytesMapClass
            {
                SessionId = Guid.NewGuid(),
                ColumnBinaryMapped = null,
                ColumnImageMapped = null,
                ColumnTinyIntMapped = null,
                ColumnVarBinaryMapped = null
            };

            using (var repository = new DbRepository<SqlConnection>(Database.ConnectionStringForRepoDb))
            {
                // Act Insert
                var id = repository.Insert(entity);

                // Act Query
                var data = repository.Query<BytesMapClass>(e => e.SessionId == (Guid)id).FirstOrDefault();

                // Assert
                Assert.IsNotNull(data);
                Assert.IsNull(data.ColumnBinaryMapped);
                Assert.IsNull(data.ColumnImageMapped);
                Assert.IsNull(data.ColumnTinyIntMapped);
                Assert.IsNull(data.ColumnVarBinaryMapped);
            }
        }

        [TestMethod]
        public void TestDbRepositoryBytesCrudAsync()
        {
            // Setup
            var text = "RepoDb"; // Helper.GetAssemblyDescription();
            var bytes = Encoding.UTF8.GetBytes(text);
            var entity = new BytesClass
            {
                SessionId = Guid.NewGuid(),
                ColumnBinary = bytes,
                ColumnImage = bytes,
                ColumnVarBinary = bytes,
                ColumnTinyInt = 128
            };

            using (var repository = new DbRepository<SqlConnection>(Database.ConnectionStringForRepoDb))
            {
                // Act Insert
                var insertResult = repository.InsertAsync(entity);
                var id = insertResult.Result;

                // Act Query
                var queryResult = repository.QueryAsync<BytesClass>(e => e.SessionId == (Guid)id);
                var data = queryResult.Result.FirstOrDefault();

                // Assert
                Assert.IsNotNull(data);
                Assert.AreEqual(text, Encoding.UTF8.GetString(data.ColumnBinary.Take(entity.ColumnBinary.Length).ToArray()));
                Assert.AreEqual(text, Encoding.UTF8.GetString(data.ColumnImage));
                Assert.AreEqual(text, Encoding.UTF8.GetString(data.ColumnVarBinary));
                Assert.AreEqual(entity.ColumnTinyInt, data.ColumnTinyInt);
            }
        }

        [TestMethod]
        public void TestDbRepositoryBytesNullCrudAsync()
        {
            // Setup
            var entity = new BytesClass
            {
                SessionId = Guid.NewGuid(),
                ColumnBinary = null,
                ColumnImage = null,
                ColumnTinyInt = null,
                ColumnVarBinary = null
            };

            using (var repository = new DbRepository<SqlConnection>(Database.ConnectionStringForRepoDb))
            {
                // Act Insert
                var insertResult = repository.InsertAsync(entity);
                var id = insertResult.Result;

                // Act Query
                var queryResult = repository.QueryAsync<BytesClass>(e => e.SessionId == (Guid)id);
                var data = queryResult.Result.FirstOrDefault();

                // Assert
                Assert.IsNotNull(data);
                Assert.IsNull(data.ColumnBinary);
                Assert.IsNull(data.ColumnImage);
                Assert.IsNull(data.ColumnTinyInt);
                Assert.IsNull(data.ColumnVarBinary);
            }
        }

        [TestMethod]
        public void TestDbRepositoryBytesMappedCrudAsync()
        {
            // Setup
            var text = "RepoDb"; // Helper.GetAssemblyDescription();
            var bytes = Encoding.UTF8.GetBytes(text);
            var entity = new BytesMapClass
            {
                SessionId = Guid.NewGuid(),
                ColumnBinaryMapped = bytes,
                ColumnImageMapped = bytes,
                ColumnVarBinaryMapped = bytes,
                ColumnTinyIntMapped = 128
            };

            using (var repository = new DbRepository<SqlConnection>(Database.ConnectionStringForRepoDb))
            {
                // Act Insert
                var insertResult = repository.InsertAsync(entity);
                var id = insertResult.Result;

                // Act Query
                var queryResult = repository.QueryAsync<BytesMapClass>(e => e.SessionId == (Guid)id);
                var data = queryResult.Result.FirstOrDefault();

                // Assert
                Assert.IsNotNull(data);
                Assert.AreEqual(text, Encoding.UTF8.GetString(data.ColumnBinaryMapped.Take(entity.ColumnBinaryMapped.Length).ToArray()));
                Assert.AreEqual(text, Encoding.UTF8.GetString(data.ColumnImageMapped));
                Assert.AreEqual(text, Encoding.UTF8.GetString(data.ColumnVarBinaryMapped));
                Assert.AreEqual(entity.ColumnTinyIntMapped, data.ColumnTinyIntMapped);
            }
        }

        [TestMethod]
        public void TestDbRepositoryBytesMappedNullCrudAsync()
        {
            // Setup
            var entity = new BytesMapClass
            {
                SessionId = Guid.NewGuid(),
                ColumnBinaryMapped = null,
                ColumnImageMapped = null,
                ColumnTinyIntMapped = null,
                ColumnVarBinaryMapped = null
            };

            using (var repository = new DbRepository<SqlConnection>(Database.ConnectionStringForRepoDb))
            {
                // Act Insert
                var insertResult = repository.InsertAsync(entity);
                var id = insertResult.Result;

                // Act Query
                var queryResult = repository.QueryAsync<BytesMapClass>(e => e.SessionId == (Guid)id);
                var data = queryResult.Result.FirstOrDefault();

                // Assert
                Assert.IsNotNull(data);
                Assert.IsNull(data.ColumnBinaryMapped);
                Assert.IsNull(data.ColumnImageMapped);
                Assert.IsNull(data.ColumnTinyIntMapped);
                Assert.IsNull(data.ColumnVarBinaryMapped);
            }
        }

        #endregion

        #region (TableName)

        [TestMethod]
        public void TestDbRepositoryBytesCrudViaTableName()
        {
            // Setup
            var text = "RepoDb"; // Helper.GetAssemblyDescription();
            var bytes = Encoding.UTF8.GetBytes(text);
            var entity = new BytesClass
            {
                SessionId = Guid.NewGuid(),
                ColumnBinary = bytes,
                ColumnImage = bytes,
                ColumnVarBinary = bytes,
                ColumnTinyInt = 128
            };

            using (var repository = new DbRepository<SqlConnection>(Database.ConnectionStringForRepoDb))
            {
                // Act Insert
                var id = repository.Insert(entity);

                // Act Query
                var data = repository.Query(ClassMappedNameCache.Get<BytesClass>(), new { SessionId = (Guid)id }).FirstOrDefault();

                // Assert
                Assert.IsNotNull(data);
                Assert.AreEqual(text, Encoding.UTF8.GetString(((byte[])data.ColumnBinary).Take(entity.ColumnBinary.Length).ToArray()));
                Assert.AreEqual(text, Encoding.UTF8.GetString(data.ColumnImage));
                Assert.AreEqual(text, Encoding.UTF8.GetString(data.ColumnVarBinary));
                Assert.AreEqual(entity.ColumnTinyInt, data.ColumnTinyInt);
            }
        }

        [TestMethod]
        public void TestDbRepositoryBytesNullCrudViaTableName()
        {
            // Setup
            var entity = new
            {
                SessionId = Guid.NewGuid(),
                ColumnBinary = (byte[])null,
                ColumnImage = (byte[])null,
                ColumnTinyInt = (byte?)null,
                ColumnVarBinary = (byte[])null
            };

            using (var repository = new DbRepository<SqlConnection>(Database.ConnectionStringForRepoDb))
            {
                // Act Insert
                var id = repository.Insert(ClassMappedNameCache.Get<BytesClass>(), entity);

                // Act Query
                var data = repository.Query(ClassMappedNameCache.Get<BytesClass>(), new { SessionId = (Guid)id }).FirstOrDefault();

                // Assert
                Assert.IsNotNull(data);
                Assert.IsNull(data.ColumnBinary);
                Assert.IsNull(data.ColumnImage);
                Assert.IsNull(data.ColumnTinyInt);
                Assert.IsNull(data.ColumnVarBinary);
            }
        }

        [TestMethod]
        public void TestDbRepositoryBytesCrudViaTableNameAsync()
        {
            // Setup
            var text = "RepoDb"; // Helper.GetAssemblyDescription();
            var bytes = Encoding.UTF8.GetBytes(text);
            var entity = new
            {
                SessionId = Guid.NewGuid(),
                ColumnBinary = bytes,
                ColumnImage = bytes,
                ColumnVarBinary = bytes,
                ColumnTinyInt = 128
            };

            using (var repository = new DbRepository<SqlConnection>(Database.ConnectionStringForRepoDb))
            {
                // Act Insert
                var insertResult = repository.InsertAsync(ClassMappedNameCache.Get<BytesClass>(), entity);
                var id = insertResult.Result;

                // Act Query
                var queryResult = repository.QueryAsync(ClassMappedNameCache.Get<BytesClass>(), new { SessionId = (Guid)id });
                var data = queryResult.Result.FirstOrDefault();

                // Assert
                Assert.IsNotNull(data);
                Assert.AreEqual(text, Encoding.UTF8.GetString(((byte[])data.ColumnBinary).Take(entity.ColumnBinary.Length).ToArray()));
                Assert.AreEqual(text, Encoding.UTF8.GetString(data.ColumnImage));
                Assert.AreEqual(text, Encoding.UTF8.GetString(data.ColumnVarBinary));
                Assert.AreEqual(entity.ColumnTinyInt, data.ColumnTinyInt);
            }
        }

        [TestMethod]
        public void TestDbRepositoryBytesNullCrudViaTableNameAsync()
        {
            // Setup
            var entity = new
            {
                SessionId = Guid.NewGuid(),
                ColumnBinary = (byte[])null,
                ColumnImage = (byte[])null,
                ColumnTinyInt = (byte?)null,
                ColumnVarBinary = (byte[])null
            };

            using (var repository = new DbRepository<SqlConnection>(Database.ConnectionStringForRepoDb))
            {
                // Act Insert
                var insertResult = repository.InsertAsync(ClassMappedNameCache.Get<BytesClass>(), entity);
                var id = insertResult.Result;

                // Act Query
                var queryResult = repository.QueryAsync(ClassMappedNameCache.Get<BytesClass>(), new { SessionId = (Guid)id });
                var data = queryResult.Result.FirstOrDefault();

                // Assert
                Assert.IsNotNull(data);
                Assert.IsNull(data.ColumnBinary);
                Assert.IsNull(data.ColumnImage);
                Assert.IsNull(data.ColumnTinyInt);
                Assert.IsNull(data.ColumnVarBinary);
            }
        }

        #endregion
    }
}
