using System.Diagnostics;
using ApprovalTests.Namers;
using ApprovalTests.Reporters;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ApprovalTests.MachineSpecific.Tests.Namer
{
	[TestClass]
	public class GenericDiffReporterTest
	{
		public static void StartProcess(string fullCommandLine)
		{
			var splitPosition = fullCommandLine.IndexOf('"', 1);
			var fileName = fullCommandLine.Substring(1, splitPosition - 1);
			var arguments = fullCommandLine.Substring(splitPosition + 1);
			Process.Start(fileName, arguments);
		}


		[TestMethod]
		public void TestLaunchesBeyondCompareImage()
		{
			AssertLauncher("../../a.png", "../../b.png", BeyondCompareReporter.INSTANCE);
		}

		[TestMethod]
		public void TestLaunchesCodeCompare()
		{
			AssertLauncher("../../a.txt", "../../b.txt", CodeCompareReporter.INSTANCE);
		}

		[TestMethod]
		public void TestLaunchesKDiff()
		{
			AssertLauncher("../../a.txt", "../../b.txt", KDiffReporter.INSTANCE);
		}

		[TestMethod]
		public void TestLaunchesP4Merge()
		{
			AssertLauncher("../../a.txt", "../../b.txt", P4MergeTextReporter.INSTANCE);
		}

		[TestMethod]
		public void TestLaunchesP4MergeImage()
		{
			AssertLauncher("../../a.png", "../../b.png", P4MergeImageReporter.INSTANCE);
		}

		[TestMethod]
		public void TestLaunchesTortoiseImage()
		{
			AssertLauncher("../../a.png", "../../b.png", TortoiseImageDiffReporter.INSTANCE);
		}

		[TestMethod]
		public void TestLaunchesTortoiseMerge()
		{
			AssertLauncher("../../a.txt", "../../b.txt", TortoiseTextDiffReporter.INSTANCE);
		}

		[TestMethod]
		public void TestLaunchesVisualStudio()
		{
			ApprovalResults.UniqueForMachineName();
			AssertLauncher("../../a.txt", "../../b.txt", VisualStudioReporter.INSTANCE);
		}


		[TestMethod]
		public void TestWinMerge()
		{
			ApprovalResults.UniqueForMachineName();
			AssertLauncher("../../a.txt", "../../b.txt", WinMergeReporter.INSTANCE);
		}

		private static void AssertLauncher(string approved, string received, GenericDiffReporter reporter)
		{
			try
			{
				ApprovalResults.UniqueForMachineName();
				var args = reporter.GetLaunchArguments(approved, received);

				Approvals.VerifyWithCallback(args, s => StartProcess(s));
			}
			finally
			{
				NamerFactory.Clear();
			}
		}
	}
}