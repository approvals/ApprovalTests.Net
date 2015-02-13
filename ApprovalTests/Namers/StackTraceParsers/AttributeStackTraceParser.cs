using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using ApprovalUtilities.CallStack;

namespace ApprovalTests.Namers.StackTraceParsers
{
	public abstract class AttributeStackTraceParser : IStackTraceParser
	{
		protected Caller caller;
		protected Caller approvalFrame;


		public string TypeName
		{
			get { return GetRealMethod(approvalFrame.Method).DeclaringType.Name; }
		}

	    static MethodBase GetRealMethod(MethodBase method)
	    {
	        var declaringType = method.DeclaringType;
	        if (declaringType.IsByRef)
	        {
	            return method;
	        }
            if (!ContainsAttribute(declaringType.GetCustomAttributes(false), "System.Runtime.CompilerServices.CompilerGeneratedAttribute"))
	        {
	            return method;
	        }
	        if (declaringType.GetInterface("System.Runtime.CompilerServices.IAsyncStateMachine") == null)
	        {
	            return method;
	        }
            if (!declaringType.Name.Contains("<") || !declaringType.Name.Contains(">"))
	        {
	            return method;
	        }
            var trueMethodName = declaringType.Name.TrimStart('<').Split('>').First();
	        MethodInfo methodInfo;
	            var bindingFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.DeclaredOnly;
	        try
	        {
	            methodInfo = declaringType.DeclaringType.GetMethod(trueMethodName, bindingFlags);
	        }
	        catch (AmbiguousMatchException)
	        {
                //TODO: Should this throw??
	            //var message = string.Format("Could not derive root method for async method '{0}' since there are multiple methods named '{1}'.", method.Name, trueMethodName);
                return method;
	        }
            return methodInfo;
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
            return GetRealMethod(approvalFrame.Method).Name;
		}

		public string SourcePath
		{
			get { return Path.GetDirectoryName(GetFileNameForStack(approvalFrame)); }
		}

		private string GetFileNameForStack(Caller frame)
		{
			return frame.Parents.Select(c => c.StackFrame.GetFileName()).FirstOrDefault(f => f != null);
		}

		public abstract string ForTestingFramework { get; }

		public virtual bool Parse(StackTrace trace)
		{
			caller = new Caller(trace, 0);
			approvalFrame = FindApprovalFrame();
			return approvalFrame != null;
		}

		public static Caller GetFirstFrameForAttribute(Caller caller, string attributeName)
		{
			return caller.Callers.FirstOrDefault(c => ContainsAttribute(GetRealMethod(c.Method).GetCustomAttributes(false), attributeName));
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