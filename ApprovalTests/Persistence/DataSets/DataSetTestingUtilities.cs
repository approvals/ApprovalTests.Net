using System.Data;
using System.Linq;

namespace ApprovalTests.Persistence.DataSets
{
	public static class DataSetTestingUtilities
	{
		public static DataTable AddTestDataRows(this DataTable table, int count = 1)
		{
			var defaults = new ColumnDefaults();
			for (var i = 0; i < count; i++)
			{
				table.AddTestDataRow(defaults);
			}
			return table;
		}

		private static void AddTestDataRow(this DataTable table, ColumnDefaults defaults)
		{
			var row = table.NewRow();
			foreach (var column in table.Columns.Cast<DataColumn>())
			{
				row[column] = defaults.GetDefaultValue(column);
			}
			table.Rows.Add(row);
		}
	}
}