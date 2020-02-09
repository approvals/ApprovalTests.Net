using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using ApprovalUtilities.Utilities;
using NUnit.Framework;

namespace ApprovalTests.Tests
{
    [TestFixture]
    public class DocumentHelpers
    {

        [Test]
        public void ListAllVerifyFunctions()
        {
            // get all classes with verify
            var methodInfos = typeof(Approvals).Assembly.GetTypes().SelectMany(t => t.GetMethods());

            var verifys = methodInfos.Where(m => m.Name.StartsWith("Verify"));
            var duplicateNames = new HashSet<string>();
            var unique = verifys.Where(v =>
            {
                var n = $"{v.DeclaringType.Name}.{v.Name}";
                var dup = !duplicateNames.Contains(n);
                duplicateNames.Add(n);
                return dup;
            });
            var linkText = unique.Select(v => $"[{v.DeclaringType.Name}.{v.Name}({ShowParameters(v.GetParameters())})]({GetLink(v)})").JoinWith("  \n  \n");

            Approvals.VerifyWithExtension(linkText, ".include.md");
        }

        private string GetLink(MethodInfo m)
        {
            var baseUrl = "https://github.com/approvals/ApprovalTests.Net/blob/master/src/";
            var classPath = m.DeclaringType.FullName.Replace(".", "/");
            return $"{baseUrl}{classPath}.cs";
        }

        private string ShowParameters(ParameterInfo[] parameters)
        {
            return parameters.Select(p => $"{p.ParameterType.ToReadableString()} {p.Name}".Replace("<", "&lt;")).JoinWith(", ");
        }
    }

    public static class _ {
        public static string ToReadableString(this Type type)
        {
            if (type.IsGenericType)
            {
                var mainType = type.Name.Substring(0, type.Name.LastIndexOf("`", StringComparison.InvariantCulture));
                var typeParameters = string.Join(", ", type.GetGenericArguments().Select(ToReadableString));
                return $"{mainType}<{typeParameters}>";
            }

            return type.Name;
        }
    }
}