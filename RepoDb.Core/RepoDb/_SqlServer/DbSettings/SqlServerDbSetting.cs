﻿using RepoDb.Interfaces;
using System;
using System.Data.Common;
using System.Data.SqlClient;

namespace RepoDb.DbSettings
{
    /// <summary>
    /// A setting class used for <see cref="SqlConnection"/> data provider.
    /// </summary>
    internal sealed class SqlServerDbSetting : IDbSetting
    {
        private int? m_hashCode = null;

        #region Properties

        /// <summary>
        /// Gets the value that indicates whether the table hints are supported.
        /// </summary>
        public bool AreTableHintsSupported { get; } = true;

        /// <summary>
        /// Gets the character (or string) used for closing quote.
        /// </summary>
        public string ClosingQuote { get; } = "]";

        /// <summary>
        /// Gets the default averageable .NET CLR types for the database.
        /// </summary>
        public Type DefaultAverageableType { get; } = typeof(double);

        /// <summary>
        /// Gets the default schema of the database.
        /// </summary>
        public string DefaultSchema { get; } = "dbo";

        /// <summary>
        /// Gets a value that indicates whether setting the value of <see cref="DbParameter.Direction"/> object is supported.
        /// </summary>
        public bool IsDbParameterDirectionSettingSupported { get; } = true;

        /// <summary>
        /// Gets a value that indicates whether the <see cref="DbCommand"/> object must be disposed after calling the <see cref="DbCommand.ExecuteReader()"/> method.
        /// </summary>
        public bool IsDisposeDbCommandAfterExecuteReader { get; } = true;

        /// <summary>
        /// Gets a value whether the multiple statement execution is supported.
        /// </summary>
        public bool IsMultipleStatementExecutionSupported { get; } = true;

        /// <summary>
        /// Gets a value that indicates whether the current DB Provider supports the <see cref="DbCommand.Prepare()"/> calls.
        /// </summary>
        public bool IsPreparable { get; } = true;

        /// <summary>
        /// Gets a value that indicates whether the Insert/Update operation will be used for Merge operation.
        /// </summary>
        public bool IsUseUpsertForMergeOperation { get; } = false;

        /// <summary>
        /// Gets the character (or string) used for opening quote.
        /// </summary>
        public string OpeningQuote { get; } = "[";

        /// <summary>
        /// Gets the character (or string) used for the database command parameter quoting.
        /// </summary>
        public string ParameterPrefix { get; } = "@";

        /// <summary>
        /// Gets the character (or string) used for separating the schema.
        /// </summary>
        public string SchemaSeparator { get; } = ".";

        #endregion

        #region Equality and comparers

        /// <summary>
        /// Returns the hashcode for this <see cref="SqlServerDbSetting"/>.
        /// </summary>
        /// <returns>The hashcode value.</returns>
        public override int GetHashCode()
        {
            if (m_hashCode != null)
            {
                return m_hashCode.Value;
            }

            // Use the non nullable for perf purposes
            var hashCode = 0;

            // AreTableHintsSupported
            hashCode += AreTableHintsSupported.GetHashCode();

            // ClosingQuote
            if (!string.IsNullOrEmpty(ClosingQuote))
            {
                hashCode += ClosingQuote.GetHashCode();
            }

            // DefaultAverageableType
            if (DefaultAverageableType != null)
            {
                hashCode += DefaultAverageableType.GetHashCode();
            }

            // DefaultSchema
            if (!string.IsNullOrEmpty(DefaultSchema))
            {
                hashCode += DefaultSchema.GetHashCode();
            }

            // IsDbParameterDirectionSettingSupported
            hashCode += IsDbParameterDirectionSettingSupported.GetHashCode();

            // IsDisposeDbCommandAfterExecuteReader
            hashCode += IsDisposeDbCommandAfterExecuteReader.GetHashCode();

            // IsMultipleStatementExecutionSupported
            hashCode += IsMultipleStatementExecutionSupported.GetHashCode();

            // IsPreparable
            hashCode += IsPreparable.GetHashCode();

            // IsUseUpsertForMergeOperation
            hashCode += IsUseUpsertForMergeOperation.GetHashCode();

            // OpeningQuote
            if (!string.IsNullOrEmpty(OpeningQuote))
            {
                hashCode += OpeningQuote.GetHashCode();
            }

            // ParameterPrefix
            if (!string.IsNullOrEmpty(ParameterPrefix))
            {
                hashCode += ParameterPrefix.GetHashCode();
            }

            // SchemaSeparator
            if (!string.IsNullOrEmpty(SchemaSeparator))
            {
                hashCode += SchemaSeparator.GetHashCode();
            }

            // Set and return the hashcode
            return (m_hashCode = hashCode).Value;
        }

        /// <summary>
        /// Compares the <see cref="SqlServerDbSetting"/> object equality against the given target object.
        /// </summary>
        /// <param name="obj">The object to be compared to the current object.</param>
        /// <returns>True if the instances are equals.</returns>
        public override bool Equals(object obj)
        {
            return obj?.GetHashCode() == GetHashCode();
        }

        /// <summary>
        /// Compares the <see cref="SqlServerDbSetting"/> object equality against the given target object.
        /// </summary>
        /// <param name="other">The object to be compared to the current object.</param>
        /// <returns>True if the instances are equal.</returns>
        public bool Equals(SqlServerDbSetting other)
        {
            return other?.GetHashCode() == GetHashCode();
        }

        /// <summary>
        /// Compares the equality of the two <see cref="SqlServerDbSetting"/> objects.
        /// </summary>
        /// <param name="objA">The first <see cref="SqlServerDbSetting"/> object.</param>
        /// <param name="objB">The second <see cref="SqlServerDbSetting"/> object.</param>
        /// <returns>True if the instances are equal.</returns>
        public static bool operator ==(SqlServerDbSetting objA, SqlServerDbSetting objB)
        {
            if (ReferenceEquals(null, objA))
            {
                return ReferenceEquals(null, objB);
            }
            return objB?.GetHashCode() == objA.GetHashCode();
        }

        /// <summary>
        /// Compares the inequality of the two <see cref="SqlServerDbSetting"/> objects.
        /// </summary>
        /// <param name="objA">The first <see cref="SqlServerDbSetting"/> object.</param>
        /// <param name="objB">The second <see cref="SqlServerDbSetting"/> object.</param>
        /// <returns>True if the instances are not equal.</returns>
        public static bool operator !=(SqlServerDbSetting objA, SqlServerDbSetting objB)
        {
            return (objA == objB) == false;
        }

        #endregion
    }
}
