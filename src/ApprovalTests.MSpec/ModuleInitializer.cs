using ApprovalTests.Namers.StackTraceParsers;

public static class ModuleInitializer
{
    public static void Initialize()
    {
        AttributeStackTraceParser.FileInfoIsValidFilter = caller => true;
    }
}