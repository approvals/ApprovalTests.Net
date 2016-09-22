using System;
using System.IO;
using ApprovalTests.Core;

namespace ApprovalTests.Utilities
{
    public static class StringReporting
    {
        public static void DiffWith(this string expected, string actual)
        {
            AssertEqual(expected, actual, Approvals.GetReporter());
        }

        public static void AssertEqual(string expected, string actual, IApprovalFailureReporter reporter)
        {
            if (expected != actual)
            {
                var expectedFile = Path.GetTempPath() + "Expected.Approvals.Temp.txt";
                var actualFile = Path.GetTempPath() + "Actual.Approvals.Temp.txt";

                File.WriteAllText(expectedFile, expected);
                File.WriteAllText(actualFile, actual);

                reporter.Report(expectedFile, actualFile);
                throw new Exception($"<{expected}> != <{actual}>");
            }
        }
    }
}