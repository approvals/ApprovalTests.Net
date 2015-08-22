using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using ApprovalUtilities.Utilities;

namespace ApprovalUtilities.CallStack
{
    public static class ReflectionUtilities
    {
        public static IEnumerable<Caller> NonLambda(this IEnumerable<Caller> callers)
        {
            return callers.Where(c => c.Class != null);
        }

        public static string ToStandardString(this MethodBase method)
        {
            return "{0}.{1}()".FormatWith(method.DeclaringType.Name, method.Name);
        }
    }

    public class Caller
    {
        private int currentFrame;
        private StackTrace stackTrace;

        public Caller()
            : this(new StackTrace(true), 2)
        {
        }

        public Caller(StackTrace stackTrace, int currentFrame)
        {
            this.stackTrace = stackTrace;
            this.currentFrame = currentFrame;
        }

        public IEnumerable<Caller> Callers
        {
            get
            {
                for (var i = currentFrame; i < StackTrace.FrameCount; i++)
                {
                    yield return new Caller(StackTrace, i);
                }
            }
        }

        public Type Class
        {
            get { return Method.DeclaringType; }
        }

        public MethodBase Method
        {
            get { return StackFrame.GetMethod(); }
        }

        public IEnumerable<MethodBase> Methods
        {
            get
            {
                for (var i = currentFrame; i < StackTrace.FrameCount; i++)
                {
                    yield return StackTrace.GetFrame(i).GetMethod();
                }
            }
        }

        public IEnumerable<Caller> NonLambdaCallers
        {
            get { return Callers.Where(c => c.Class != null); }
        }

        public IEnumerable<Caller> Parents
        {
            get
            {
                for (var i = currentFrame; 0 <= i; i--)
                {
                    yield return new Caller(StackTrace, i);
                }
            }
        }

        public StackFrame StackFrame
        {
            get { return StackTrace.GetFrame(currentFrame); }
        }

        public StackTrace StackTrace
        {
            get { return stackTrace; }
        }

        public A GetFirstFrameForAttribute<A>() where A : Attribute
        {
            var attribute = typeof(A);
            return this.GetFirstFrameForAttribute(attribute) as A;
        }

        public object GetFirstFrameForAttribute(Type attribute)
        {
            var attributeExtractors = new Func<MethodBase, Object[]>[]
	                                      {
	                                          m => m.GetCustomAttributes(attribute, true),
	                                          m => m.DeclaringType.GetCustomAttributes(attribute, true),
	                                          m => m.DeclaringType.Assembly.GetCustomAttributes(attribute, true)
	                                      };
            foreach (var attributeExtractor in attributeExtractors)
            {
                foreach (MethodBase method in this.NonLambdaCallers.Select(c => c.Method))
                {
                    try
                    {
                        object[] useReporters = attributeExtractor(method);
                        if (useReporters.Length != 0)
                        {
                            return useReporters.First();
                        }
                    }
                    catch (FileNotFoundException)
                    {
                        // ignore exceptions
                    }
                }
            }
            return null;
        }

        public override string ToString()
        {
            return Class.Assembly.GetName().Name + "." + Method.ToStandardString();
        }
    }
}