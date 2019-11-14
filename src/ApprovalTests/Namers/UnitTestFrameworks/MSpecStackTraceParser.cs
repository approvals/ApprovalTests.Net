using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using ApprovalTests.Namers.StackTraceParsers;
using ApprovalUtilities.CallStack;

namespace ApprovalTests.StackTraceParsers
{
    public class MSpecStackTraceParser : AttributeStackTraceParser
    {

        public override bool Parse(StackTrace stackTrace)
        {
            caller = new Caller(stackTrace, 0);
            approvalFrame = FindApprovalFrame(caller);
            if (approvalFrame == null)
            {
                return false;
            }

            return true;
        }

        protected override string GetAttributeType()
        {
            return "MSpec";
        }

        public override string TypeName => GetRecursiveTypeName(approvalFrame.Class.DeclaringType);

        protected override string GetMethodName()
        {
            var testClass = approvalFrame.Class.DeclaringType;
            var instance = Activator.CreateInstance(testClass);
            var fields = testClass.GetFields(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly);
            var delegates = fields.Where(f => typeof(Delegate).IsAssignableFrom(f.FieldType));
            var approvalField = delegates.FirstOrDefault(f => f.GetValue(instance) is Delegate theDelegate && theDelegate.Method == approvalFrame.Method);
            if (approvalField == null)
            {
                throw new Exception("Could not find the Field for this MSpec Test \n (Please log this if found at: https://github.com/approvals/ApprovalTests.Net/issues");
            }

            return approvalField.Name;
        }

        public override string ForTestingFramework => "Machine.Specifications (MSpec)";


        private Caller FindApprovalFrame(Caller caller)
        {
            var mspecInvocationFrame = caller.Callers.NonLambda().FirstOrDefault(
                c => c.Class.FullName == "Machine.Specifications.Model.Specification"
                     && c.Method.Name == "InvokeSpecificationField");

            return mspecInvocationFrame?.Parents.NonLambda().Skip(1)
                .FirstOrDefault(c => !c.Class.FullName.StartsWith("System."));
        }
    }
}