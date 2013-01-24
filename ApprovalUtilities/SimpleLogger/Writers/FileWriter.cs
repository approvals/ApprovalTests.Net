using System;
using System.Diagnostics;
using System.IO;

namespace ApprovalUtilities.SimpleLogger.Writers
{
	public class FileWriter : IAppendable
	{
		public FileWriter()
		{
			LogFile = Path.GetTempPath() + "Logger.txt";
		}

		public void AppendLine(string text)
		{
			lock (this)
			{
				File.AppendAllText(LogFile, text + Environment.NewLine);
			}
		}

		private string logFilePath;
		private Func<string> getLogFile;
		public Func<string> GetLogFile
		{
			get { return getLogFile; }
			set
			{
				getLogFile = value;
				InitialWrite(value());
			}
		}


		public string LogFile
		{
			get
			{
				var logFile = GetLogFile();
				InitialWrite(logFile);
				return logFile;
			}
			set
			{
				GetLogFile = () => value;
				InitialWrite(value);
			}
		}

		private void InitialWrite(string value)
		{
			if (value.Equals(logFilePath))
			{
				return;
			}
			logFilePath = value;
			EnsureDirectoryExists(new FileInfo(value).Directory);
			var message = "Logging to:" + value;
			Console.WriteLine(message);
			Debug.WriteLine(message);
		}

		public static void EnsureDirectoryExists(DirectoryInfo directory)
		{
			directory.Refresh();
			if (directory.Exists)
			{
				return;
			}

			EnsureDirectoryExists(directory.Parent);

			directory.Create();
		}
	}
}