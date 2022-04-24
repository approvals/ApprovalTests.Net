using System;
using System.IO;

namespace ApprovalUtilities.Utilities;

public class TempFile : IDisposable
{
    private readonly FileInfo backingFile;

    public TempFile(string name)
    {
        backingFile = new FileInfo(name);
        backingFile.Create().Close();
    }

    ~TempFile()
    {
        Dispose();
    }

    public FileInfo File => backingFile;

    public void Dispose()
    {
        // File on the file system is not a managed resource :)
        if (backingFile.Exists)
        {
            backingFile.Delete();
        }
    }

    public void WriteAllText(string text)
    {
        System.IO.File.WriteAllText(File.FullName, text);
    }
}