using System.Collections.Generic;
using System.Linq;
using Alphaleonis.Win32.Filesystem;
using NUnit.Framework;
using IOFileInfo = System.IO.FileInfo;
using IODirectoryInfo = System.IO.DirectoryInfo;

namespace ApprovalTests.AlphaFS.Tests
{
	[TestFixture]
	public class FileInfoTests
	{
		[Test]
		[TestCaseSource(nameof(FilepathTestCases))]
		public void Extension(string filepath)
		{
			var alphaFS = new FileInfo(filepath).Extension;
			var io = new IOFileInfo(filepath).Extension;

			Assert.That(alphaFS, Is.EqualTo(io));
		}

		[Test]
		[TestCaseSource(nameof(FileTestCases))]
		public void Exists(string filepath)
		{
			var alphaFS = new FileInfo(filepath).Exists;
			var io = new IOFileInfo(filepath).Exists;

			Assert.That(alphaFS, Is.EqualTo(io));
		}

		[Test]
		[TestCaseSource(nameof(ExistingFileTestCases))]
		public void Length(string filepath)
		{
			var alphaFS = new FileInfo(filepath).Length;
			var io = new IOFileInfo(filepath).Length;

			Assert.That(alphaFS, Is.EqualTo(io));
		}

		[Test]
		[TestCaseSource(nameof(ExistingFileTestCases))]
		public void Directory(string filepath)
		{
			var alphaFS = new FileInfo(filepath).Directory;
			var io = new IOFileInfo(filepath).Directory;

			AssertEquals(alphaFS, io);
		}

		[Test]
		[TestCaseSource(nameof(ExistingFileTestCases))]
		public void Ctor(string filepath)
		{
			var alphaFS = new FileInfo(filepath);
			var io = new IOFileInfo(filepath);


			Assert.That(alphaFS.DirectoryName, Is.EqualTo(io.DirectoryName));
			Assert.That(alphaFS.Length, Is.EqualTo(io.Length));
			Assert.That(alphaFS.IsReadOnly, Is.EqualTo(io.IsReadOnly));
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
			}
		}

		private static IEnumerable<TestCaseData> FileTestCases => UnexistingFileTestCases.Concat(ExistingFileTestCases);

		private static IEnumerable<TestCaseData> UnexistingFileTestCases
		{
			get
			{
				yield return new TestCaseData("unexistingFile.tmp").SetName("unexistingFile.tmp");
			}
		}

		private static IEnumerable<TestCaseData> ExistingFileTestCases
		{
			get
			{
				yield return new TestCaseData(Path.Combine(ProjectDirectories.AlphaFSTestDirectoryPath, "ApprovalTests.AlphaFS.Tests.csproj")).SetName("ApprovalTests.AlphaFS.Tests.csproj");
				yield return new TestCaseData(Path.Combine(ProjectDirectories.AlphaFSTestDirectoryPath, "PathTests.cs")).SetName("PathTests.cs");
				yield return new TestCaseData(Path.Combine(ProjectDirectories.AlphaFSTestDirectoryPath, "FileTests.cs")).SetName("FileTests.cs");
			}
		}
	}
}