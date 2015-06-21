using System.Diagnostics;

namespace ApprovalTests.Namers.StackTraceParsers
{
	public interface IStackTraceParser
	{
		string ApprovalName { get; }
		string SourcePath { get; }
		string RootNamespace { get; }
		string RootPath { get; }
		string Namespace { get; }
		string ForTestingFramework { get; }
		bool Parse(StackTrace stackTrace);
	}
}