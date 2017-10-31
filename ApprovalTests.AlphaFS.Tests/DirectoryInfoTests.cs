using System;
using System.Collections.Generic;
using System.Linq;
using Alphaleonis.Win32.Filesystem;
using NUnit.Framework;
using IODirectoryInfo = System.IO.DirectoryInfo;

namespace ApprovalTests.AlphaFS.Tests
{
	[TestFixture]
	public class DirectoryInfoTests
	{
		[Test]
		[TestCaseSource(nameof(MaskTestCases))]
		public void GetFiles(string mask)
		{
			var alphaFS = new DirectoryInfo(ProjectDirectories.AlphaFSTestDirectoryPath).GetFiles(mask).Select(x => x.FullName).ToArray();
			var io = new IODirectoryInfo(ProjectDirectories.AlphaFSTestDirectoryPath).GetFiles(mask).Select(x => x.FullName).ToArray();

			Assert.That(alphaFS, Is.EqualTo(io));
		}

		[Test]
		[TestCaseSource(nameof(MaskTestCases))]
		public void EnumerateFiles(string mask)
		{
			var alphaFS = new DirectoryInfo(ProjectDirectories.AlphaFSTestDirectoryPath).EnumerateFiles(mask).Select(x => x.FullName).ToArray();
			var io = new IODirectoryInfo(ProjectDirectories.AlphaFSTestDirectoryPath).EnumerateFiles(mask).Select(x => x.FullName).ToArray();

			Assert.That(alphaFS, Is.EqualTo(io));
		}

		[Test]
		[TestCaseSource(nameof(MaskTestCases))]
		public void GetLatestFile(string mask)
		{
			var alphaFS = new DirectoryInfo(ProjectDirectories.AlphaFSTestDirectoryPath).GetFiles(mask).OrderBy(f => f.CreationTime).LastOrDefault()?.FullName;
			var io = new IODirectoryInfo(ProjectDirectories.AlphaFSTestDirectoryPath).GetFiles(mask).OrderBy(f => f.CreationTime).LastOrDefault()?.FullName;

			Assert.That(alphaFS, Is.EqualTo(io));
		}

		[Test]
		[TestCaseSource(nameof(MaskTestCases))]
		public void FirstOrDefault(string mask)
		{
			var alphaFS = new DirectoryInfo(ProjectDirectories.AlphaFSTestDirectoryPath).EnumerateFiles(mask).FirstOrDefault()?.FullName;
			var io = new IODirectoryInfo(ProjectDirectories.AlphaFSTestDirectoryPath).EnumerateFiles(mask).FirstOrDefault()?.FullName;

			Assert.That(alphaFS, Is.EqualTo(io));
		}

		[Test]
		[TestCaseSource(nameof(DirectoryTestCases))]
		public void Exists(string directory)
		{
			var alphaFS = new DirectoryInfo(directory).Exists;
			var io = new IODirectoryInfo(directory).Exists;

			Assert.That(alphaFS, Is.EqualTo(io));
		}

		[Test]
		public void CreateDirectory()
		{
			var dirName = new Random(31).Next().ToString();
			var temp = Path.GetTempPath() + dirName;
			var info = new DirectoryInfo(temp);
			info.Create();

			Assert.That(info.Exists, Is.True);
			Assert.That(info.Name, Is.EqualTo(dirName));
		}

		[Test]
		public void Ctor()
		{
			var alphaFS = new DirectoryInfo(ProjectDirectories.AlphaFSTestDirectoryPath);
			var io = new IODirectoryInfo(ProjectDirectories.AlphaFSTestDirectoryPath);

			AssertEquals(alphaFS, io);
		}

		[Test]
		[TestCaseSource(nameof(FilepathTestCases))]
		public void Parent(string filepath)
		{
			var alphaFS = new DirectoryInfo(filepath).Parent;
			var io = new IODirectoryInfo(filepath).Parent;

			if (alphaFS != null && io != null)
				AssertEquals(alphaFS, io);
			else
			{
				Assert.That(alphaFS, Is.Null);
				Assert.That(io, Is.Null);
			}
		}

		[Test]
		[TestCaseSource(nameof(FilepathTestCases))]
		public void Root(string filepath)
		{
			var alphaFS = new DirectoryInfo(filepath).Root;
			var io = new IODirectoryInfo(filepath).Root;

			AssertEquals(alphaFS, io);
		}

		private void AssertEquals(DirectoryInfo alphaFS, IODirectoryInfo io)
		{
			Assert.That(alphaFS.Exists, Is.EqualTo(io.Exists));
			Assert.That(alphaFS.Name, Is.EqualTo(io.Name));
			Assert.That(alphaFS.FullName, Is.EqualTo(io.FullName));
			Assert.That(alphaFS.CreationTime, Is.EqualTo(io.CreationTime));
			Assert.That(alphaFS.CreationTimeUtc, Is.EqualTo(io.CreationTimeUtc));
			Assert.That(alphaFS.Extension, Is.EqualTo(io.Extension));
			Assert.That(alphaFS.LastAccessTime, Is.EqualTo(io.LastAccessTime));
			Assert.That(alphaFS.LastAccessTimeUtc, Is.EqualTo(io.LastAccessTimeUtc));
			Assert.That(alphaFS.LastWriteTime, Is.EqualTo(io.LastWriteTime));
			Assert.That(alphaFS.LastWriteTimeUtc, Is.EqualTo(io.LastWriteTimeUtc));
			Assert.That((int)alphaFS.Attributes, Is.EqualTo((int)io.Attributes));
		}

		private static IEnumerable<TestCaseData> MaskTestCases
		{
			get
			{
				yield return new TestCaseData("*").SetName("*");
				yield return new TestCaseData("*.*").SetName("*.*");
				yield return new TestCaseData("*.cs").SetName("*.cs");
				yield return new TestCaseData("*.csproj").SetName("*.csproj");
				yield return new TestCaseData("").SetName("empty");
				yield return new TestCaseData("unknown").SetName("unknown");
			}
		}

		private static IEnumerable<TestCaseData> DirectoryTestCases
		{
			get
			{
				yield return new TestCaseData("unexistingDirectory").SetName("unexistingDirectory");
				yield return new TestCaseData(ProjectDirectories.AlphaFSTestDirectoryPath).SetName("ApprovalTests.AlphaFS.Tests");
			}
		}

		private static IEnumerable<TestCaseData> FilepathTestCases
		{
			get
			{
				yield return new TestCaseData(@"aa\b\c\file.tmp").SetName(@"aa\b\c\file.tmp");
				yield return new TestCaseData(@"x:\a\b\file.n").SetName(@"x:\a\b\file.n");
				yield return new TestCaseData(@"a\.\b\file.ss").SetName(@"a\.\b\file.ss");
				yield return new TestCaseData(@"a\..\b\c\file.").SetName(@"a\..\b\c\file.");
				yield return new TestCaseData(@"a\b\c\d\").SetName(@"a\b\c\d\");
				yield return new TestCaseData(@"x:\a\b\c").SetName(@"x:\a\b\c");
				yield return new TestCaseData(@"x:\").SetName(@"x:\");
				yield return new TestCaseData(@"a").SetName("a");
				yield return new TestCaseData(@"a\").SetName(@"a\");
			}
		}
	}
}