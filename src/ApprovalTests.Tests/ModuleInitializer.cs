using System.Runtime.CompilerServices;

public static class ModuleInitializer
{
    [ModuleInitializer]
    public static void Initialize() =>
        AttributeStackTraceParser.FileInfoIsValidFilter = _ => true;
}