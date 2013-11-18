using System;
using System.IO;

namespace ApprovalUtilities.Utilities
{
	public class TempFile : IDisposable
	{
		private readonly FileInfo backingFile;

		public TempFile(string name)
		{
			this.backingFile = new FileInfo(name);
			this.backingFile.Create().Close();
		}

		~TempFile()
		{
			this.Dispose();
		}

		public FileInfo File
		{
			get { return this.backingFile; }
		}

		public void Dispose()
		{
			// File on the file system is not a managed resource :)
			if (this.backingFile.Exists)
			{
				this.backingFile.Delete();
			}
		}

		public void WriteAllText(string text)
		{
			System.IO.File.WriteAllText(File.FullName, text);
		}
	}
}
