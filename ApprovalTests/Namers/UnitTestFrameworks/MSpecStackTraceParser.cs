using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using ApprovalTests.Namers;
using ApprovalUtilities.CallStack;

namespace ApprovalTests.StackTraceParsers
{
	public class MSpecStackTraceParser : IStackTraceParser
	{
		private string methodName;
		private Caller approvalFrame;

		public bool Parse(StackTrace stackTrace)
		{
			var caller = new Caller(stackTrace, 0);
			approvalFrame = FindApprovalFrame(caller);
			if (approvalFrame == null)
			{
				return false;
			}
			methodName = FindFieldNameForDelegate(approvalFrame);
      return true;
		}

		private string FindFieldNameForDelegate(Caller approvalFrame)
		{
			var type = approvalFrame.Class;
			object x = Activator.CreateInstance(type);
			var y = type.GetMembers()
				.Where(m => m.ReflectedType.Name == "It");
			var g = type.GetFields(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly);
			var h = g.Where(fi => fi.FieldType.FullName == "Machine.Specifications.It").ToList();
			var i = h.Where(mi =>
				{
					var theDelegate = mi.GetValue(x) as Delegate;
					return theDelegate != null && theDelegate.Method == approvalFrame.Method;
				});
			FieldInfo j = i.FirstOrDefault();

			return j.Name;
		}

		public string ForTestingFramework
		{
			get { return "Machine.Specifications (MSpec)"; }
		}

		public string ApprovalName
		{
			get
			{
				var a = String.Format(@"{0}.{1}{2}", TypeName, methodName, AdditionalInfo);
				return a;
			}
		}

		public string SourcePath
		{
			get { return Path.GetDirectoryName(GetFileNameForStack(approvalFrame)); }
		}

		public MethodBase Method
		{
			get { return approvalFrame.Method; }
		}

		public string TypeName
		{
			get { return Method.DeclaringType.Name; }
		}

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

		private string GetFileNameForStack(Caller frame)
		{
			return frame.Parents.Select(c => c.StackFrame.GetFileName()).FirstOrDefault(f => f != null);
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