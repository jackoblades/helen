namespace Helen.App.Repository
{
    public enum OrmType
    {
        Invalid = 0,
        Int,
        Real,
        Blob,
        Text,
    }

    public static class OrmTypeExtensions
    {
        public static string ToString(this OrmType type)
        {
            return (type == OrmType.Int)  ? "INTEGER"
                 : (type == OrmType.Real) ? "REAL"
                 : (type == OrmType.Blob) ? "BLOB"
                 : (type == OrmType.Text) ? "TEXT"
                 : "INVALID";
        }
    }
}
