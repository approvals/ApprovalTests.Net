using System.Net;

class WeatherLoader : IExecutableQuery
{
    readonly string weatherStationId;

    public WeatherLoader(string weatherStationId) =>
        this.weatherStationId = weatherStationId;

    public string GetQuery() =>
        "ID=" + weatherStationId;

    public string ExecuteQuery(string query)
    {
        var Url = "http://api.wunderground.com/weatherstation/WXCurrentObXML.asp";
#pragma warning disable SYSLIB0014
        return new WebClient().DownloadString(Url + "?" + query);
#pragma warning restore SYSLIB0014
    }
}