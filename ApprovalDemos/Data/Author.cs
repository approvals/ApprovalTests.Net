using System;
using System.Collections.Generic;
using System.Data;

namespace ApprovalDemos.Data
{
	public class Author
	{
		public string ID { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }

		public override string ToString()
		{
			return string.Format("{0} {1}", FirstName, LastName);
		}

		public static IEnumerable<Author> Load(IDataReader arg)
		{
			return null;
		}
	}
}