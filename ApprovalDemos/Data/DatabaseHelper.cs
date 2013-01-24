using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace ApprovalDemos.Data
{
	public class DatabaseHelper
	{
		public static T ExecuteQueryExtracted<T>(string query, string connString, Func<IDataReader, T> DataTransformer)
		{
			using (var conn = new SqlConnection(connString))
			{
				conn.Open();
				SqlCommand cmd = conn.CreateCommand();
				cmd.CommandText = query;

				using (SqlDataReader reader = cmd.ExecuteReader())
				{
					return DataTransformer(reader);
				}
			}
		}

		public static string AsString(IDataReader reader)
		{
			var sb = new StringBuilder();
			while (reader.Read())
			{
				string comma = null;
				for (int i = 0; i < reader.FieldCount; i++)
				{
					sb.AppendFormat("{0}{1}", comma, reader[i]);
					comma = ",";
				}
				sb.Append("\r\n");
			}

			return sb.ToString();
		}
	}
}