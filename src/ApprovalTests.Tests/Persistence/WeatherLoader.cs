using System.Net;

class WeatherLoader : IExecutableQuery
{
    readonly string weatherStationId;

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
        var Url = "http://api.wunderground.com/weatherstation/WXCurrentObXML.asp";
        return new WebClient().DownloadString(Url + "?" + query);
    }
}