using Helen.App.Models;
using Microsoft.Data.Sqlite;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Helen.App.Repository
{
    public static class Orm
    {
        #region Properties

        public const int UserVersion = 1;

        public static readonly string Name = "./db.sqlite3";

        #endregion

        #region Methods

        /// <summary>
        /// Opens/Creates a DB, constructs the schema, creating tables as needed.
        /// </summary>
        public static async Task InitAsync()
        {
            using (var db = await GetDatabaseAsync())
            {
                // Create tables.
                ExecuteNonQueryAsync(db, $"CREATE TABLE IF NOT EXISTS \"{nameof(Settings)}\" (\"Id\" BLOB NOT NULL CONSTRAINT \"PK_{nameof(Settings)}\" PRIMARY KEY, \"Preferences\" INT NOT NULL, \"MusicVolume\" INT NOT NULL)");
            }
        }

        public static async Task<SqliteConnection> GetDatabaseAsync()
        {
            // Find out if our DB exists before we attempt to open, which we will create if it does not exist.
            var isNew = File.Exists(Name);

            // Open/create DB file.
            var db = new SqliteConnection($"DataSource={Name}");
            db.Open();

            // If this is a newly created DB, add in the current UserVersion.
            if (isNew)
            {
                await ExecuteScalarAsync(db, $"PRAGMA user_version={UserVersion}");
            }

            return db;
        }

        private static async Task ExecuteScalarAsync(SqliteConnection db, string sql)
        {
            using (var command = new SqliteCommand(sql, db))
            {
                await command.ExecuteScalarAsync();
            }
        }

        private static async void ExecuteNonQueryAsync(SqliteConnection db, string sql)
        {
            using (var command = new SqliteCommand(sql, db))
            {
                await command.ExecuteNonQueryAsync();
            }
        }

        #region Mappers

        public static string MapString(object obj) => (obj != DBNull.Value) ? obj as string : null;

        public static Guid MapGuid(object obj) => (obj != DBNull.Value) ? new Guid((byte[])obj) : Guid.Empty;

        public static DateTimeOffset MapDateTimeOffset(object obj) => DateTimeOffset.TryParse(obj as string, out DateTimeOffset result) ? result : default;

        public static DateTimeOffset? MapNullableDateTimeOffset(object obj) => DateTimeOffset.TryParse(obj as string, out DateTimeOffset result) ? result : default(DateTimeOffset?);

        public static int MapInt(object obj) => (obj != DBNull.Value) ? Convert.ToInt32(obj) : default;

        public static int? MapNullableInt(object obj) => (obj != DBNull.Value) ? Convert.ToInt32(obj) : default(int?);

        #endregion

        #endregion
    }
}
