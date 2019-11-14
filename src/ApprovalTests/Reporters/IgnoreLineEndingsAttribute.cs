using System;

namespace ApprovalTests.Reporters
{
    public class IgnoreLineEndingsAttribute : Attribute
    {
        public IgnoreLineEndingsAttribute(bool ignoreLineEndings)
        {
            IgnoreLineEndings = ignoreLineEndings;
        }

        public bool IgnoreLineEndings { get; }
    }
}