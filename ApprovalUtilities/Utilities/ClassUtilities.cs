using System;
using System.Linq;

namespace ApprovalUtilities.Utilities
{
    public class ClassUtilities
    {
        public static bool IsClassAvailable(string typeName)
        {
            return ExceptionUtilities.GetException(() => Type.GetType(typeName)) == null;
        }

        public static bool IsAssemblyLoaded(String assembly)
        {
            AppDomain.CurrentDomain.GetAssemblies().ToList().ForEach(a => Console.WriteLine(a.GetName()));
            return AppDomain.CurrentDomain.GetAssemblies().Any(a => a.FullName == assembly);
        }
    }
}