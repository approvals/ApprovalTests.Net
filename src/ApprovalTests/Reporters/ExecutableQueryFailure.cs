using ApprovalUtilities.Persistence;

namespace ApprovalTests.Reporters;

public class ExecutableQueryFailure : IApprovalFailureReporter, IApprovalReporterWithCleanUp
{
    const string FileNameSuffix = ".queryresults.txt";
    const string Header = "\t\tDo NOT approve\n\t\tThis File will be Deleted\n\t\tit is for feedback purposes only.\n\t\tAn additional file has been opened with only the query which you can approve.\n";
    readonly IExecutableQuery query;
    readonly IApprovalFailureReporter reporter;

    public ExecutableQueryFailure(IExecutableQuery query, IApprovalFailureReporter reporter)
    {
        this.query = query;
        this.reporter = reporter;
    }

    public void CleanUp(string approved, string received)
    {
        File.Delete(approved + FileNameSuffix);
        File.Delete(received + FileNameSuffix);
    }

    public void Report(string approved, string received)
    {
        reporter.Report(approved, received);

        var receivedResult = ExecuteQuery(received);
        var approvedResult = ExecuteQuery(approved);

        if (string.IsNullOrEmpty(approvedResult.Result) && string.IsNullOrEmpty(receivedResult.Result))
        {
            return;
        }

        var r = RunQueryAndGetPath(received, receivedResult);
        var a = RunQueryAndGetPath(approved, approvedResult);
        reporter.Report(a, r);
    }

    static string RunQueryAndGetPath(string fileName, QueryResult result)
    {
        var newFileName = fileName + FileNameSuffix;
        File.WriteAllText(newFileName, $"{Header}query:\n\n{result.Query}\n\nresult:\n\n{result.Result}");
        return newFileName;
    }

    QueryResult ExecuteQuery(string fileName)
    {
        if (!File.Exists(fileName))
        {
            return new();
        }

        var newQuery = File.ReadAllText(fileName).Trim();
        var newResult = query.ExecuteQuery(newQuery);
        return new() { Query = newQuery, Result = newResult };
    }
}