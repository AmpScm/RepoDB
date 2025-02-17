﻿using MySqlConnector;
using RepoDb.DbHelpers;
using RepoDb.DbSettings;
using RepoDb.StatementBuilders;
using System;

namespace RepoDb;

/// <summary>
/// A class that is being used to initialize necessary objects that is connected to <see cref="MySqlConnection"/> object.
/// </summary>
public static class MySqlConnectorBootstrap
{
    #region Properties

    /// <summary>
    /// Gets the value indicating whether the initialization is completed.
    /// </summary>
    public static bool IsInitialized { get; private set; }

    #endregion

    #region Methods

    /// <summary>
    /// Initializes all necessary settings for MySQL.
    /// </summary>
    [Obsolete("This class will soon to be hidden as internal class. Use the 'GlobalConfiguration.Setup().UseMySqlConnector()' method instead.")]
    public static void Initialize() => InitializeInternal();

    /// <summary>
    /// 
    /// </summary>
    internal static void InitializeInternal()
    {
        // Skip if already initialized
        if (IsInitialized == true)
        {
            return;
        }

        // Map the DbSetting
        DbSettingMapper.Add<MySqlConnection>(new MySqlConnectorDbSetting(), true);

        // Map the DbHelper
        DbHelperMapper.Add<MySqlConnection>(new MySqlConnectorDbHelper(), true);

        // Map the Statement Builder
        StatementBuilderMapper.Add<MySqlConnection>(new MySqlConnectorStatementBuilder(), true);

        // Set the flag
        IsInitialized = true;
    }

    #endregion
}
