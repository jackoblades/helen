using Microsoft.Data.Sqlite;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using System.Threading.Tasks;

namespace Helen.App.Extensions
{
    public static class SqliteExtensions
    {
        public static void GenerateUpsert(this SqliteCommand command, string table)
        {
            var paramsCmd = new StringBuilder($"INSERT INTO {table} (");
            var valuesCmd = new StringBuilder("VALUES (");
            var updateCmd = new StringBuilder("DO UPDATE SET ");

            // Fill up command string fragments for each parameter already in the command.
            foreach (SqliteParameter parameter in command.Parameters)
            {
                var column = parameter.ParameterName.Substring(1);
                paramsCmd.Append($"{column}, ");
                valuesCmd.Append($"{parameter.ParameterName}, ");
                updateCmd.Append($"{column}=excluded.{column}, ");
            }

            // Close each command string fragment.
            paramsCmd.Append(")").Replace(", )", ")");
            valuesCmd.Append(")").Replace(", )", ")");
            updateCmd.Append(")").Replace(", )", "");

            // Set command text.
            command.CommandText = $"{paramsCmd} {valuesCmd} ON CONFLICT (Id) {updateCmd}";
        }

        public static async Task ExecuteScalarAsync(this SqliteConnection db, string sql)
        {
            using (var command = new SqliteCommand(sql, db))
            {
                await command.ExecuteScalarAsync();
            }
        }

        public static async Task ExecuteNonQueryAsync(this SqliteConnection db, string sql)
        {
            using (var command = new SqliteCommand(sql, db))
            {
                await command.ExecuteNonQueryAsync();
            }
        }

        public static void AddParameter(this SqliteCommand command, string name, object value)
        {
            if (value != null)
            {
                command.Parameters.Add(new SqliteParameter(name, value));
            }
        }

        public static IEnumerable<Dictionary<string, object>> ReadValues(this DbDataReader reader)
        {
            var result = new List<Dictionary<string, object>>();

            while (reader.Read())
            {
                var item  = new Dictionary<string, object>();

                for (int i = 0; i < reader.FieldCount; ++i)
                {
                    item.Add(reader.GetName(i), reader.GetValue(i));
                }

                result.Add(item);
            }

            return result;
        }
    }
}
