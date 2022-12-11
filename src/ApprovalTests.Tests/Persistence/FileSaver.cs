public class FileSaver : ISaver<string>
{
    readonly FileInfo file;

    public FileSaver(FileInfo file) =>
        this.file = file;

    public string Save(string objectToBeSaved)
    {
        File.WriteAllText(file.FullName, objectToBeSaved);
        return objectToBeSaved;
    }
}