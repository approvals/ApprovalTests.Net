using System;

namespace ApprovalUtilities.Utilities
{
    [Obsolete(ObsoleteError, true)]
    public class ClipboardUtilities
    {
        internal const string ObsoleteError = "Moved to the TextCopy NuGet package (https://www.nuget.org/packages/TextCopy)";
    }
}