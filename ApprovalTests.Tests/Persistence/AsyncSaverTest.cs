using System.IO;
using System.Threading.Tasks;
using ApprovalUtilities.Persistence;
using ApprovalUtilities.Utilities;
using NUnit.Framework;

namespace ApprovalTests.Tests.Persistence
{
	[TestFixture]
	public class AsyncSaverTest
	{
		[Test]
		public void TestAnsyncWrapperSave()
		{
			using (var f = new TempFile("stuff"))
			{
				var s = new FileSaver(f.File);
				Assert.AreEqual("hello", s.ToAsync().Save("hello").Result);
			}
		}

		[Test]
		public void TestTrueAnsyncSave()
		{
			using (var f = new TempFile("stuff"))
			{
				var s = new FileAsyncSaver(f.File);
				Assert.AreEqual("hello", s.Save("hello").Result);
			}
		}

		[Test]
		public void TestNonAnsyncWrapper()
		{
			using (var f = new TempFile("stuff"))
			{
				var s = new FileAsyncSaver(f.File);
				Assert.AreEqual("hello", s.ToSynchronous().Save("hello"));
			}
		}
	}

	public class FileAsyncSaver : ISaverAsync<string>
	{
		private readonly FileInfo file;

		public FileAsyncSaver(FileInfo file)
		{
			this.file = file;
		}


		public async Task<string> Save(string objectToBeSaved)
		{
			using (var fileStream = file.OpenWrite())
			{
				using (var writer = new StreamWriter(fileStream))
				{
					await writer.WriteAsync(objectToBeSaved);
					return objectToBeSaved;
				}
			}
		}
	}
	public class FileSaver : ISaver<string>
	{
		private readonly FileInfo file;

		public FileSaver(FileInfo file)
		{
			this.file = file;
		}

		public string Save(string objectToBeSaved)
		{
			File.WriteAllText(file.FullName, objectToBeSaved);
			return objectToBeSaved;
		}
	}

}