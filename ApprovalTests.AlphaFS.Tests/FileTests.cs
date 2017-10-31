using System.Collections.Generic;
using System.Linq;
using Alphaleonis.Win32.Filesystem;
using NUnit.Framework;
using IOFile = System.IO.File;

namespace ApprovalTests.AlphaFS.Tests
{
	[TestFixture]
	public class FileTests
	{
		[Test]
		[TestCaseSource(nameof(FileTestCases))]
		public void Exists(string filepath)
		{
			var alphaFS = File.Exists(filepath);
			var io = IOFile.Exists(filepath);

			Assert.That(alphaFS, Is.EqualTo(io));
		}

		[Test]
		[TestCaseSource(nameof(ExistingFileTestCases))]
		public void ReadAllText(string filepath)
		{
			var alphaFS = File.ReadAllText(filepath);
			var io = IOFile.ReadAllText(filepath);

			Assert.That(alphaFS, Is.EqualTo(io));
		}

		[Test]
		[TestCaseSource(nameof(ExistingFileTestCases))]
		public void ReadAllBytes(string filepath)
		{
			var alphaFS = File.ReadAllBytes(filepath);
			var io = IOFile.ReadAllBytes(filepath);

			Assert.That(alphaFS, Is.EqualTo(io));
		}

		[Test]
		[TestCaseSource(nameof(ExistingFileTestCases))]
		public void ReadAllLines(string filepath)
		{
			var alphaFS = File.ReadAllLines(filepath);
			var io = IOFile.ReadAllLines(filepath);

			Assert.That(alphaFS, Is.EqualTo(io));
		}

		[Test]
		[TestCaseSource(nameof(ExistingFileTestCases))]
		public void WriteAllText(string filepath)
		{
			var content = File.ReadAllText(filepath);
			var fileOne = Path.GetTempFileName();
			var fileTwo = Path.GetTempFileName();
			try
			{
				IOFile.WriteAllText(fileOne, content);
				File.WriteAllText(fileTwo, content);

				var alphaFS = File.ReadAllText(fileTwo);
				var io = File.ReadAllText(fileOne);

				Assert.That(alphaFS, Is.EqualTo(io));
			}
			finally
			{
				File.Delete(fileOne);
				File.Delete(fileTwo);
			}
		}

		[Test]
		[TestCaseSource(nameof(ExistingFileTestCases))]
		public void WriteAllBytes(string filepath)
		{
			var content = File.ReadAllBytes(filepath);
			var fileOne = Path.GetTempFileName();
			var fileTwo = Path.GetTempFileName();
			try
			{
				IOFile.WriteAllBytes(fileOne, content);
				File.WriteAllBytes(fileTwo, content);

				var alphaFS = File.ReadAllBytes(fileTwo);
				var io = File.ReadAllBytes(fileOne);

				Assert.That(alphaFS, Is.EqualTo(io));
			}
			finally
			{
				File.Delete(fileOne);
				File.Delete(fileTwo);
			}
		}

		[Test]
		[TestCaseSource(nameof(ExistingFileTestCases))]
		public void AppendAllText(string filepath)
		{
			var content = File.ReadAllText(filepath);
			var fileOne = Path.GetTempFileName();
			var fileTwo = Path.GetTempFileName();
			try
			{
				for (var index = 0; index < 3; index++)
				{
					IOFile.AppendAllText(fileOne, content);
					File.AppendAllText(fileTwo, content);
				}

				var alphaFS = File.ReadAllText(fileTwo);
				var io = File.ReadAllText(fileOne);

				Assert.That(alphaFS, Is.EqualTo(io));
			}
			finally
			{
				File.Delete(fileOne);
				File.Delete(fileTwo);
			}
		}

		[Test]
		public void Delete()
		{
			var file = Path.GetTempFileName();
			File.WriteAllText(file, "content");
			File.Delete(file);

			Assert.That(File.Exists(file), Is.False);
		}

		[Test]
		[TestCaseSource(nameof(ExistingFileTestCases))]
		public void OpenRead(string filepath)
		{
			byte[] alphaFS = new byte[short.MaxValue];
			byte[] io = new byte[short.MaxValue];
			using (var stream = IOFile.OpenRead(filepath))
			{
				stream.Read(io, 0, io.Length);
			}
			using (var stream = File.OpenRead(filepath))
			{
				stream.Read(alphaFS, 0, alphaFS.Length);
			}
			Assert.That(alphaFS, Is.EqualTo(io));
		}

		[Test]
		[TestCaseSource(nameof(ExistingFileTestCases))]
		public void OpenWrite(string filepath)
		{
			var content = File.ReadAllBytes(filepath);
			var fileOne = Path.GetTempFileName();
			var fileTwo = Path.GetTempFileName();
			try
			{
				using (var stream = IOFile.OpenWrite(fileOne))
				{
					stream.Write(content, 0, content.Length);
				}
				using (var stream = File.OpenWrite(fileTwo))
				{
					stream.Write(content, 0, content.Length);
				}

				var alphaFS = File.ReadAllText(fileTwo);
				var io = File.ReadAllText(fileOne);

				Assert.That(alphaFS, Is.EqualTo(io));
			}
			finally
			{
				File.Delete(fileOne);
				File.Delete(fileTwo);
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