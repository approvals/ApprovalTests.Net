using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using ApprovalTests.Approvers;
using ApprovalTests.Core;
using ApprovalTests.Html;
using ApprovalTests.Namers;
using ApprovalTests.Reporters;
using ApprovalTests.Scrubber;
using ApprovalTests.Utilities;
using ApprovalTests.Writers;
using ApprovalTests.Xml;
using ApprovalUtilities.CallStack;
using ApprovalUtilities.Persistence;
using ApprovalUtilities.Utilities;

namespace ApprovalTests
{
    public class Approvals
    {
        private static readonly ThreadLocal<Caller> currentCaller = new ThreadLocal<Caller>();
        private static Func<IApprovalNamer> defaultNamerCreator = () => new UnitTestFrameworkNamer();
        private static Func<IApprovalWriter, IApprovalNamer, bool, IApprovalApprover> defaultApproverCreator = (IApprovalWriter writer, IApprovalNamer namer, bool shouldIgnoreLineEndings) => new FileApprover(writer, namer, shouldIgnoreLineEndings);

        public static Caller CurrentCaller
        {
            get
            {
                if (currentCaller == null || currentCaller.Value==null)
                {
                    SetCaller();
                }
                return currentCaller.Value;
            }
        }

        public static void SetCaller()
        {
            currentCaller.Value = new Caller();
        }

        public static void Verify(IApprovalWriter writer, IApprovalNamer namer, IApprovalFailureReporter reporter)
        {
            var normalizeLineEndingsForTextFiles = CurrentCaller.GetFirstFrameForAttribute<IgnoreLineEndingsAttribute>();
            var shouldIgnoreLineEndings = normalizeLineEndingsForTextFiles == null || normalizeLineEndingsForTextFiles.IgnoreLineEndings;
            var approver = GetDefaultApprover(writer, namer, shouldIgnoreLineEndings);
            Verify(approver, reporter);
        }
        public static void RegisterDefaultAppover(Func<IApprovalWriter, IApprovalNamer, bool, IApprovalApprover> creator)
        {
            defaultApproverCreator =  creator;
        }

        private static IApprovalApprover GetDefaultApprover(IApprovalWriter writer, IApprovalNamer namer, bool shouldIgnoreLineEndings)
        {
            return defaultApproverCreator(writer, namer, shouldIgnoreLineEndings);
        }

        public static void Verify(IApprovalApprover approver)
        {
            Approver.Verify(approver, GetReporter());
        }
        public static void Verify(IApprovalApprover approver, IApprovalFailureReporter reporter)
        {
            Approver.Verify(approver, reporter);
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
            var frontLoaded = CurrentCaller.GetFirstFrameForAttribute<FrontLoadedReporterAttribute>();
            return frontLoaded != null ? frontLoaded.Reporter : DefaultFrontLoaderReporter.INSTANCE;
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
            var useReporter = CurrentCaller.GetFirstFrameForAttribute<UseReporterAttribute>();
            return useReporter != null ? useReporter.Reporter : null;
        }

        public static void Verify(IExecutableQuery query)
        {
            Verify(
                WriterFactory.CreateTextWriter(query.GetQuery()),
                GetDefaultNamer(),
                new ExecutableQueryFailure(query, GetReporter()));
        }

        public static void Verify(IApprovalWriter writer)
        {
            Verify(writer, GetDefaultNamer(), GetReporter());
        }

        public static void VerifyFile(string receivedFilePath)
        {
            Verify(new ExistingFileWriter(receivedFilePath));
        }

        public static void Verify(FileInfo receivedFilePath)
        {
            VerifyFile(receivedFilePath.FullName);
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
            Verify(WriterFactory.CreateTextWriter(text));
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
            Verify(WriterFactory.CreateTextWriter("" + text));
        }

        public static void Verify(string text, Func<string, string> scrubber)
        {
            Verify(WriterFactory.CreateTextWriter(scrubber(text)));
        }

        public static void Verify(Exception e)
        {
            Verify(e.Scrub());
        }

        #endregion Text

        #region Enumerable

        public static void VerifyAll<T>(string header, IEnumerable<T> enumerable, string label)
        {
            Verify(header + "\n\n" + enumerable.Write(label));
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

        public static void VerifyAll<T>(string header, IEnumerable<T> enumerable,
                                        Func<T, string> formatter)
        {
            Verify(header + "\n\n" + enumerable.Write(formatter));
        }

        public static void VerifyAll<T>(IEnumerable<T> enumerable, Func<T, string> formatter)
        {
            Verify(enumerable.Write(formatter));
        }

        public static void VerifyAll<K, V>(IDictionary<K, V> dictionary)
        {
            dictionary = dictionary ?? new Dictionary<K, V>();
            VerifyAll(dictionary.OrderBy(p => p.Key), p => $"{p.Key} => {p.Value}");
        }

        public static void VerifyAll<K, V>(string header, IDictionary<K, V> dictionary)
        {
            VerifyAll(header, dictionary.OrderBy(p => p.Key), p => $"{p.Key} => {p.Value}");
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
            Verify(new ApprovalBinaryWriter(bytes, fileExtensionWithoutDot));
        }

        public static void VerifyHtml(string html)
        {
            HtmlApprovals.VerifyHtml(html);
        }

        public static void VerifyXml(string xml)
        {
            XmlApprovals.VerifyXml(xml);
        }

        public static void VerifyJson(string json)
        {
            Verify(WriterFactory.CreateTextWriter(json.FormatJson(), "json"));
        }

        #endregion Enumerable

        public static void AssertEquals(string expected, string actual, IApprovalFailureReporter reporter)
        {
            StringReporting.AssertEqual(expected,actual,reporter);
        }

        public static void AssertEquals<T>(string expected, string actual) where T : IApprovalFailureReporter, new()
        {
            StringReporting.AssertEqual(expected, actual, new T());
        }
        public static void AssertEquals(string expected, string actual)
        {
            StringReporting.AssertEqual(expected, actual, GetReporter());
        }
        public static void VerifyPdfFile(string pdfFilePath)
        {
            PdfScrubber.ScrubPdf(pdfFilePath);
            Verify(new ExistingFileWriter(pdfFilePath));
        }
    }
}