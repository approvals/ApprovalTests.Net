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
        public void LinkTest()
        {
           // Assert.AreEqual("", GetLink(typeof(EmailApprovals).GetMethod("Verify")));
        }
        [Test]
        public void ListAllVerifyFunctions()
        {
            var allClasses  = typeof(Approvals).Assembly.GetTypes();
            var classes = allClasses.Where(c => !c.GetCustomAttributes(false).Any(a => a.GetType().Name.Contains("Obsolete")));
            var methodInfos= classes.SelectMany(t => t.GetMethods());

            var verifys = methodInfos.Where(m => m.Name.StartsWith("Verify"));
            var duplicateNames = new HashSet<string>();
            var unique = verifys.Where(v =>
            {
                var n = $"{v.DeclaringType.Name}.{v.Name}";
                var dup = !duplicateNames.Contains(n);
                duplicateNames.Add(n);
                return dup;
            });
            var linkText = unique.Select(v => $"{v.DeclaringType.Name}.[{v.Name}]({GetLink(v)})({ShowParameters(v.GetParameters())})").OrderBy(n => n).JoinWith("  \n  \n");

            Approvals.VerifyWithExtension(linkText, ".include.md");
        }

        private string GetLink(MethodInfo m)
        {
            var baseUrl = "https://github.com/approvals/ApprovalTests.Net/blob/master/src/";
            var classPath = m.DeclaringType.FullName.Replace(".", "/");
            var filePath = PathUtilities.GetAdjacentFile($"../../{classPath}.cs");
            var code = File.ReadAllLines(filePath);
            var lineNumber = 0;
            for (var i = 0; i < code.Length; i++)
            {
                if (code[i].Contains("void "+ m.Name))
                {
                    lineNumber = i+1;
                    break;
                }
            }
            return $"{baseUrl}{classPath}.cs#L{lineNumber}";
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