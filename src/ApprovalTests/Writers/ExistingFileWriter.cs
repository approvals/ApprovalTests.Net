using ApprovalTests.Core;

namespace ApprovalTests.Writers;

public class ExistingFileWriter : IApprovalWriter
{
    string file;

    public ExistingFileWriter(string file)
    {
        this.file = file;
        if (!File.Exists(file))
        {
            throw new("Existing File is required: '" + file + "'");
        }
    }

    public string GetApprovalFilename(string basename) =>
        basename + WriterUtils.Approved + new FileInfo(file).Extension;

    public string GetReceivedFilename(string basename) => file;

    public string WriteReceivedFile(string received) => file;
}