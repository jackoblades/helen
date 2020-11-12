using Helen.App.Models;
using Helen.App.Extensions;
using Microsoft.Data.Sqlite;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Helen.App.Repository.Charters
{
    public class SettingsCharter : Charter
    {
        #region Properties

        public static SettingsCharter Instance => _instance ?? (_instance = new SettingsCharter(Orm.Instance));
        private static SettingsCharter _instance;

        #endregion

        #region Constructors

        public SettingsCharter(Orm orm) : base(orm) { }

        #endregion

        #region Methods

        public async Task<Settings> ReadAsync()
        {
            Settings result = null;

            using (var db = await _orm.GetDatabaseAsync())
            {
                using (var cmd = new SqliteCommand($"SELECT * FROM {nameof(Settings)} LIMIT 1", db))
                {
                    using (var query = await cmd.ExecuteReaderAsync())
                    {
                        var row = query.ReadValues();

                        if (row.Any())
                        {
                            result = Map(new Settings(), row.First());
                        }
                    }
                }
            }

            return result;
        }

        public async Task UpsertAsync(Settings settings, SqliteCommand parentCmd = null)
        {
            using (var database = parentCmd?.Connection == null ? await _orm.GetDatabaseAsync() : null)
            {
                var db = database ?? parentCmd.Connection;

                using (var transaction = parentCmd?.Transaction == null ? db.BeginTransaction() : null)
                {
                    var t = transaction ?? parentCmd.Transaction;

                    using (var cmd = new SqliteCommand(string.Empty, db, t))
                    {
                        // Amend.
                        AmendParameters(settings, cmd);
                        cmd.GenerateUpsert(nameof(Settings));

                        // Upsert our foreign key components, if any.

                        // Execute.
                        await cmd.ExecuteNonQueryAsync();

                        // Upsert other components, if any.

                        // Commit, if we newly created the transaction.
                        transaction?.Commit();
                    }
                }
            }
        }

        private Settings Map(Settings settings, IDictionary<string, object> row)
        {
            settings.Id          = Orm.MapGuid(row[nameof(settings.Id)]);
            settings.MusicVolume = Orm.MapInt(row[nameof(settings.MusicVolume)]);
            settings.Preferences = (Preferences)Orm.MapInt(row[nameof(settings.Preferences)]);
            return settings;
        }

        private void AmendParameters(Settings settings, SqliteCommand cmd)
        {
            cmd.AddParameter($"@{nameof(settings.Id)}",          settings.Id);
            cmd.AddParameter($"@{nameof(settings.MusicVolume)}", settings.MusicVolume);
            cmd.AddParameter($"@{nameof(settings.Preferences)}", settings.Preferences);
        }

        #endregion
    }
}
