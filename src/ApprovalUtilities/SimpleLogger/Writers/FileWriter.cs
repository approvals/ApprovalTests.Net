using System;
using System.Diagnostics;
using System.IO;

namespace ApprovalUtilities.SimpleLogger.Writers;

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
            InitialWrite();
            File.AppendAllText(LogFile, text + Environment.NewLine);
        }
    }

    string logFilePath;
    Func<string> getLogFile;

    public Func<string> GetLogFile
    {
        get => getLogFile;
        set
        {
            getLogFile = value;
            logFilePath = null;
        }
    }


    public string LogFile
    {
        get => GetLogFile();
        set => GetLogFile = () => value;
    }

    void InitialWrite()
    {
        if (logFilePath != null)
        {
            return;
        }

        logFilePath = getLogFile();
        EnsureDirectoryExists(new FileInfo(logFilePath).Directory);
        var message = "Logging to:" + logFilePath;
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