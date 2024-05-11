using ApprovalTests.Core;
using ApprovalUtilities.Persistence;

namespace ApprovalTests.Reporters;

public class ExecutableQueryFailure(IExecutableQuery query, IApprovalFailureReporter reporter) :
    IApprovalFailureReporter, IApprovalReporterWithCleanUp
{
    const string FileNameSuffix = ".queryresults.txt";
    const string Header = """
                          		Do NOT approve
                          		This File will be Deleted
                          		it is for feedback purposes only.
                          		An additional file has been opened with only the query which you can approve.

                          """;

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