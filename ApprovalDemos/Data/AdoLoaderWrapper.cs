using ApprovalUtilities.Persistence;

namespace ApprovalDemos.Data
{
	internal class AdoLoaderWrapper<T> : IExecutableQuery
	{
		public IAdoLoader<T> Loader { get; set; }

		public AdoLoaderWrapper(IAdoLoader<T> loader)
		{
			Loader = loader;
		}

		public string GetQuery()
		{
			return Loader.Query;
		}

		public string ExecuteQuery(string query)
		{
			return DatabaseHelper.ExecuteQueryExtracted<string>(
				query,
				Loader.ConnectionString,
				DatabaseHelper.AsString );
		}
	}
}
