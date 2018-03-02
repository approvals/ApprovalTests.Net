using System;
using System.Collections.Generic;
using System.Data;

namespace ApprovalTests.Persistence.DataSets
{
	public class ColumnDefaults
	{
		private int integer = 1;
		private DateTime date = new DateTime(2001, 01, 11);
		private bool boolean = false;

		private int GetNextInteger()
		{
			return integer++;
		}

		private double GetNextDouble()
		{
			return (integer++) + 0.5;
		}

		private bool GetNextBoolean()
		{
			var r = boolean;
			boolean = !boolean;
			return r;
		}

		private DateTime GetNextDate()
		{
			var r = date;
			date = date.AddYears(1).AddMonths(1).AddDays(1);
			return r;
		}

		public object GetDefaultValue(DataColumn column)
		{
			object defaultValue = column.ColumnName;
			if (column.DataType == typeof (int))
			{
				defaultValue = GetNextInteger();
			}
			else if (new List<Type> {typeof (decimal), typeof (double)}.Contains(column.DataType))
			{
				defaultValue = GetNextDouble();
			}
			else if (column.DataType == typeof (bool))
			{
				defaultValue = GetNextBoolean();
			}
			else if (column.DataType == typeof (DateTime))
			{
				defaultValue = GetNextDate();
			}
			else if (column.DataType == typeof (string))
			{
				defaultValue = column.ColumnName.Substring(0, Math.Min(column.ColumnName.Length, column.MaxLength));
			}
			return defaultValue;
		}
	}
}