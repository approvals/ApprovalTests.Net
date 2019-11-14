using System.IO;
using ApprovalUtilities.Persistence;

namespace ApprovalTests.Tests.Persistence
{
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