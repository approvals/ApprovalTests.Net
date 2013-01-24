using System.IO;
using ApprovalTests.Core;
using ApprovalUtilities.Persistence;

namespace ApprovalTests.Reporters
{
	public class ExecutableQueryFailure : IApprovalFailureReporter, IApprovalReporterWithCleanUp
	{
		private const string FILE_ADDITION = ".queryresults.txt";
		private readonly IExecutableQuery query;
		private readonly IApprovalFailureReporter reporter;

		public ExecutableQueryFailure(IExecutableQuery query, IApprovalFailureReporter reporter)
		{
			this.query = query;
			this.reporter = reporter;
		}

		public void Report(string approved, string received)
		{
			reporter.Report(approved, received);

			var rResult = ExecuteQuery(received);
			var aResult = ExecuteQuery(approved);

			if (string.IsNullOrEmpty(aResult.Result) && string.IsNullOrEmpty(rResult.Result))
			{
				return;
			}

			var r = RunQueryAndGetPath(received, rResult);
			var a = RunQueryAndGetPath(approved, aResult);
			reporter.Report(a, r);
		}

		private string RunQueryAndGetPath(string fileName, QueryResult qResult)
		{
			var newFileName = fileName + FILE_ADDITION;
			var header = "\t\tDo NOT approve\r\n\t\tThis File will be Deleted\r\n\t\tit is for feedback purposes only.\r\n\t\tAn additional file has been opened with only the query which you can approve.\r\n";
			File.WriteAllText(newFileName, string.Format("{0}query:\r\n\r\n{1}\r\n\r\nresult:\r\n\r\n{2}", header, qResult.Query, qResult.Result));
			return newFileName;
		}
		private QueryResult ExecuteQuery(string fileName)
		{
			if (!File.Exists(fileName))
			{
				return new QueryResult();
			}
			var newQuery = File.ReadAllText(fileName).Trim();
			var newResult = query.ExecuteQuery(newQuery);
			return new QueryResult() { Query = newQuery, Result = newResult };
		}

		public void CleanUp(string approved, string received)
		{
			File.Delete(approved + FILE_ADDITION);
			File.Delete(received + FILE_ADDITION);
		}
	}
	public class QueryResult
	{
		public string Query { get; set; }
		public string Result { get; set; }
	}
}