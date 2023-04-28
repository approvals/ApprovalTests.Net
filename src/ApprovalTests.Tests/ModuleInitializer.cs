using System.Runtime.CompilerServices;
using ApprovalTests.Namers.StackTraceParsers;

public static class ModuleInitializer
{
    [ModuleInitializer]
    public static void Initialize() =>
        AttributeStackTraceParser.FileInfoIsValidFilter = _ => true;
}