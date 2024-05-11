using ApprovalUtilities.CallStack;

namespace ApprovalTests.Namers.StackTraceParsers;

public abstract class AttributeStackTraceParser : IStackTraceParser
{
    protected Caller caller;
    protected Caller approvalFrame;

    public virtual string TypeName => GetRecursiveTypeName(ApprovalUtilities.Reflection.ReflectionUtilities.GetRealMethod(approvalFrame.Method).DeclaringType);

    public string AdditionalInfo
    {
        get
        {
            var additionalInformation = NamerFactory.AdditionalInformation;
            if (additionalInformation != null)
            {
                NamerFactory.AdditionalInformation = null;
                additionalInformation = "." + additionalInformation;
            }

            return additionalInformation;
        }
    }

    public string ApprovalName => $"{TypeName}.{GetMethodName()}{AdditionalInfo}";

    protected virtual string GetMethodName() =>
        ApprovalUtilities.Reflection.ReflectionUtilities.GetRealMethod(approvalFrame.Method).Name;

    public string SourcePath => Path.GetDirectoryName(GetFileNameForStack(approvalFrame));

    public static Func<Caller, bool> FileInfoIsValidFilter = x =>
    {
        var classNamespace = x.Class.Namespace;
        return !IsNamespaceApprovals(classNamespace);
    };

    public static bool IsNamespaceApprovals(string classNamespace) =>
        classNamespace != null &&
        ( classNamespace.StartsWith("ApprovalTests") ||
          classNamespace.StartsWith("ApprovalUtilities"));

    static string GetFileNameForStack(Caller frame) =>
        frame.Parents
            .Where(FileInfoIsValidFilter)
            .Select(c => c.StackFrame.GetFileName())
            .FirstOrDefault(f => f != null);

    public abstract string ForTestingFramework { get; }

    public virtual bool Parse(StackTrace trace)
    {
        caller = new(trace, 0);
        approvalFrame = FindApprovalFrame();
        return approvalFrame != null;
    }

    public static Caller GetFirstFrameForAttribute(Caller caller, string attributeName) =>
        caller.Callers.FirstOrDefault(c =>
        {
            var attributes = ApprovalUtilities.Reflection.ReflectionUtilities.GetRealMethod(c.Method).GetCustomAttributes(false);
            return ContainsAttribute(attributes, attributeName);
        });

    static bool ContainsAttribute(object[] attributes, string attributeName) =>
        attributes.Any(attribute =>
        {
            var type = attribute.GetType();
            do
            {
                if (type.FullName.StartsWith(attributeName))
                {
                    return true;
                }

                type = type.BaseType;
            } while (type != null);

            return false;
        });

    protected virtual Caller FindApprovalFrame() =>
        GetFirstFrameForAttribute(caller, GetAttributeType());

    public bool IsApplicable() =>
        GetAttributeType() != null;

    protected abstract string GetAttributeType();

    protected static string GetRecursiveTypeName(Type type)
    {
        if (type.DeclaringType != null)
        {
            return $"{GetRecursiveTypeName(type.DeclaringType)}.{type.Name}";
        }

        return type.Name;
    }
}