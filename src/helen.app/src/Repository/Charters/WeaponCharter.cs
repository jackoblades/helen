using Helen.App.Extensions;
using Helen.Core;
using Microsoft.Data.Sqlite;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Helen.App.Repository.Charters
{
    public class WeaponCharter : Charter
    {
        #region Properties

        public static WeaponCharter Instance => _instance ?? (_instance = new WeaponCharter(Orm.Instance));
        private static WeaponCharter _instance;

        #endregion

        #region Constructors

        public WeaponCharter(Orm orm) : base(orm) { }

        #endregion

        #region Methods

        public async Task<Weapon> ReadAsync()
        {
            Weapon result = null;

            using (var db = await _orm.GetDatabaseAsync())
            {
                using (var cmd = new SqliteCommand($"SELECT * FROM {nameof(Weapon)} LIMIT 1", db))
                {
                    using (var query = await cmd.ExecuteReaderAsync())
                    {
                        var row = query.ReadValues();

                        if (row.Any())
                        {
                            result = Map(new Weapon(), row.First());
                        }
                    }
                }
            }

            return result;
        }

        public async Task UpsertAsync(Weapon weapon, SqliteCommand parentCmd = null)
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
                        AmendParameters(weapon, cmd);
                        cmd.GenerateUpsert(nameof(Weapon));

                        // Upsert our foreign key components, if any.

                        // Execute.
                        await cmd.ExecuteNonQueryAsync();

                        // Upsert other components, if any.

                        // Commit, if this upsert created the transaction.
                        transaction?.Commit();
                    }
                }
            }
        }

        private Weapon Map(Weapon weapon, IDictionary<string, object> row)
        {
            weapon.Id            = Orm.MapGuid(row[nameof(weapon.Id)]);
            weapon.Name          = Orm.MapString(row[nameof(weapon.Name)]);
            weapon.Effectiveness = Orm.MapInt(row[nameof(weapon.Effectiveness)]);
            weapon.Defense       = Orm.MapInt(row[nameof(weapon.Defense)]);
            weapon.Speed         = Orm.MapInt(row[nameof(weapon.Speed)]);
            weapon.Stun          = Orm.MapInt(row[nameof(weapon.Stun)]);
            weapon.Properties    = (WeaponProperties)Orm.MapInt(row[nameof(weapon.Properties)]);
            return weapon;
        }

        private void AmendParameters(Weapon weapon, SqliteCommand cmd)
        {
            cmd.AddParameter($"@{nameof(weapon.Id)}",            weapon.Id);
            cmd.AddParameter($"@{nameof(weapon.Name)}",          weapon.Name);
            cmd.AddParameter($"@{nameof(weapon.Effectiveness)}", weapon.Effectiveness);
            cmd.AddParameter($"@{nameof(weapon.Defense)}",       weapon.Defense);
            cmd.AddParameter($"@{nameof(weapon.Speed)}",         weapon.Speed);
            cmd.AddParameter($"@{nameof(weapon.Stun)}",          weapon.Stun);
            cmd.AddParameter($"@{nameof(weapon.Properties)}",    weapon.Properties);
        }

        #endregion
    }
}
