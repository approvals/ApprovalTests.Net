using System.IO;
using System.Threading.Tasks;
using ApprovalUtilities.Persistence;

namespace ApprovalTests.Tests.Persistence
{
    public class FileAsyncSaver : ISaverAsync<string>
    {
        private readonly FileInfo file;

        public FileAsyncSaver(FileInfo file)
        {
            this.file = file;
        }


        public async Task<string> Save(string objectToBeSaved)
        {
            using var fileStream = file.OpenWrite();
            using var writer = new StreamWriter(fileStream);
            await writer.WriteAsync(objectToBeSaved);
            return objectToBeSaved;
        }
    }
}