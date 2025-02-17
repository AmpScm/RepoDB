using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data.SQLite;

namespace RepoDb.SQLite.System.UnitTests;

[TestClass]
public class MappingTest
{
    [TestInitialize]
    public void Initialize()
    {
        GlobalConfiguration
            .Setup()
            .UseSQLite();
    }

    #region SDS

    [TestMethod]
    public void TestSdsSqLiteStatementBuilderMapper()
    {
        // Setup
        var builder = StatementBuilderMapper.Get<SQLiteConnection>();

        // Assert
        Assert.IsNotNull(builder);
    }

    [TestMethod]
    public void TestSdsSqLiteDbHelperMapper()
    {
        // Setup
        var helper = DbHelperMapper.Get<SQLiteConnection>();

        // Assert
        Assert.IsNotNull(helper);
    }

    [TestMethod]
    public void TestSdsSqLiteDbSettingMapper()
    {
        // Setup
        var setting = DbSettingMapper.Get<SQLiteConnection>();

        // Assert
        Assert.IsNotNull(setting);
    }

    #endregion
}
