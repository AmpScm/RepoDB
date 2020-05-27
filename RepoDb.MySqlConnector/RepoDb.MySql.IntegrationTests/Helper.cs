﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using RepoDb.Extensions;
using RepoDb.MySql.IntegrationTests.Models;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;

namespace RepoDb.MySql.IntegrationTests
{
    public static class Helper
    {
        static Helper()
        {
            EpocDate = new DateTime(1970, 1, 1, 0, 0, 0);
        }

        /// <summary>
        /// Gets the value of the Epoc date.
        /// </summary>
        public static DateTime EpocDate { get; }

        /// <summary>
        /// Gets the current <see cref="Random"/> object in used.
        /// </summary>
        public static Random Randomizer => new Random(1);

        /// <summary>
        /// Asserts the properties equality of 2 types.
        /// </summary>
        /// <typeparam name="T1">The type of first object.</typeparam>
        /// <typeparam name="T2">The type of second object.</typeparam>
        /// <param name="t1">The instance of first object.</param>
        /// <param name="t2">The instance of second object.</param>
        public static void AssertPropertiesEquality<T1, T2>(T1 t1, T2 t2)
        {
            var propertiesOfType1 = typeof(T1).GetProperties();
            var propertiesOfType2 = typeof(T2).GetProperties();
            propertiesOfType1.AsList().ForEach(propertyOfType1 =>
            {
                if (propertyOfType1.Name == "Id")
                {
                    return;
                }
                var propertyOfType2 = propertiesOfType2.FirstOrDefault(p => p.Name == propertyOfType1.Name);
                if (propertyOfType2 == null)
                {
                    return;
                }
                var value1 = propertyOfType1.GetValue(t1);
                var value2 = propertyOfType2.GetValue(t2);
                if (value1 is byte[] && value2 is byte[])
                {
                    var b1 = (byte[])value1;
                    var b2 = (byte[])value2;
                    for (var i = 0; i < Math.Min(b1.Length, b2.Length); i++)
                    {
                        var v1 = b1[i];
                        var v2 = b2[i];
                        Assert.AreEqual(v1, v2,
                            $"Assert failed for '{propertyOfType1.Name}'. The values are '{value1} ({propertyOfType1.PropertyType.FullName})' and '{value2} ({propertyOfType2.PropertyType.FullName})'.");
                    }
                }
                else
                {
                    Assert.AreEqual(value1, value2,
                        $"Assert failed for '{propertyOfType1.Name}'. The values are '{value1} ({propertyOfType1.PropertyType.FullName})' and '{value2} ({propertyOfType2.PropertyType.FullName})'.");
                }
            });
        }

        /// <summary>
        /// Asserts the members equality of 2 object and <see cref="ExpandoObject"/>.
        /// </summary>
        /// <typeparam name="T">The type of first object.</typeparam>
        /// <param name="obj">The instance of first object.</param>
        /// <param name="expandoObj">The instance of second object.</param>
        public static void AssertMembersEquality(object obj, object expandoObj)
        {
            var dictionary = new ExpandoObject() as IDictionary<string, object>;
            foreach (var property in expandoObj.GetType().GetProperties())
            {
                dictionary.Add(property.Name, property.GetValue(expandoObj));
            }
            AssertMembersEquality(obj, dictionary);
        }

        /// <summary>
        /// Asserts the members equality of 2 object and <see cref="ExpandoObject"/>.
        /// </summary>
        /// <typeparam name="T">The type of first object.</typeparam>
        /// <param name="obj">The instance of first object.</param>
        /// <param name="expandoObj">The instance of second object.</param>
        public static void AssertMembersEquality(object obj, ExpandoObject expandoObj)
        {
            var dictionary = expandoObj as IDictionary<string, object>;
            AssertMembersEquality(obj, dictionary);
        }

        /// <summary>
        /// Asserts the members equality of 2 objects.
        /// </summary>
        /// <typeparam name="T">The type of first object.</typeparam>
        /// <param name="obj">The instance of first object.</param>
        /// <param name="dictionary">The instance of second object.</param>
        public static void AssertMembersEquality(object obj, IDictionary<string, object> dictionary)
        {
            var properties = obj.GetType().GetProperties();
            properties.AsList().ForEach(property =>
            {
                if (property.Name == "Id")
                {
                    return;
                }
                if (dictionary.ContainsKey(property.Name))
                {
                    var value1 = property.GetValue(obj);
                    var value2 = dictionary[property.Name];
                    if (value1 is byte[] && value2 is byte[])
                    {
                        var b1 = (byte[])value1;
                        var b2 = (byte[])value2;
                        for (var i = 0; i < Math.Min(b1.Length, b2.Length); i++)
                        {
                            var v1 = b1[i];
                            var v2 = b2[i];
                            Assert.AreEqual(v1, v2,
                                $"Assert failed for '{property.Name}'. The values are '{v1}' and '{v2}'.");
                        }
                    }
                    else
                    {
                        var propertyType = property.PropertyType.GetUnderlyingType();
                        if (propertyType == typeof(TimeSpan) && value2 is DateTime)
                        {
                            value2 = ((DateTime)value2).TimeOfDay;
                        }
                        Assert.AreEqual(Convert.ChangeType(value1, propertyType), Convert.ChangeType(value2, propertyType),
                            $"Assert failed for '{property.Name}'. The values are '{value1}' and '{value2}'.");
                    }
                }
            });
        }

        #region CompleteTable

        /// <summary>
        /// Creates a list of <see cref="CompleteTable"/> objects.
        /// </summary>
        /// <param name="count">The number of rows.</param>
        /// <returns>A list of <see cref="CompleteTable"/> objects.</returns>
        public static List<CompleteTable> CreateCompleteTables(int count)
        {
            var tables = new List<CompleteTable>();
            var now = DateTime.SpecifyKind(
                DateTime.Parse(DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss.fffff")),
                    DateTimeKind.Unspecified);
            for (var i = 0; i < count; i++)
            {
                tables.Add(new CompleteTable
                {
                    ColumnVarchar = $"ColumnVarChar:{i}",
                    ColumnInt = i,
                    ColumnDecimal2 = Convert.ToDecimal(i),
                    ColumnDateTime = EpocDate,
                    ColumnBlob = Encoding.Default.GetBytes($"ColumnBlob:{i}"),
                    ColumnBlobAsArray = Encoding.Default.GetBytes($"ColumnBlobAsArray:{i}"),
                    ColumnBinary = Encoding.Default.GetBytes($"ColumnBinary:{i}"),
                    ColumnLongBlob = Encoding.Default.GetBytes($"ColumnLongBlob:{i}"),
                    ColumnMediumBlob = Encoding.Default.GetBytes($"ColumnMediumBlob:{i}"),
                    ColumnTinyBlob = Encoding.Default.GetBytes($"ColumnTinyBlob:{i}"),
                    ColumnVarBinary = Encoding.Default.GetBytes($"ColumnVarBinary:{i}"),
                    ColumnDate = EpocDate,
                    ColumnDateTime2 = now,
                    ColumnTime = EpocDate.AddHours(5).AddMinutes(7).AddSeconds(12).TimeOfDay,
                    ColumnTimeStamp = now,
                    ColumnYear = Convert.ToInt16(now.Year),
                    //ColumnGeometry = Encoding.Default.GetBytes($"POLYGON ((0 0, 50 0, 50 50, 0 50, 0 0))"),
                    //ColumnLineString = Encoding.Default.GetBytes($"LINESTRING (-122.36 47.656, -122.343 47.656)"),
                    //ColumnMultiLineString = Encoding.Default.GetBytes($"ColumnMultiLineString:{i}"),
                    //ColumnMultiPoint = Encoding.Default.GetBytes($"ColumnMultiPoint:{i}"),
                    //ColumnMultiPolygon = Encoding.Default.GetBytes($"ColumnMultiPolygon:{i}"),
                    //ColumnPoint = Encoding.Default.GetBytes($"ColumnPoint:{i}"),
                    //ColumnPolygon = Encoding.Default.GetBytes($"ColumnPolygon:{i}"),
                    ColumnBigint = i,
                    ColumnDecimal = Convert.ToDecimal(i),
                    ColumnDouble = Convert.ToDouble(i),
                    ColumnFloat = Convert.ToSingle(i),
                    ColumnInt2 = i,
                    ColumnMediumInt = i,
                    ColumnReal = Convert.ToDouble(i),
                    ColumnSmallInt = Convert.ToInt16(i),
                    ColumnTinyInt = (SByte)i,
                    ColumnChar = "C",
                    ColumnJson = "{\"Field1\": \"Value1\", \"Field2\": \"Value2\"}",
                    ColumnNChar = "C",
                    ColumnNVarChar = $"ColumnNVarChar:{i}",
                    ColumnLongText = $"ColumnLongText:{i}",
                    ColumnMediumText = $"ColumnMediumText:{i}",
                    ColumnText = $"ColumText:{i}",
                    ColumnTinyText = $"ColumnTinyText:{i}",
                    ColumnBit = 1
                });
            }
            return tables;
        }

        /// <summary>
        /// Update the properties of <see cref="CompleteTable"/> instance.
        /// </summary>
        /// <param name="table">The instance to be updated.</param>
        public static void UpdateCompleteTableProperties(CompleteTable table)
        {
            var now = DateTime.SpecifyKind(
                DateTime.Parse(DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss.fffff")),
                    DateTimeKind.Unspecified);
            table.ColumnVarchar = $"ColumnVarChar:{1}-Updated";
            table.ColumnInt = 1;
            table.ColumnDecimal2 = Convert.ToDecimal(1);
            table.ColumnDateTime = EpocDate;
            table.ColumnBlob = Encoding.Default.GetBytes($"ColumnBlob:{1}-Updated");
            table.ColumnBlobAsArray = Encoding.Default.GetBytes($"ColumnBlobAsArray:{1}-Updated");
            table.ColumnBinary = Encoding.Default.GetBytes($"ColumnBinary:{1}-Updated");
            table.ColumnLongBlob = Encoding.Default.GetBytes($"ColumnLongBlob:{1}-Updated");
            table.ColumnMediumBlob = Encoding.Default.GetBytes($"ColumnMediumBlob:{1}-Updated");
            table.ColumnTinyBlob = Encoding.Default.GetBytes($"ColumnTinyBlob:{1}-Updated");
            table.ColumnVarBinary = Encoding.Default.GetBytes($"ColumnVarBinary:{1}-Updated");
            table.ColumnDate = EpocDate;
            table.ColumnDateTime2 = now;
            table.ColumnTime = EpocDate.AddHours(5).AddMinutes(7).AddSeconds(12).TimeOfDay;
            table.ColumnTimeStamp = now;
            table.ColumnYear = Convert.ToInt16(now.Year);
            //table.ColumnGeometry = Encoding.Default.GetBytes($"ColumnGeometry:{1}");
            //table.ColumnLineString = Encoding.Default.GetBytes($"ColumnLineString:{1}");
            //table.ColumnMultiLineString = Encoding.Default.GetBytes($"ColumnMultiLineString:{1}");
            //table.ColumnMultiPoint = Encoding.Default.GetBytes($"ColumnMultiPoint:{1}");
            //table.ColumnMultiPolygon = Encoding.Default.GetBytes($"ColumnMultiPolygon:{1}");
            //table.ColumnPoint = Encoding.Default.GetBytes($"ColumnPoint:{1}");
            //table.ColumnPolygon = Encoding.Default.GetBytes($"ColumnPolygon:{1}");
            table.ColumnBigint = 1;
            table.ColumnDecimal = Convert.ToDecimal(1);
            table.ColumnDouble = Convert.ToDouble(1);
            table.ColumnFloat = Convert.ToSingle(1);
            table.ColumnInt2 = 1;
            table.ColumnMediumInt = 1;
            table.ColumnReal = Convert.ToDouble(1);
            table.ColumnSmallInt = Convert.ToInt16(1);
            table.ColumnTinyInt = (SByte)1;
            table.ColumnChar = "C";
            table.ColumnJson = "{\"Field\": \"Value-Updated\"}";
            table.ColumnNChar = "C";
            table.ColumnNVarChar = $"ColumnNVarChar:{1}-Updated";
            table.ColumnLongText = $"ColumnLongText:{1}-Updated";
            table.ColumnMediumText = $"ColumnMediumText:{1}-Updated";
            table.ColumnText = $"ColumText:{1}-Updated";
            table.ColumnTinyText = $"ColumnTinyText:{1}-Updated";
            table.ColumnBit = (UInt64)1;
        }

        /// <summary>
        /// Creates a list of <see cref="CompleteTable"/> objects represented as dynamics.
        /// </summary>
        /// <param name="count">The number of rows.</param>
        /// <returns>A list of <see cref="CompleteTable"/> objects represented as dynamics.</returns>
        public static List<dynamic> CreateCompleteTablesAsDynamics(int count)
        {
            var tables = new List<dynamic>();
            var now = DateTime.SpecifyKind(
                DateTime.Parse(DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss.fffff")),
                    DateTimeKind.Unspecified);
            for (var i = 0; i < count; i++)
            {
                tables.Add(new
                {
                    Id = (long)(i + 1),
                    ColumnVarchar = $"ColumnVarChar:{i}",
                    ColumnInt = i,
                    ColumnDecimal2 = Convert.ToDecimal(i),
                    ColumnDateTime = EpocDate,
                    ColumnBlob = Encoding.Default.GetBytes($"ColumnBlob:{i}"),
                    ColumnBlobAsArray = Encoding.Default.GetBytes($"ColumnBlobAsArray:{i}"),
                    ColumnBinary = Encoding.Default.GetBytes($"ColumnBinary:{i}"),
                    ColumnLongBlob = Encoding.Default.GetBytes($"ColumnLongBlob:{i}"),
                    ColumnMediumBlob = Encoding.Default.GetBytes($"ColumnMediumBlob:{i}"),
                    ColumnTinyBlob = Encoding.Default.GetBytes($"ColumnTinyBlob:{i}"),
                    ColumnVarBinary = Encoding.Default.GetBytes($"ColumnVarBinary:{i}"),
                    ColumnDate = EpocDate,
                    ColumnDateTime2 = now,
                    ColumnTime = EpocDate.AddHours(5).AddMinutes(7).AddSeconds(12).TimeOfDay,
                    ColumnTimeStamp = now,
                    ColumnYear = Convert.ToInt16(now.Year),
                    //ColumnGeometry = Encoding.Default.GetBytes($"POLYGON ((0 0, 50 0, 50 50, 0 50, 0 0))"),
                    //ColumnLineString = Encoding.Default.GetBytes($"LINESTRING (-122.36 47.656, -122.343 47.656)"),
                    //ColumnMultiLineString = Encoding.Default.GetBytes($"ColumnMultiLineString:{i}"),
                    //ColumnMultiPoint = Encoding.Default.GetBytes($"ColumnMultiPoint:{i}"),
                    //ColumnMultiPolygon = Encoding.Default.GetBytes($"ColumnMultiPolygon:{i}"),
                    //ColumnPoint = Encoding.Default.GetBytes($"ColumnPoint:{i}"),
                    //ColumnPolygon = Encoding.Default.GetBytes($"ColumnPolygon:{i}"),
                    ColumnBigint = i,
                    ColumnDecimal = Convert.ToDecimal(i),
                    ColumnDouble = Convert.ToDouble(i),
                    ColumnFloat = Convert.ToSingle(i),
                    ColumnInt2 = i,
                    ColumnMediumInt = i,
                    ColumnReal = Convert.ToDouble(i),
                    ColumnSmallInt = Convert.ToInt16(i),
                    ColumnTinyInt = (SByte)i,
                    ColumnChar = "C",
                    ColumnJson = "{\"Field1\": \"Value1\", \"Field2\": \"Value2\"}",
                    ColumnNChar = "C",
                    ColumnNVarChar = $"ColumnNVarChar:{i}",
                    ColumnLongText = $"ColumnLongText:{i}",
                    ColumnMediumText = $"ColumnMediumText:{i}",
                    ColumnText = $"ColumText:{i}",
                    ColumnTinyText = $"ColumnTinyText:{i}",
                    ColumnBit = 1
                });
            }
            return tables;
        }

        /// <summary>
        /// Update the properties of <see cref="CompleteTable"/> instance represented asy dynamic.
        /// </summary>
        /// <param name="table">The instance to be updated.</param>
        public static void UpdateCompleteTableAsDynamicProperties(dynamic table)
        {
            var now = DateTime.SpecifyKind(
                DateTime.Parse(DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss.fffff")),
                    DateTimeKind.Unspecified);
            table.ColumnVarchar = $"ColumnVarChar:{1}";
            table.ColumnInt = 1;
            table.ColumnDecimal2 = Convert.ToDecimal(1);
            table.ColumnDateTime = EpocDate;
            table.ColumnBlob = Encoding.Default.GetBytes($"ColumnBlob:{1}");
            table.ColumnBlobAsArray = Encoding.Default.GetBytes($"ColumnBlobAsArray:{1}");
            table.ColumnBinary = Encoding.Default.GetBytes($"ColumnBinary:{1}");
            table.ColumnLongBlob = Encoding.Default.GetBytes($"ColumnLongBlob:{1}");
            table.ColumnMediumBlob = Encoding.Default.GetBytes($"ColumnMediumBlob:{1}");
            table.ColumnTinyBlob = Encoding.Default.GetBytes($"ColumnTinyBlob:{1}");
            table.ColumnVarBinary = Encoding.Default.GetBytes($"ColumnVarBinary:{1}");
            table.ColumnDate = EpocDate;
            table.ColumnDateTime2 = now;
            table.ColumnTime = EpocDate.AddHours(5).AddMinutes(7).AddSeconds(12).TimeOfDay;
            table.ColumnTimeStamp = now;
            table.ColumnYear = Convert.ToInt16(now.Year);
            //table.ColumnGeometry = Encoding.Default.GetBytes($"ColumnGeometry:{1}");
            //table.ColumnLineString = Encoding.Default.GetBytes($"ColumnLineString:{1}");
            //table.ColumnMultiLineString = Encoding.Default.GetBytes($"ColumnMultiLineString:{1}");
            //table.ColumnMultiPoint = Encoding.Default.GetBytes($"ColumnMultiPoint:{1}");
            //table.ColumnMultiPolygon = Encoding.Default.GetBytes($"ColumnMultiPolygon:{1}");
            //table.ColumnPoint = Encoding.Default.GetBytes($"ColumnPoint:{1}");
            //table.ColumnPolygon = Encoding.Default.GetBytes($"ColumnPolygon:{1}");
            table.ColumnBigint = 1;
            table.ColumnDecimal = Convert.ToDecimal(1);
            table.ColumnDouble = Convert.ToDouble(1);
            table.ColumnFloat = Convert.ToSingle(1);
            table.ColumnInt2 = 1;
            table.ColumnMediumInt = 1;
            table.ColumnReal = Convert.ToDouble(1);
            table.ColumnSmallInt = Convert.ToInt16(1);
            table.ColumnTinyInt = (SByte)1;
            table.ColumnChar = "C";
            table.ColumnJson = "{ \"Field\" : \"Value\" }";
            table.ColumnNChar = "C";
            table.ColumnNVarChar = $"ColumnNVarChar:{1}";
            table.ColumnLongText = $"ColumnLongText:{1}";
            table.ColumnMediumText = $"ColumnMediumText:{1}";
            table.ColumnText = $"ColumText:{1}";
            table.ColumnTinyText = $"ColumnTinyText:{1}";
            table.ColumnBit = (UInt64)1;
        }

        #endregion

        #region NonIdentityCompleteTable

        /// <summary>
        /// Creates a list of <see cref="NonIdentityCompleteTable"/> objects.
        /// </summary>
        /// <param name="count">The number of rows.</param>
        /// <returns>A list of <see cref="NonIdentityCompleteTable"/> objects.</returns>
        public static List<NonIdentityCompleteTable> CreateNonIdentityCompleteTables(int count)
        {
            var tables = new List<NonIdentityCompleteTable>();
            var now = DateTime.SpecifyKind(
                DateTime.Parse(DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss.fffff")),
                    DateTimeKind.Unspecified);
            for (var i = 0; i < count; i++)
            {
                tables.Add(new NonIdentityCompleteTable
                {
                    Id = (i + 1),
                    ColumnVarchar = $"ColumnVarChar:{i}",
                    ColumnInt = i,
                    ColumnDecimal2 = Convert.ToDecimal(i),
                    ColumnDateTime = EpocDate,
                    ColumnBlob = Encoding.Default.GetBytes($"ColumnBlob:{i}"),
                    ColumnBlobAsArray = Encoding.Default.GetBytes($"ColumnBlobAsArray:{i}"),
                    ColumnBinary = Encoding.Default.GetBytes($"ColumnBinary:{i}"),
                    ColumnLongBlob = Encoding.Default.GetBytes($"ColumnLongBlob:{i}"),
                    ColumnMediumBlob = Encoding.Default.GetBytes($"ColumnMediumBlob:{i}"),
                    ColumnTinyBlob = Encoding.Default.GetBytes($"ColumnTinyBlob:{i}"),
                    ColumnVarBinary = Encoding.Default.GetBytes($"ColumnVarBinary:{i}"),
                    ColumnDate = EpocDate,
                    ColumnDateTime2 = now,
                    ColumnTime = EpocDate.AddHours(5).AddMinutes(7).AddSeconds(12).TimeOfDay,
                    ColumnTimeStamp = now,
                    ColumnYear = Convert.ToInt16(now.Year),
                    //ColumnGeometry = Encoding.Default.GetBytes($"POLYGON ((0 0, 50 0, 50 50, 0 50, 0 0))"),
                    //ColumnLineString = Encoding.Default.GetBytes($"LINESTRING (-122.36 47.656, -122.343 47.656)"),
                    //ColumnMultiLineString = Encoding.Default.GetBytes($"ColumnMultiLineString:{i}"),
                    //ColumnMultiPoint = Encoding.Default.GetBytes($"ColumnMultiPoint:{i}"),
                    //ColumnMultiPolygon = Encoding.Default.GetBytes($"ColumnMultiPolygon:{i}"),
                    //ColumnPoint = Encoding.Default.GetBytes($"ColumnPoint:{i}"),
                    //ColumnPolygon = Encoding.Default.GetBytes($"ColumnPolygon:{i}"),
                    ColumnBigint = i,
                    ColumnDecimal = Convert.ToDecimal(i),
                    ColumnDouble = Convert.ToDouble(i),
                    ColumnFloat = Convert.ToSingle(i),
                    ColumnInt2 = i,
                    ColumnMediumInt = i,
                    ColumnReal = Convert.ToDouble(i),
                    ColumnSmallInt = Convert.ToInt16(i),
                    ColumnTinyInt = (SByte)i,
                    ColumnChar = "C",
                    ColumnJson = "{\"Field1\": \"Value1\", \"Field2\": \"Value2\"}",
                    ColumnNChar = "C",
                    ColumnNVarChar = $"ColumnNVarChar:{i}",
                    ColumnLongText = $"ColumnLongText:{i}",
                    ColumnMediumText = $"ColumnMediumText:{i}",
                    ColumnText = $"ColumText:{i}",
                    ColumnTinyText = $"ColumnTinyText:{i}",
                    ColumnBit = 1
                });
            }
            return tables;
        }

        /// <summary>
        /// Update the properties of <see cref="NonIdentityCompleteTable"/> instance.
        /// </summary>
        /// <param name="table">The instance to be updated.</param>
        public static void UpdateNonIdentityCompleteTableProperties(NonIdentityCompleteTable table)
        {
            var now = DateTime.SpecifyKind(
                DateTime.Parse(DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss.fffff")),
                    DateTimeKind.Unspecified);
            table.ColumnVarchar = $"ColumnVarChar:{1}";
            table.ColumnInt = 1;
            table.ColumnDecimal2 = Convert.ToDecimal(1);
            table.ColumnDateTime = EpocDate;
            table.ColumnBlob = Encoding.Default.GetBytes($"ColumnBlob:{1}");
            table.ColumnBlobAsArray = Encoding.Default.GetBytes($"ColumnBlobAsArray:{1}");
            table.ColumnBinary = Encoding.Default.GetBytes($"ColumnBinary:{1}");
            table.ColumnLongBlob = Encoding.Default.GetBytes($"ColumnLongBlob:{1}");
            table.ColumnMediumBlob = Encoding.Default.GetBytes($"ColumnMediumBlob:{1}");
            table.ColumnTinyBlob = Encoding.Default.GetBytes($"ColumnTinyBlob:{1}");
            table.ColumnVarBinary = Encoding.Default.GetBytes($"ColumnVarBinary:{1}");
            table.ColumnDate = EpocDate;
            table.ColumnDateTime2 = now;
            table.ColumnTime = EpocDate.AddHours(5).AddMinutes(7).AddSeconds(12).TimeOfDay;
            table.ColumnTimeStamp = now;
            table.ColumnYear = Convert.ToInt16(now.Year);
            //table.ColumnGeometry = Encoding.Default.GetBytes($"ColumnGeometry:{1}");
            //table.ColumnLineString = Encoding.Default.GetBytes($"ColumnLineString:{1}");
            //table.ColumnMultiLineString = Encoding.Default.GetBytes($"ColumnMultiLineString:{1}");
            //table.ColumnMultiPoint = Encoding.Default.GetBytes($"ColumnMultiPoint:{1}");
            //table.ColumnMultiPolygon = Encoding.Default.GetBytes($"ColumnMultiPolygon:{1}");
            //table.ColumnPoint = Encoding.Default.GetBytes($"ColumnPoint:{1}");
            //table.ColumnPolygon = Encoding.Default.GetBytes($"ColumnPolygon:{1}");
            table.ColumnBigint = 1;
            table.ColumnDecimal = Convert.ToDecimal(1);
            table.ColumnDouble = Convert.ToDouble(1);
            table.ColumnFloat = Convert.ToSingle(1);
            table.ColumnInt2 = 1;
            table.ColumnMediumInt = 1;
            table.ColumnReal = Convert.ToDouble(1);
            table.ColumnSmallInt = Convert.ToInt16(1);
            table.ColumnTinyInt = (SByte)1;
            table.ColumnChar = "C";
            table.ColumnJson = "{\"Field\": \"Value\"}";
            table.ColumnNChar = "C";
            table.ColumnNVarChar = $"ColumnNVarChar:{1}";
            table.ColumnLongText = $"ColumnLongText:{1}";
            table.ColumnMediumText = $"ColumnMediumText:{1}";
            table.ColumnText = $"ColumText:{1}";
            table.ColumnTinyText = $"ColumnTinyText:{1}";
            table.ColumnBit = (UInt64)1;
        }

        /// <summary>
        /// Creates a list of <see cref="NonIdentityCompleteTable"/> objects represented as dynamics.
        /// </summary>
        /// <param name="count">The number of rows.</param>
        /// <returns>A list of <see cref="NonIdentityCompleteTable"/> objects represented as dynamics.</returns>
        public static List<dynamic> CreateNonIdentityCompleteTablesAsDynamics(int count)
        {
            var tables = new List<dynamic>();
            var now = DateTime.SpecifyKind(
                DateTime.Parse(DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss.fffff")),
                    DateTimeKind.Unspecified);
            for (var i = 0; i < count; i++)
            {
                tables.Add(new
                {
                    Id = (long)(i + 1),
                    ColumnVarchar = $"ColumnVarChar:{i}",
                    ColumnInt = i,
                    ColumnDecimal2 = Convert.ToDecimal(i),
                    ColumnDateTime = EpocDate,
                    ColumnBlob = Encoding.Default.GetBytes($"ColumnBlob:{i}"),
                    ColumnBlobAsArray = Encoding.Default.GetBytes($"ColumnBlobAsArray:{i}"),
                    ColumnBinary = Encoding.Default.GetBytes($"ColumnBinary:{i}"),
                    ColumnLongBlob = Encoding.Default.GetBytes($"ColumnLongBlob:{i}"),
                    ColumnMediumBlob = Encoding.Default.GetBytes($"ColumnMediumBlob:{i}"),
                    ColumnTinyBlob = Encoding.Default.GetBytes($"ColumnTinyBlob:{i}"),
                    ColumnVarBinary = Encoding.Default.GetBytes($"ColumnVarBinary:{i}"),
                    ColumnDate = EpocDate,
                    ColumnDateTime2 = now,
                    ColumnTime = EpocDate.AddHours(5).AddMinutes(7).AddSeconds(12).TimeOfDay,
                    ColumnTimeStamp = now,
                    ColumnYear = Convert.ToInt16(now.Year),
                    //ColumnGeometry = Encoding.Default.GetBytes($"POLYGON ((0 0, 50 0, 50 50, 0 50, 0 0))"),
                    //ColumnLineString = Encoding.Default.GetBytes($"LINESTRING (-122.36 47.656, -122.343 47.656)"),
                    //ColumnMultiLineString = Encoding.Default.GetBytes($"ColumnMultiLineString:{i}"),
                    //ColumnMultiPoint = Encoding.Default.GetBytes($"ColumnMultiPoint:{i}"),
                    //ColumnMultiPolygon = Encoding.Default.GetBytes($"ColumnMultiPolygon:{i}"),
                    //ColumnPoint = Encoding.Default.GetBytes($"ColumnPoint:{i}"),
                    //ColumnPolygon = Encoding.Default.GetBytes($"ColumnPolygon:{i}"),
                    ColumnBigint = i,
                    ColumnDecimal = Convert.ToDecimal(i),
                    ColumnDouble = Convert.ToDouble(i),
                    ColumnFloat = Convert.ToSingle(i),
                    ColumnInt2 = i,
                    ColumnMediumInt = i,
                    ColumnReal = Convert.ToDouble(i),
                    ColumnSmallInt = Convert.ToInt16(i),
                    ColumnTinyInt = (SByte)i,
                    ColumnChar = "C",
                    ColumnJson = "{\"Field1\": \"Value1\", \"Field2\": \"Value2\"}",
                    ColumnNChar = "C",
                    ColumnNVarChar = $"ColumnNVarChar:{i}",
                    ColumnLongText = $"ColumnLongText:{i}",
                    ColumnMediumText = $"ColumnMediumText:{i}",
                    ColumnText = $"ColumText:{i}",
                    ColumnTinyText = $"ColumnTinyText:{i}",
                    ColumnBit = 1
                });
            }
            return tables;
        }

        /// <summary>
        /// Update the properties of <see cref="NonIdentityCompleteTable"/> instance represented asy dynamic.
        /// </summary>
        /// <param name="table">The instance to be updated.</param>
        public static void UpdateNonIdentityCompleteTableAsDynamicProperties(dynamic table)
        {
            var now = DateTime.SpecifyKind(
                DateTime.Parse(DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss.fffff")),
                    DateTimeKind.Unspecified);
            table.ColumnVarchar = $"ColumnVarChar:{1}";
            table.ColumnInt = 1;
            table.ColumnDecimal2 = Convert.ToDecimal(1);
            table.ColumnDateTime = EpocDate;
            table.ColumnBlob = Encoding.Default.GetBytes($"ColumnBlob:{1}");
            table.ColumnBlobAsArray = Encoding.Default.GetBytes($"ColumnBlobAsArray:{1}");
            table.ColumnBinary = Encoding.Default.GetBytes($"ColumnBinary:{1}");
            table.ColumnLongBlob = Encoding.Default.GetBytes($"ColumnLongBlob:{1}");
            table.ColumnMediumBlob = Encoding.Default.GetBytes($"ColumnMediumBlob:{1}");
            table.ColumnTinyBlob = Encoding.Default.GetBytes($"ColumnTinyBlob:{1}");
            table.ColumnVarBinary = Encoding.Default.GetBytes($"ColumnVarBinary:{1}");
            table.ColumnDate = EpocDate;
            table.ColumnDateTime2 = now;
            table.ColumnTime = EpocDate.AddHours(5).AddMinutes(7).AddSeconds(12).TimeOfDay;
            table.ColumnTimeStamp = now;
            table.ColumnYear = Convert.ToInt16(now.Year);
            //table.ColumnGeometry = Encoding.Default.GetBytes($"ColumnGeometry:{1}");
            //table.ColumnLineString = Encoding.Default.GetBytes($"ColumnLineString:{1}");
            //table.ColumnMultiLineString = Encoding.Default.GetBytes($"ColumnMultiLineString:{1}");
            //table.ColumnMultiPoint = Encoding.Default.GetBytes($"ColumnMultiPoint:{1}");
            //table.ColumnMultiPolygon = Encoding.Default.GetBytes($"ColumnMultiPolygon:{1}");
            //table.ColumnPoint = Encoding.Default.GetBytes($"ColumnPoint:{1}");
            //table.ColumnPolygon = Encoding.Default.GetBytes($"ColumnPolygon:{1}");
            table.ColumnBigint = 1;
            table.ColumnDecimal = Convert.ToDecimal(1);
            table.ColumnDouble = Convert.ToDouble(1);
            table.ColumnFloat = Convert.ToSingle(1);
            table.ColumnInt2 = 1;
            table.ColumnMediumInt = 1;
            table.ColumnReal = Convert.ToDouble(1);
            table.ColumnSmallInt = Convert.ToInt16(1);
            table.ColumnTinyInt = (SByte)1;
            table.ColumnChar = "C";
            table.ColumnJson = "{ \"Field\" : \"Value\" }";
            table.ColumnNChar = "C";
            table.ColumnNVarChar = $"ColumnNVarChar:{1}";
            table.ColumnLongText = $"ColumnLongText:{1}";
            table.ColumnMediumText = $"ColumnMediumText:{1}";
            table.ColumnText = $"ColumText:{1}";
            table.ColumnTinyText = $"ColumnTinyText:{1}";
            table.ColumnBit = (UInt64)1;
        }

        #endregion
    }
}
