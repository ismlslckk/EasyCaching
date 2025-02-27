﻿namespace EasyCaching.SQLite
{
    using System;
    using EasyCaching.Core;
    using EasyCaching.Core.Configurations;
    using Microsoft.Extensions.Configuration;

    /// <summary>
    /// Easy caching options extensions.
    /// </summary>
    public static class EasyCachingOptionsExtensions
    {
        /// <summary>
        /// Uses the SQLite.
        /// </summary>
        /// <returns>The SQL ite.</returns>
        /// <param name="options">Options.</param>
        /// <param name="configure">Configure.</param>
        /// <param name="name">Name.</param>
        public static EasyCachingOptions UseSQLite(this EasyCachingOptions options, Action<SQLiteOptions> configure, string name = EasyCachingConstValue.DefaultSQLiteName)
        {
            if (configure == null)
            {
                throw new ArgumentNullException(nameof(configure));
            }

            options.RegisterExtension(new SQLiteOptionsExtension(name, configure));
            return options;
        }

        /// <summary>
        /// Uses the SQLite.
        /// </summary>
        /// <returns>The SQL ite.</returns>
        /// <param name="options">Options.</param>
        /// <param name="configuration">Configuration.</param>
        /// <param name="name">Name.</param>
        /// <param name="sectionName">Section name.</param>
        public static EasyCachingOptions UseSQLite(this EasyCachingOptions options, IConfiguration configuration, string name = EasyCachingConstValue.DefaultSQLiteName, string sectionName = EasyCachingConstValue.SQLiteSection)
        {
            var dbConfig = configuration.GetSection(sectionName);
            var sqliteOptions = new SQLiteOptions();
            dbConfig.Bind(sqliteOptions);

            void configure(SQLiteOptions x)
            {
                x.EnableLogging = sqliteOptions.EnableLogging;
                x.MaxRdSecond = sqliteOptions.MaxRdSecond;             
                x.DBConfig = sqliteOptions.DBConfig;
            }

            options.RegisterExtension(new SQLiteOptionsExtension(name, configure));
            return options;
        }
    }
}
