namespace ApprovalTests.Writers;

public static class WriterFactory
{
    static Func<string, IApprovalWriter> TextWriterCreator = s => new ApprovalTextWriter(s);

    public static void SetTextWriterCreator(Func<string, IApprovalWriter> textWriterCreator)
    {
        TextWriterCreator = textWriterCreator;
    }

    public static IApprovalWriter CreateTextWriter(string data)
    {
        return TextWriterCreator(data);
    }

    static Func<string, string, IApprovalWriter> TextWriterWithExtensionCreator = (s, e) => new ApprovalTextWriter(s, e);

    public static void SetTextWriterCreator(Func<string, string, IApprovalWriter> textWriterWithExtensionCreator)
    {
        TextWriterWithExtensionCreator = textWriterWithExtensionCreator;
    }

    public static IApprovalWriter CreateTextWriter(string data, string extensionWithoutDot)
    {
        return TextWriterWithExtensionCreator(data, extensionWithoutDot);
    }
}