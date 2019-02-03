using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using ApprovalUtilities.CallStack;

namespace ApprovalTests.Namers.StackTraceParsers
{
    public abstract class AttributeStackTraceParser : IStackTraceParser
    {
        protected Caller caller;
        protected Caller approvalFrame;

        public virtual string TypeName => GetRecursiveTypeName(GetRealMethod(approvalFrame.Method).DeclaringType);

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

        public string ApprovalName => $"{TypeName}.{GetMethodName()}{AdditionalInfo}";

        protected virtual string GetMethodName()
        {
            return GetRealMethod(approvalFrame.Method).Name;
        }

        static MethodBase GetRealMethod(MethodBase method)
        {
            var declaringType = method.DeclaringType;
            if (typeof(IAsyncStateMachine).IsAssignableFrom(declaringType))
            {
                var realType = declaringType.DeclaringType;
                foreach (var methodInfo in realType.GetMethods(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic))
                {
                    var stateMachineAttribute = methodInfo.GetCustomAttribute<AsyncStateMachineAttribute>();
                    if (stateMachineAttribute == null)
                    {
                        continue;
                    }
                    if (stateMachineAttribute.StateMachineType == declaringType)
                    {
                        return methodInfo;
                    }
                }
            }

            return method;
        }

        public string SourcePath => Path.GetDirectoryName(GetFileNameForStack(approvalFrame));

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
            return caller.Callers.FirstOrDefault(c =>
            {
                var attributes = GetRealMethod(c.Method).GetCustomAttributes(false);
                return ContainsAttribute(attributes, attributeName);
            });
        }

        private static bool ContainsAttribute(object[] attributes, string attributeName)
        {
            return attributes.Any(attribute =>
            {
                var type = attribute.GetType();
                do
                {
                    if (type.FullName.StartsWith(attributeName))
                    {
                        return true;
                    }

                    type = type.BaseType;
                } while (type != null);

                return false;
            });
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

        protected static string GetRecursiveTypeName(Type type)
        {
            if (type.DeclaringType != null)
            {
                return $"{GetRecursiveTypeName(type.DeclaringType)}.{type.Name}";
            }

            return type.Name;
        }
    }
}