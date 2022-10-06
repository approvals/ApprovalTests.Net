namespace ApprovalTests;

public class ApprovalTextWriter : IApprovalWriter
{
    public ApprovalTextWriter(string data, string extensionWithoutDot = "txt")
    {
        Data = data;
        ExtensionWithDot = EnsureDot(extensionWithoutDot);
    }

    public static string EnsureDot(string extension)
    {
        var extensionWithDot = $".{extension}";
        return extension.StartsWith(".") ? extension : extensionWithDot;
    }

    public string Data { get; set; }
    public string ExtensionWithDot { get; set; }

    public virtual string GetApprovalFilename(string basename)
    {
        return $"{basename}.approved{ExtensionWithDot}";
    }

    public virtual string GetReceivedFilename(string basename)
    {
        return $"{basename}.received{ExtensionWithDot}";
    }

    public string WriteReceivedFile(string received)
    {
        Directory.CreateDirectory(Path.GetDirectoryName(received));
        File.WriteAllText(received, Data, Encoding.UTF8);
        return received;
    }
}