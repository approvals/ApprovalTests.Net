using System.Diagnostics;
using ApprovalTests.Namers;
using ApprovalTests.Reporters;
using NUnit.Framework;

namespace ApprovalTests.MachineSpecific.Tests.Reporters
{
    [TestFixture]
    public class GenericDiffReporterTest
    {
        public static void StartProcess(string fullCommandLine)
        {
            var splitPosition = fullCommandLine.IndexOf('"', 1);
            var fileName = fullCommandLine.Substring(1, splitPosition - 1);
            var arguments = fullCommandLine.Substring(splitPosition + 1);
            Process.Start(fileName, arguments);
        }

        [Test]
        public void TestLaunchesBeyondCompareImage()
        {
            AssertLauncher("../../a.png", "../../b.png", BeyondCompare3Reporter.INSTANCE);
        }

        [Test]
        public void TestLaunchesCodeCompare()
        {
            AssertLauncher("../../a.txt", "../../b.txt", CodeCompareReporter.INSTANCE);
        }

        [Test]
        public void TestLaunchesKDiff()
        {
            AssertLauncher("../../a.txt", "../../b.txt", KDiffReporter.INSTANCE);
        }

        [Test]
        public void TestLaunchesP4Merge()
        {
            AssertLauncher("../../a.txt", "../../b.txt", P4MergeTextReporter.INSTANCE);
        }

        [Test]
        public void TestLaunchesP4MergeImage()
        {
            AssertLauncher("../../a.png", "../../b.png", P4MergeImageReporter.INSTANCE);
        }

        [Test]
        public void TestLaunchesTortoiseImage()
        {
            AssertLauncher("../../a.png", "../../b.png", TortoiseImageDiffReporter.INSTANCE);
        }

        [Test]
        public void TestLaunchesTortoiseMerge()
        {
            AssertLauncher("../../a.txt", "../../b.txt", TortoiseTextDiffReporter.INSTANCE);
        }

        [Test]
        public void TestLaunchesVisualStudio()
        {
            using (ApprovalResults.UniqueForMachineName())
            {
                AssertLauncher("../../a.txt", "../../b.txt", VisualStudioReporter.INSTANCE);
            }
        }

        [Test]
        public void TestWinMerge()
        {
            using (ApprovalResults.UniqueForMachineName())
            {
                AssertLauncher("../../a.txt", "../../b.txt", WinMergeReporter.INSTANCE);
            }
        }

        private static void AssertLauncher(string approved, string received, GenericDiffReporter reporter)
        {
            using (ApprovalResults.UniqueForMachineName())
            {
                var args = reporter.GetLaunchArguments(approved, received);

                Approvals.VerifyWithCallback(args, s => StartProcess(s));
            }
        }
    }
}