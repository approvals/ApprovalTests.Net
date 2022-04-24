using ApprovalTests.Core;
using ApprovalTests.Namers.StackTraceParsers;
using ApprovalTests.Tests.Reporters;
using ApprovalTests.Writers;
using ApprovalUtilities.CallStack;
using NUnit.Framework;

namespace ApprovalTests.Tests.CustomImplementation;

[TestFixture]
public class CustomNamerShouldBeSubstitutableTest
{
    /// <summary>
    /// Test for Issue #140
    /// https://github.com/approvals/ApprovalTests.Net/issues/140
    /// </summary>
    [Test]
    public void CustomNamerShouldNotDependOnSetCallerTest()
    {
        var approvalText = "CustomNamerShouldBeSubstitutable";

        var writer = WriterFactory.CreateTextWriter(approvalText);
        var namer = new CustomNamer();
        var reporter = new MethodLevelReporter();

        Approvals.Verify(writer, namer, reporter);
    }

    [Test]
    public void CustomNamerShouldBeSubstitutable()
    {
        // just here to prevent false detection as abandoned file via Maintenance test
    }

    class CustomNamer : IApprovalNamer
    {
        public string Name => "CustomNamerShouldBeSubstitutableTest.CustomNamerShouldBeSubstitutable";

        public string SourcePath
        {
            get
            {
                var stackTraceParser = new StackTraceParser();
                stackTraceParser.Parse(new Caller().StackTrace);
                return stackTraceParser.SourcePath;
            }
        }
    }
}