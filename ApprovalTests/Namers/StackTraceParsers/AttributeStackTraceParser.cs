using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using ApprovalUtilities.CallStack;
using ApprovalUtilities.SimpleLogger;

namespace ApprovalTests.Namers.StackTraceParsers
{
	public abstract class AttributeStackTraceParser : IStackTraceParser
	{
		protected Caller caller;
		protected Caller approvalFrame;


		public string TypeName
		{
			get { return approvalFrame.Method.DeclaringType.Name; }
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

		public string ApprovalName
		{
			get { return String.Format(@"{0}.{1}{2}", TypeName, GetMethodName(), AdditionalInfo); }
		}

		protected virtual string GetMethodName()
		{
			return approvalFrame.Method.Name;
		}

		public string SourcePath
		{
			get { return Path.GetDirectoryName(GetFileNameForStack(approvalFrame)); }
		}

		private string GetFileNameForStack(Caller frame)
		{
		
			Logger.Variable("frame",  frame);
			return 
				frame.Parents.Select(c => c.StackFrame.GetFileName()).FirstOrDefault(f => f != null);
		}

		public abstract string ForTestingFramework { get; }

		public virtual bool Parse(StackTrace trace)
		{
			caller = new Caller(trace, 0);
			approvalFrame = FindApprovalFrame();
			Logger.Variable("approvalFrame",approvalFrame);
			return approvalFrame != null;
		}

		public static Caller GetFirstFrameForAttribute(Caller caller, string attributeName)
		{
			var firstFrameForAttribute =
				caller.Callers.FirstOrDefault(c => ContainsAttribute(c.Method.GetCustomAttributes(false), attributeName));
			return firstFrameForAttribute;
		}

		private static bool ContainsAttribute(object[] attributes, string attributeName)
		{
			return attributes.Any(a => a.GetType().FullName.StartsWith(attributeName));
		}

		protected virtual Caller FindApprovalFrame()
		{
			return GetFirstFrameForAttribute(caller, GetAttributeType());
		}

		public bool IsApplicable()
		{
			return GetAttributeType() != null;
		}

		protected abstract string GetAttributeType();
	}
}