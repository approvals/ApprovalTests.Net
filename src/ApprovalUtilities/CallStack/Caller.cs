namespace ApprovalUtilities.CallStack;

public class Caller
{
    int currentFrame;

    public Caller()
        : this(new(true), 2)
    {
    }

    public Caller(StackTrace stackTrace, int currentFrame)
    {
        StackTrace = stackTrace;
        this.currentFrame = currentFrame;
    }

    public IEnumerable<Caller> Callers
    {
        get
        {
            for (var i = currentFrame; i < StackTrace.FrameCount; i++)
            {
                yield return new(StackTrace, i);
            }
        }
    }

    public Type Class => Method.DeclaringType;

    public MethodBase Method => StackFrame.GetMethod();

    public IEnumerable<MethodBase> Methods
    {
        get
        {
            for (var i = currentFrame; i < StackTrace.FrameCount; i++)
            {
                yield return StackTrace.GetFrame(i).GetMethod();
            }
        }
    }

    public IEnumerable<Caller> NonLambdaCallers => Callers.Where(c => c.Class != null);

    public IEnumerable<Caller> Parents
    {
        get
        {
            for (var i = currentFrame; 0 <= i; i--)
            {
                yield return new(StackTrace, i);
            }
        }
    }

    public StackFrame StackFrame => StackTrace.GetFrame(currentFrame);

    public StackTrace StackTrace { get; }

    public A GetFirstFrameForAttribute<A>() where A : Attribute
    {
        var attribute = typeof(A);
        return GetFirstFrameForAttribute(attribute) as A;
    }

    public object GetFirstFrameForAttribute(Type attribute)
    {
        var attributeExtractors = new Func<MethodBase, object[]>[]
        {
            m => m.GetCustomAttributes(attribute, true),
            m => m.DeclaringType.GetCustomAttributes(attribute, true),
            m => m.DeclaringType.Assembly.GetCustomAttributes(attribute, true)
        };
        foreach (var attributeExtractor in attributeExtractors)
        {
            foreach (var method in NonLambdaCallers.Select(c => c.Method))
            {
                try
                {
                    var realMethod = Reflection.ReflectionUtilities.GetRealMethod(method);
                    var useReporters = attributeExtractor(realMethod);
                    if (useReporters.Length != 0)
                    {
                        return useReporters.First();
                    }
                }
                catch (FileNotFoundException)
                {
                    // ignore exceptions
                }
            }
        }

        return null;
    }

    public override string ToString()
    {
        return Class.Assembly.GetName().Name + "." + Method.ToStandardString();
    }
}