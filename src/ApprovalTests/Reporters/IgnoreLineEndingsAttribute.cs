namespace ApprovalTests.Reporters;

public class IgnoreLineEndingsAttribute(bool ignoreLineEndings) :
    Attribute
{
    public bool IgnoreLineEndings { get; } = ignoreLineEndings;
}