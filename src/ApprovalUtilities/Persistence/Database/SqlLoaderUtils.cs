using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using ApprovalUtilities.Utilities;

namespace ApprovalUtilities.Persistence.Database
{
    public class SqlLoaderUtils
    {
        public static string ExecuteQueryToDisplayString(string query, DbConnection conn)
        {
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }

            return ExecuteQueryToDisplayString(query, null, conn.CreateCommand);
        }

        public static string ExecuteQueryToDisplayString(
            string query,
            string connectionString,
            Func<DbCommand> commandCreator)
        {
            if (string.IsNullOrEmpty(query))
            {
                return string.Empty;
            }

            try
            {
                string[] dataset = null;
                var headers = new StringBuilder();
                if (connectionString != null)
                {
                    dataset = DatabaseUtils.Query(query, connectionString, r => ConvertRowToString(r, headers)).ToArray();
                }
                else if (commandCreator != null)
                {
                    dataset = DatabaseUtils.Query(query, commandCreator, r => ConvertRowToString(r, headers)).ToArray();
                }

                return headers + "\n" + string.Join("\n", dataset.OrEmpty().ToArray());
            }
            catch (Exception ex)
            {
                return ExceptionUtilities.FormatException(ex);
            }
        }

        public static string ConvertRowToString(DbDataReader row, StringBuilder headers)
        {
            var output = new List<string>();
            for (var i = 0; i < row.FieldCount; i++)
            {
                output.Add(string.Empty + row.GetValue(i));
            }

            if (headers.Length == 0)
            {
                for (var i = 0; i < row.FieldCount; i++)
                {
                    headers.Append(row.GetName(i) + ", ");
                }
            }

            return string.Join(", ", output.ToArray());
        }
    }
}