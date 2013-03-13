using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using ApprovalTests.Namers;
using ApprovalUtilities.CallStack;

namespace ApprovalTests.StackTraceParsers
{
	public class MSpecStackTraceParser    : IStackTraceParser
    {
        private Caller _caller;
        private string approverMethodName;
 
        public bool Parse(StackTrace stackTrace)
        {
            _caller = new Caller(stackTrace, 0);
            var approvalFrame = FindApprovalFrame();
            if (approvalFrame == null) return false;
 
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
 
            approverMethodName = j.Name;
 
            return true;
        }
 
        public string ForTestingFramework { get { return "Machine.Specifications (MSpec)"; } }
 
        public string ApprovalName
        {
            get { var a = String.Format(@"{0}.{1}{2}", TypeName, approverMethodName, AdditionalInfo);
                return a;
            }
        }
 
        public string SourcePath
        {
            get { var x = Path.GetDirectoryName(GetFileNameForStack(FindApprovalFrame()));
                return x;
            }
        }
 
        public MethodBase Method
        {
            get { return FindApprovalFrame().Method; }
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
 
        private Caller FindApprovalFrame()
        {
            var mspecCallers = _caller.Callers.Where(c => c.Class.FullName.StartsWith("Machine.Specifications"));
 
            var mspecInvocationFrame = mspecCallers.FirstOrDefault(
                c => c.Class.FullName == "Machine.Specifications.Model.Specification"
                     && c.Method.Name == "InvokeSpecificationField");
 
            var frameContainingTheApproval = mspecInvocationFrame.Parents.Skip(1) // the mspecInvocationFrame
                                                                 .FirstOrDefault(c => c.Class.FullName.StartsWith("System") == false);
 
            return frameContainingTheApproval;
        }
    }
}