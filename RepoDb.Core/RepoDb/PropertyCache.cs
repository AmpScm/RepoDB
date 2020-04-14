﻿using RepoDb.Extensions;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace RepoDb
{
    /// <summary>
    /// A class used to cache the properties of the entity.
    /// </summary>
    public static class PropertyCache
    {
        private static readonly ConcurrentDictionary<int, IEnumerable<ClassProperty>> m_cache = new ConcurrentDictionary<int, IEnumerable<ClassProperty>>();

        #region Methods

        /// <summary>
        /// Gets the cached list of <see cref="ClassProperty"/> objects of the data entity.
        /// </summary>
        /// <typeparam name="TEntity">The type of the data entity.</typeparam>
        /// <returns>The cached list <see cref="ClassProperty"/> objects.</returns>
        public static IEnumerable<ClassProperty> Get<TEntity>()
            where TEntity : class
        {
            var type = typeof(TEntity);
            var properties = (IEnumerable<ClassProperty>)null;
            var key = GenerateHashCode(type);

            // Try get the value
            if (type.IsGenericType == false && m_cache.TryGetValue(key, out properties) == false)
            {
                properties = ClassExpression.GetProperties<TEntity>();
                m_cache.TryAdd(key, properties);
            }

            // Return the value
            return properties;
        }

        /// <summary>
        /// Gets the cached list of <see cref="ClassProperty"/> objects of the data entity.
        /// </summary>
        /// <param name="type">The type of the data entity.</param>
        /// <returns>The cached list <see cref="ClassProperty"/> objects.</returns>
        public static IEnumerable<ClassProperty> Get(Type type)
        {
            var properties = (IEnumerable<ClassProperty>)null;
            var key = GenerateHashCode(type);

            // Try get the value
            if (type.IsGenericType == false && m_cache.TryGetValue(key, out properties) == false)
            {
                properties = type.GetClassProperties().AsList();
                m_cache.TryAdd(key, properties);
            }

            // Return the value
            return properties;
        }

        #endregion

        #region Helpers

        /// <summary>
        /// Flushes all the existing cached enumerable of <see cref="ClassProperty"/> objects.
        /// </summary>
        public static void Flush()
        {
            m_cache.Clear();
        }

        /// <summary>
        /// Generates a hashcode for caching.
        /// </summary>
        /// <param name="type">The type of the data entity.</param>
        /// <returns>The generated hashcode.</returns>
        private static int GenerateHashCode(Type type)
        {
            return TypeExtension.GenerateHashCode(type);
        }

        #endregion
    }
}
