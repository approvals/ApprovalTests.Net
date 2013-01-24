using System.Net;
using ApprovalUtilities.Persistence;
using NUnit.Framework;

namespace ApprovalTests.Tests.Persistence
{
	[TestFixture]
	public class WeatherTest
	{
		[Test]
		public void TestWeather()
		{
			Approvals.Verify(new WeatherLoader("KCASANDI69"));
		}
	}
	internal class WeatherLoader : IExecutableQuery
	{
		private readonly string weatherStationId;

		public WeatherLoader(string weatherStationId)
		{
			this.weatherStationId = weatherStationId;
		}

		public string GetQuery()
		{
			return "ID=" + weatherStationId;
		}

		public string ExecuteQuery(string query)
		{
			string Url = "http://api.wunderground.com/weatherstation/WXCurrentObXML.asp";
			return new WebClient().DownloadString(Url + "?" + query);
		}
	}
}