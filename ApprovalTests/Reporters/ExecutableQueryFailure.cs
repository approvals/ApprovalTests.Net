#if NETCORE
using System.IO;
#else
using Alphaleonis.Win32.Filesystem;
#endif
using ApprovalTests.Core;
using ApprovalUtilities.Persistence;

namespace ApprovalTests.Reporters
{

    public class ExecutableQueryFailure : IApprovalFailureReporter, IApprovalReporterWithCleanUp
    {
        private const string FileNameSuffix = ".queryresults.txt";
        private const string Header = "\t\tDo NOT approve\n\t\tThis File will be Deleted\n\t\tit is for feedback purposes only.\n\t\tAn additional file has been opened with only the query which you can approve.\n";
        private readonly IExecutableQuery query;
        private readonly IApprovalFailureReporter reporter;

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
            this.reporter.Report(approved, received);

            var receivedResult = this.ExecuteQuery(received);
            var approvedResult = this.ExecuteQuery(approved);

            if (string.IsNullOrEmpty(approvedResult.Result) && string.IsNullOrEmpty(receivedResult.Result))
            {
                return;
            }

            var r = RunQueryAndGetPath(received, receivedResult);
            var a = RunQueryAndGetPath(approved, approvedResult);
            this.reporter.Report(a, r);
        }

        private static string RunQueryAndGetPath(string fileName, QueryResult result)
        {
            var newFileName = fileName + FileNameSuffix;
            File.WriteAllText(newFileName, $"{Header}query:\n\n{result.Query}\n\nresult:\n\n{result.Result}");
            return newFileName;
        }

        private QueryResult ExecuteQuery(string fileName)
        {
            if (!File.Exists(fileName))
            {
                return new QueryResult();
            }

            var newQuery = File.ReadAllText(fileName).Trim();
            var newResult = this.query.ExecuteQuery(newQuery);
            return new QueryResult { Query = newQuery, Result = newResult };
        }
    }
}