using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ApprovalUtilities.CallStack;

public static class ReflectionUtilities
{
    public static IEnumerable<Caller> NonLambda(this IEnumerable<Caller> callers)
    {
        return callers.Where(c => c.Class != null);
    }

    public static string ToStandardString(this MethodBase method)
    {
        return $"{method.DeclaringType.Name}.{method.Name}()";
    }
}