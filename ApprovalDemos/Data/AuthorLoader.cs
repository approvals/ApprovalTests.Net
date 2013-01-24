using System.Collections.Generic;

namespace ApprovalDemos.Data
{
	internal class AuthorLoader : IAdoLoader<IEnumerable<Author>>
	{
		#region IAdoLoader<IEnumerable<Author>> Members

		public string Query
		{
			get { return "SELECT * FROM Authors a"; }
		}

		public IEnumerable<Author> Load()
		{
			return DatabaseHelper.ExecuteQueryExtracted<IEnumerable<Author>>(
				Query, ConnectionString, Author.Load);
		}

		public string ConnectionString
		{
			get { return @"server=.\sqlexpress;database=pubs;trusted_connection=true"; }
		}

		#endregion
	}
}