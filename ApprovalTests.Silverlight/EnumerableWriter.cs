using System;
using System.Collections.Generic;
using System.Text;

namespace ApprovalTests.Silverlight
{
	public class EnumerableWriter
	{
		#region Delegates

		public delegate string CustomFormatter<T>(T item);

		public delegate string CustomFormatterWithIndex<T>(int index, T item);

		#endregion

		public static string Write<T>(IEnumerable<T> enumerable, String label)
		{
			return Write(enumerable, label, s => s.ToString());
		}

		public static string Write<T>(IEnumerable<T> enumerable, string label, CustomFormatter<T> formatter)
		{
			return Write(enumerable, (i, s) => string.Format("{0}[{1}] = {2}" + Environment.NewLine, label, i, formatter(s)),
			             string.Format("{0} is empty", label));
		}

		public static string Write<T>(IEnumerable<T> enumerable, CustomFormatter<T> formatter)
		{
			return Write(enumerable, (i, s) => formatter(s), "Empty");
		}

		public static string Write<T>(IEnumerable<T> enumerable, CustomFormatterWithIndex<T> formatter, string emptyMessage)
		{
			var list = new List<T>(enumerable);

			if (list.Count == 0)
				return emptyMessage;

			var sb = new StringBuilder();
			int i = 0;
			list.ForEach(item => sb.Append(formatter(i++, item)));

			return sb.ToString();
		}
	}
}