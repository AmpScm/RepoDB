using Microsoft.VisualStudio.TestTools.UnitTesting;
using MySql.Data.MySqlClient;

namespace RepoDb.MySql.UnitTests
{
    [TestClass]
    public class MappingTest
    {
        [TestInitialize]
        public void Initialize()
        {
            MySqlBootstrap.Initialize();
        }

        [TestMethod]
        public void TestMySqlStatementBuilderMapper()
        {
            // Setup
            var builder = StatementBuilderMapper.Get<MySqlConnection>();

            // Assert
            Assert.IsNotNull(builder);
        }

        [TestMethod]
        public void TestMySqlDbHelperMapper()
        {
            // Setup
            var helper = DbHelperMapper.Get<MySqlConnection>();

            // Assert
            Assert.IsNotNull(helper);
        }

        [TestMethod]
        public void TestMySqlDbSettingMapper()
        {
            // Setup
            var setting = DbSettingMapper.Get<MySqlConnection>();

            // Assert
            Assert.IsNotNull(setting);
        }
    }
}
