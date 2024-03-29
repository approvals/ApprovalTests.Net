﻿namespace ApprovalTests.Writers;

public class ConfigurableTempTextFileWriter : ApprovalTextWriter
{
    string receivedFilePath;
    string approvedFilePath;

    public ConfigurableTempTextFileWriter(string approvedFilePath, string data)
        : base(data, Path.GetExtension(approvedFilePath)) =>
        this.approvedFilePath = Path.GetFullPath(approvedFilePath);

    public override string GetApprovalFilename(string basename) =>
        approvedFilePath;

    public override string GetReceivedFilename(string basename)
    {
        if (string.IsNullOrEmpty(receivedFilePath))
        {
            receivedFilePath = Path.ChangeExtension(Path.Combine(Path.GetTempPath(), Path.GetTempFileName()), ExtensionWithDot);
        }

        return receivedFilePath;
    }
}