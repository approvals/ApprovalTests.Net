using System.Net;
using ApprovalTests.WebApi.MicrosoftHttpClient;

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
    readonly string searchTerm;

    public GoogleQuery(string searchTerm) =>
        this.searchTerm = searchTerm;

    public override string GetQuery() =>
        $"?v=1.0&q={searchTerm}";

    public override string GetBaseAddress() =>
        "http://ajax.googleapis.com/ajax/services/search/web";

    public override GoogleQueryResults Load() =>
        new(GetResponse().Result);
}

public class GoogleQueryResults
{
    // ReSharper disable once UnusedParameter.Local
    public GoogleQueryResults(DownloadStringCompletedEventArgs result)
    {
        //do extraction stuff
    }
}