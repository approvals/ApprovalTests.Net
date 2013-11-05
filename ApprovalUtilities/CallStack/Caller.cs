using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using ApprovalUtilities.Utilities;

namespace ApprovalUtilities.CallStack
{
	public class Caller
	{
		private StackTrace stackTrace;
		private int currentFrame;


		public Caller() : this(new StackTrace(true), 2)
		{
		}

		public Caller(StackTrace stackTrace, int currentFrame)
		{
			this.stackTrace = stackTrace;
			this.currentFrame = currentFrame;
		}


		public MethodBase Method
		{
			get { return StackFrame.GetMethod(); }
		}

		public StackFrame StackFrame
		{
			get { return StackTrace.GetFrame(currentFrame); }
		}

		public Type Class
		{
			get { return Method.DeclaringType; }
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

		public StackTrace StackTrace
		{
			get { return stackTrace; }
		}

		public override string ToString()
		{
			return Class.Assembly.GetName().Name + "." + Method.ToStandardString();
		}

		public A GetFirstFrameForAttribute<A>() where A : Attribute
		{
			var attribute = typeof (A);
			var attributeExtractors = new Func<MethodBase, Object[]>[]
				{
					m => m.GetCustomAttributes(attribute, true),
					m => m.DeclaringType.GetCustomAttributes(attribute, true),
					m => m.DeclaringType.Assembly.GetCustomAttributes(attribute, true)
				};
			foreach (var attributeExtractor in attributeExtractors)
			{
				foreach (MethodBase method in NonLambdaCallers.Select(c => c.Method))
				{
					try
					{
						object[] useReporters = attributeExtractor(method);
						if (useReporters.Length != 0)
						{
							return useReporters.First() as A;
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
	}

	public static class ReflectionUtilities
	{
		public static string ToStandardString(this MethodBase method)
		{
			return "{0}.{1}()".FormatWith(method.DeclaringType.Name, method.Name);
		}

		public static IEnumerable<Caller> NonLambda(this IEnumerable<Caller> callers)
		{
			return callers.Where(c => c.Class != null);
		}
	}
}