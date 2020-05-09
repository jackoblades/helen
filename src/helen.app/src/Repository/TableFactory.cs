using System.Text;

namespace Helen.App.Repository
{
    public class TableFactory
    {
        #region Properties

        private StringBuilder _sql;
        private string _name;

        #endregion

        #region Constructors

        public TableFactory(string name)
        {
            _name = name;
            _sql = new StringBuilder($"CREATE TABLE IF NOT EXISTS \"{_name}\" (");
        }

        #endregion

        #region Methods

        /// <summary>
        /// "Id" BLOB NOT NULL CONSTRAINT "PK_MyTable" PRIMARY KEY, 
        /// </summary>
        public TableFactory Id(string id = "Id")
        {
            _sql.Append($"\"{id}\" BLOB NOT NULL CONSTRAINT \"PK_{_name}\" PRIMARY KEY, ");
            return this;
        }

        /// <summary>
        /// "MyColumn" TYPE (NOT) NULL, 
        /// </summary>
        public TableFactory Column(string column, OrmType type, bool isNull)
        {
            string nullClause = isNull ? "NULL" : "NOT NULL";
            _sql.Append($"\"{column}\" {type.ToSql()} {nullClause}, ");
            return this;
        }

        /// <summary>
        /// Closes and returns the final sql command string.
        /// </summary>
        public string Build()
        {
            _sql.Append(")");
            _sql.Replace(", )", ")");
            return _sql.ToString();
        }

        #endregion
    }
}
