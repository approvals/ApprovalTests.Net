namespace ApprovalTests;

public class Approvals
{
    static readonly ThreadLocal<Caller> currentCaller = new();
    static Func<IApprovalNamer> defaultNamerCreator = () => new UnitTestFrameworkNamer();
    static Func<IApprovalWriter, IApprovalNamer, bool, IApprovalApprover> defaultApproverCreator = (writer, namer, shouldIgnoreLineEndings) => new FileApprover(writer, namer, shouldIgnoreLineEndings);

    public static Caller CurrentCaller
    {
        get
        {
            if (currentCaller.Value == null)
            {
                SetCaller();
            }

            return currentCaller.Value;
        }
    }

    public static void SetCaller()
    {
        currentCaller.Value = new();
    }

    //begin-snippet: complete_verify_call
    public static void Verify(IApprovalWriter writer, IApprovalNamer namer, IApprovalFailureReporter reporter)
        //end-snippet
    {
        var normalizeLineEndingsForTextFiles = CurrentCaller.GetFirstFrameForAttribute<IgnoreLineEndingsAttribute>();
        var shouldIgnoreLineEndings = normalizeLineEndingsForTextFiles == null || normalizeLineEndingsForTextFiles.IgnoreLineEndings;
        var approver = GetDefaultApprover(writer, namer, shouldIgnoreLineEndings);
        Verify(approver, reporter);
    }

    public static void RegisterDefaultApprover(Func<IApprovalWriter, IApprovalNamer, bool, IApprovalApprover> creator)
    {
        defaultApproverCreator =  creator;
    }

    static IApprovalApprover GetDefaultApprover(IApprovalWriter writer, IApprovalNamer namer, bool shouldIgnoreLineEndings)
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

    static IEnvironmentAwareReporter GetFrontLoadedReporterFromAttribute()
    {
        var frontLoaded = CurrentCaller.GetFirstFrameForAttribute<FrontLoadedReporterAttribute>();
        return frontLoaded != null ? frontLoaded.Reporter : FrontLoadedReporterDisposer.Default;
    }

    static IApprovalFailureReporter GetFrontLoadedReporter(IApprovalFailureReporter defaultIfNotFound,
        IEnvironmentAwareReporter frontLoad)
    {
        return frontLoad.IsWorkingInThisEnvironment("default.txt")
            ? frontLoad
            : GetReporterFromAttribute() ?? defaultIfNotFound;
    }

    static IEnvironmentAwareReporter WrapAsEnvironmentAwareReporter(IApprovalFailureReporter mainReporter)
    {
        if (mainReporter is IEnvironmentAwareReporter reporter)
        {
            return reporter;
        }
        return new AlwaysWorksReporter(mainReporter);
    }

    static IApprovalFailureReporter GetReporterFromAttribute()
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

    public static void RegisterDefaultNamerCreation(Func<IApprovalNamer> creator)
    {
        defaultNamerCreator = creator;
    }

    public static IApprovalNamer GetDefaultNamer()
    {
        return defaultNamerCreator.Invoke();
    }
    /// <summary>
    /// This is sometimes needed on CI systems that move/remove the original source.
    /// If you use this you will also need to set the .approved. files to "Copy Always"
    /// and if you use subdirectories you'll need a post-build command like
    /// copy /y $(ProjectDir)**subfolder**\*.approved.txt $(TargetDir)
    /// </summary>
    public static void UseAssemblyLocationForApprovedFiles()
    {
        RegisterDefaultNamerCreation(()=> new AssemblyLocationNamer());
    }

    public static void Verify(object text)
    {
        Verify(WriterFactory.CreateTextWriter(text.ToString()));
    }

    public static void Verify(string text, Func<string, string> scrubber = null)
    {
        VerifyWithExtension(text, ".txt", scrubber);
    }

    //begin-snippet: verify_with_extension
    public static void VerifyWithExtension(string text, string fileExtensionWithDot, Func<string, string> scrubber = null)
        //end-snippet
    {
        if (scrubber == null)
        {
            scrubber = ScrubberUtils.NO_SCRUBBER;
        }

        var fileExtensionWithoutDot = fileExtensionWithDot.TrimStart('.');
        Verify(WriterFactory.CreateTextWriter(scrubber(text), fileExtensionWithoutDot));
    }

    public static void VerifyException(Exception e)
    {
        Verify($"{e.GetType().FullName}: {e.Message}");
    }

    public static void VerifyExceptionWithStacktrace(Exception e)
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
        dictionary ??= new Dictionary<K, V>();
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

    public static void VerifyBinaryFile(byte[] bytes, string fileExtensionWithDot)
    {
        var fileExtensionWithoutDot = fileExtensionWithDot.TrimStart('.');
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
        VerifyWithExtension(json.FormatJson(), ".json");
    }

    #endregion Enumerable

    public static void AssertEquals(string expected, string actual, IApprovalFailureReporter reporter)
    {
        StringReporting.AssertEqual(expected, actual, reporter);
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

    public static IDisposable SetFrontLoadedReporter(IEnvironmentAwareReporter reporter)
    {
        return new FrontLoadedReporterDisposer(reporter);
    }

    public static void AssertText(string[] expected, string actual, IApprovalFailureReporter reporter = null)
    {
        AssertText(expected.JoinWith("\n"), actual);
    }
    public static void AssertText(string expected, string actual, IApprovalFailureReporter reporter = null)
    {
        if (reporter == null)
        {
            reporter = GetReporter(DiffReporter.INSTANCE);
        }
        reporter = new MultiReporter(reporter, InlineTextReporter.INSTANCE);
        StringReporting.AssertEqual(expected, actual, reporter);
    }
}