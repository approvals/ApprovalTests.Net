using System.Reflection;

namespace ApprovalUtilities.CallStack;

public static class ReflectionUtilities
{
    public static IEnumerable<Caller> NonLambda(this IEnumerable<Caller> callers) =>
        callers.Where(c => c.Class != null);

    public static string ToStandardString(this MethodBase method) =>
        $"{method.DeclaringType.Name}.{method.Name}()";
}