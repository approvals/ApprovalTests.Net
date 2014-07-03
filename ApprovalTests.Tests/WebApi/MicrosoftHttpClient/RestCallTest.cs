using System.Net;
using ApprovalTests.WebApi.MicrosoftHttpClient;
using NUnit.Framework;

namespace ApprovalTests.Tests.WebApi.MicrosoftHttpClient
{
	[TestFixture]
	public class RestCallTest
	{
		[Test]
		public void TestGoogleQuery()
		{
			Approvals.Verify(new GoogleQuery("lolcats"));
		}
	}

	public class GoogleQuery : RestQuery<GoogleQueryResults>
	{
		private readonly string searchTerm;

		public GoogleQuery(string searchTerm)
		{
			this.searchTerm = searchTerm;
		}

		public override string GetQuery()
		{
			return string.Format("?v=1.0&q={0}", searchTerm);
		}

		public override string GetBaseAddress()
		{
			return "http://ajax.googleapis.com/ajax/services/search/web";
		}

		public override GoogleQueryResults Load()
		{
			return new GoogleQueryResults(GetResponse().Result);
		}
	}

	public class GoogleQueryResults
	{
		public GoogleQueryResults(DownloadStringCompletedEventArgs result)
		{
			//do extraction stuff
		}
	}
}