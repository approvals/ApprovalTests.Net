using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using ApprovalTests.Reporters;
using ApprovalUtilities.CallStack;
using ApprovalUtilities.Utilities;

namespace ApprovalTests.StackTraceParsers
{
    public class XUnitStackTraceParser : AttributeStackTraceParser
    {
        public const string Attribute = "Xunit.FactAttribute";
        public static string AsyncAttributeName = "System.Runtime.CompilerServices.AsyncStateMachineAttribute";

        public override string ForTestingFramework
        {
            get { return "xUnit.net"; }
        }

        protected override Caller FindApprovalFrame()
        {
            return VerifyAsyncUsedProperly(base.FindApprovalFrame());
        }

        protected override string GetAttributeType()
        {
            return Attribute;
        }

        private static void FailLoudly(string msg)
        {
            Console.WriteLine(msg);
            Debug.WriteLine(msg);
            var tempFile = Path.ChangeExtension(Path.GetTempFileName(), "txt");
            File.WriteAllText(tempFile, msg);
            new FileLauncherReporter().Report(null, tempFile);
            throw new InvalidOperationException(msg);
        }

        private static Caller VerifyAsyncUsedProperly(Caller res)
        {
            if (res == null)
            {
                return null;
            }

            var attribute = Type.GetType(AsyncAttributeName, false);
            if (attribute == null)
            {
                return res;
            }

            var asyncFrame = res.GetFirstFrameForAttribute(attribute);
            MethodBase method = res.Method;
            var mi = method as MethodInfo;
            if (asyncFrame != null && mi != null && mi.ReturnType == typeof(void))
            {
                var msg =
                    @"WARNING: You are using xUnit async incorrectly.
The method {0} has a return type of void instead of Task.
In this form this test can never fail, it will always pass.
".FormatWith(method.ToStandardString());
                msg = StringUtils.FormatFrame('*', msg);
                FailLoudly(msg);
            }
            return res;
        }
    }
}