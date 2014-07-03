using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ApprovalTests.Namers.StackTraceParsers;
using NUnit.Framework;

namespace ApprovalTests.Tests.Namer.StackTraceParsers
{
	[TestFixture]
	public class StackTraceParserTests
	{
		[Test]
		public void Parse_UsingStaticInitialize_DontThrowInvalidOperationException()
		{
			var parser = new StackTraceParser();

			try
			{
				Parallel.ForEach(Enumerable.Range(1, 20), (_) =>
					{
						try
						{
							var stackTrace = new StackTrace();
							parser.Parse(stackTrace);
						}
						catch (InvalidOperationException e)
						{
							Assert.Fail(
								"InvalidOperationException when trying to parse stacktrace. " +
								"This is caused by the parser collection not being thread-safe. " +
								"Original exception message : {0} and stacktrace : {1}",
								e.Message,
								e.StackTrace
								);
						}
							// Because the current stacktrace passed to the parse method doesn't contains any trace of a compliant stacktrace parser
							// it's normal that we receive an exception here so let's ignore it.
						catch (Exception e)
						{
							if (
								!e.Message.Contains("Approvals is not set up to use your test framework"))
							{
								throw;
							}
						}
					});
			}
			catch (AggregateException e)
			{
				// Throw the first inner exception of the AggretateException, this way NUnit shows a much clearer result.
				throw e.InnerException;
			}
		}
	}
}