using System;
using System.Net.Http;
using System.Threading.Tasks;
using ApprovalTests.Utilities;
using ApprovalUtilities.Persistence;
using ApprovalUtilities.Utilities;

namespace ApprovalTests.WebApi.MicrosoftHttpClient
{
	public abstract class RestQuery<T> : IExecutableQuery, ILoader<T>
	{
		public abstract string GetQuery();

		public abstract string GetBaseAddress();

		public string ExecuteQuery(string query)
		{
			var json = ExecuteAsync(query).Result.Content.ReadAsStringAsync().Result;
			return json.FormatJson();
		}

		public Task<HttpResponseMessage> ExecuteAsync(string requestUri)
		{
			var httpClient = new HttpClient();
			httpClient.BaseAddress = new Uri(GetBaseAddress());
			return httpClient.GetAsync(requestUri);
		}

		public Task<HttpResponseMessage> GetResponse()
		{
			return ExecuteAsync(GetQuery());
		}

		public abstract T Load();
	}
}