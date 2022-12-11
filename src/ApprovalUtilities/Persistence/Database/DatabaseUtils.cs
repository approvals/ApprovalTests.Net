using System.Data.Common;
using System.Data.SqlClient;

namespace ApprovalUtilities.Persistence.Database;

public static class DatabaseUtils
{
    public static List<T> Query<T>(string sql, string connection, Func<DbDataReader, T> converter)
    {
        using var conn = new SqlConnection(connection);
        conn.Open();
        return Query(sql, conn.CreateCommand, converter);
    }

    public static List<T> Query<T>(string sql, Func<DbCommand> commandCreator, Func<DbDataReader, T> converter)
    {
        using var command = commandCreator();
        command.CommandText = sql;
        using var reader = command.ExecuteReader();
        var list = new List<T>();
        while (reader.Read())
        {
            list.Add(converter(reader));
        }

        return list;
    }
}