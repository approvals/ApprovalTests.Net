using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using ApprovalTests.Approvers;
using ApprovalTests.Core;
using ApprovalTests.Html;
using ApprovalTests.Namers;
using ApprovalTests.Reporters;
using ApprovalTests.Writers;
using ApprovalTests.Xml;
using ApprovalUtilities.CallStack;
using ApprovalUtilities.Persistence;
using ApprovalUtilities.Utilities;
using BinaryWriter = ApprovalTests.Writers.BinaryWriter;

namespace ApprovalTests
{
	public class Approvals
	{
		private static readonly ThreadLocal<Caller> currentCaller = new ThreadLocal<Caller>();
		private static Func<IApprovalNamer> defaultNamerCreator = () => new UnitTestFrameworkNamer();

		public static Caller CurrentCaller
		{
			get { return currentCaller.Value; }
		}

		public static void SetCaller()
		{
			currentCaller.Value = new Caller();
		}

		public static void Verify(IApprovalWriter writer, IApprovalNamer namer, IApprovalFailureReporter reporter)
		{
			Core.Approvals.Verify(new FileApprover(writer, namer), reporter);
		}

		public static IApprovalFailureReporter GetReporter()
		{
			SetCaller();
			return GetReporter(IntroductionReporter.INSTANCE);
		}

		public static IApprovalFailureReporter GetReporter(IApprovalFailureReporter defaultIfNotFound)
		{
			return GetFrontLoadedReporter(defaultIfNotFound, GetFrontLoadedReporterFromAttribute());
		}

		private static IEnvironmentAwareReporter GetFrontLoadedReporterFromAttribute()
		{
			var frontLoaded = GetFirstFrameForAttribute<FrontLoadedReporterAttribute>(CurrentCaller);
			return frontLoaded != null ? frontLoaded.Reporter : (IEnvironmentAwareReporter) DefaultFrontLoaderReporter.INSTANCE;
		}

		private static IApprovalFailureReporter GetFrontLoadedReporter(IApprovalFailureReporter defaultIfNotFound,
		                                                               IEnvironmentAwareReporter frontLoad)
		{
			return frontLoad.IsWorkingInThisEnvironment("default.txt")
				       ? frontLoad
				       : (GetReporterFromAttribute() ?? defaultIfNotFound);
		}


		private static IEnvironmentAwareReporter WrapAsEnvironmentAwareReporter(IApprovalFailureReporter mainReporter)
		{
			if (mainReporter is IEnvironmentAwareReporter)
			{
				return mainReporter as IEnvironmentAwareReporter;
			}
			return new AlwaysWorksReporter(mainReporter);
		}

		private static IApprovalFailureReporter GetReporterFromAttribute()
		{
			var useReporter = GetFirstFrameForAttribute<UseReporterAttribute>(CurrentCaller);
			return useReporter != null ? useReporter.Reporter : null;
		}

		private static A GetFirstFrameForAttribute<A>(Caller caller) where A : Attribute
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
				foreach (MethodBase method in caller.NonLambdaCallers.Select(c => c.Method))
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

		public static void Verify(IExecutableQuery query)
		{
			Verify(new ApprovalTextWriter(query.GetQuery()), GetDefaultNamer(),
			       new ExecutableQueryFailure(query, GetReporter()));
		}

		public static void Verify(IApprovalWriter writer)
		{
			Verify(writer, GetDefaultNamer(), GetReporter());
		}

		public static void VerifyFile(string file)
		{
			Verify(new ExistingFileWriter(file));
		}

		public static void Verify(FileInfo file)
		{
			VerifyFile(file.FullName);
		}

		public static void VerifyWithCallback(object text, Action<string> callBackOnFailure)
		{
			Verify(new ExecutableLambda("" + text, callBackOnFailure));
		}

		public static void VerifyWithCallback(object text, Func<string, string> callBackOnFailure)
		{
			Verify(new ExecutableLambda("" + text, callBackOnFailure));
		}

		#region Text

		public static void Verify(string text)
		{
			Verify(new ApprovalTextWriter(text));
		}

		public static void RegisterDefaultNamerCreation(Func<IApprovalNamer> creator)
		{
			defaultNamerCreator = creator;
		}

		public static IApprovalNamer GetDefaultNamer()
		{
			return defaultNamerCreator.Invoke();
		}

		public static void Verify(object text)
		{
			Verify(new ApprovalTextWriter("" + text));
		}
		public static void Verify(string text,  Func<string, string> scrubber)
		{
			Verify(new ApprovalTextWriter(scrubber(text)));
		}

		public static void Verify(Exception e)
		{
			string stackTrace = "" + e;

			Verify(stackTrace, StackTraceScrubber.Scrub);
		}

		#endregion

		#region Enumerable

		public static void VerifyAll<T>(String header, IEnumerable<T> enumerable, string label)
		{
			Verify(header + "\r\n\r\n" + enumerable.Write(label));
		}

		public static void VerifyAll<T>(IEnumerable<T> enumerable, string label)
		{
			Verify(enumerable.Write(label));
		}

		public static void VerifyAll<T>(IEnumerable<T> enumerable, string label,
		                                Func<T, string> formatter)
		{
			Verify(enumerable.Write(label, formatter));
		}

		public static void VerifyAll<T>(String header, IEnumerable<T> enumerable,
		                                Func<T, string> formatter)
		{
			Verify(header + "\r\n\r\n" + enumerable.Write(formatter));
		}

		public static void VerifyAll<T>(IEnumerable<T> enumerable, Func<T, string> formatter)
		{
			Verify(enumerable.Write(formatter));
		}

		public static void VerifyAll<K, V>(IDictionary<K, V> dictionary)
		{
			VerifyAll(dictionary.OrderBy(p => p.Key), p => "{0} => {1}".FormatWith(p.Key, p.Value));
		}

		public static void VerifyAll<K, V>(string header, IDictionary<K, V> dictionary)
		{
			VerifyAll(header, dictionary.OrderBy(p => p.Key), p => "{0} => {1}".FormatWith(p.Key, p.Value));
		}

		public static void VerifyAll<K, V>(string header, IDictionary<K, V> dictionary, Func<K, V, string> formatter)
		{
			VerifyAll(header, dictionary.OrderBy(p => p.Key), p => formatter(p.Key, p.Value));
		}

		public static void VerifyAll<K, V>(IDictionary<K, V> dictionary, Func<K, V, string> formatter)
		{
			VerifyAll(dictionary.OrderBy(p => p.Key), p => formatter(p.Key, p.Value));
		}

		public static void VerifyBinaryFile(byte[] bytes, string fileExtensionWithoutDot)
		{
			Verify(new BinaryWriter(bytes, fileExtensionWithoutDot));
		}

		public static void VerifyHtml(string html)
		{
			HtmlApprovals.VerifyHtml(html);
		}

		public static void VerifyXml(string xml)
		{
			XmlApprovals.VerifyXml(xml);
		}

		#endregion

		public static void VerifyPdfFile(string pdfFilePath)
		{
			PdfScrubber.ScrubPdf(pdfFilePath);
			Verify(new ExistingFileWriter(pdfFilePath));
		}
	}


	internal class AlwaysWorksReporter : IEnvironmentAwareReporter
	{
		private readonly IApprovalFailureReporter reporter;

		public AlwaysWorksReporter(IApprovalFailureReporter reporter)
		{
			this.reporter = reporter;
		}

		#region IEnvironmentAwareReporter Members

		public void Report(string approved, string received)
		{
			reporter.Report(approved, received);
		}

		public bool IsWorkingInThisEnvironment(string forFile)
		{
			return true;
		}

		#endregion
	}
}