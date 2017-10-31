using System;
using System.Collections.Generic;
using Alphaleonis.Win32.Filesystem;
using NUnit.Framework;
using IODirectory = System.IO.Directory;

namespace ApprovalTests.AlphaFS.Tests
{
	[TestFixture]
	public class DirectoryTests
	{
		[Test]
		[TestCaseSource(nameof(DirectoryTestCases))]
		public void Exists(string directory)
		{
			var alphaFS = Directory.Exists(directory);
			var io = IODirectory.Exists(directory);

			Assert.That(alphaFS, Is.EqualTo(io));
		}

		[Test]
		public void CreateDirectory()
		{
			var dirName = new Random(31).Next().ToString();
			var temp = Path.GetTempPath() + dirName;
			var info = Directory.CreateDirectory(temp);

			Assert.That(info.Exists, Is.True);
			Assert.That(info.Name, Is.EqualTo(dirName));
		}

		private static IEnumerable<TestCaseData> DirectoryTestCases
		{
			get
			{
				yield return new TestCaseData("unexistingDirectory").SetName("unexistingDirectory");
				yield return new TestCaseData(ProjectDirectories.AlphaFSTestDirectoryPath).SetName("ApprovalTests.AlphaFS.Tests");
			}
		}
	}
}