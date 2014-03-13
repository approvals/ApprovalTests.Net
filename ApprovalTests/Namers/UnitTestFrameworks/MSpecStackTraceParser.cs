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

		protected override string GetMethodName()
		{
			var instance = Activator.CreateInstance(approvalFrame.Class);
			var fields = approvalFrame.Class.GetFields(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly);
			var delegates = fields.Where(f => typeof(Delegate).IsAssignableFrom(f.FieldType));
			var approvalField = delegates.FirstOrDefault(f =>
				{
					var theDelegate = f.GetValue(instance) as Delegate;
					return theDelegate != null && theDelegate.Method == approvalFrame.Method;
				});
			if (approvalField == null)
			{
				throw new Exception("Could not find the Field for this MSpec Test \r\n (Please log this if found at: https://github.com/approvals/ApprovalTests.Net/issues");
			}
			return approvalField.Name;
		}

		public override string ForTestingFramework
		{
			get { return "Machine.Specifications (MSpec)"; }
		}

	


		private Caller FindApprovalFrame(Caller caller)
		{
			var mspecInvocationFrame = caller.Callers.NonLambda().FirstOrDefault(
				c => c.Class.FullName == "Machine.Specifications.Model.Specification"
				     && c.Method.Name == "InvokeSpecificationField");

			if (mspecInvocationFrame == null)
			{
				return null;
			}

			return mspecInvocationFrame.Parents.NonLambda().Skip(1)
				.FirstOrDefault(c => !c.Class.FullName.StartsWith("System."));
		}
	}
}