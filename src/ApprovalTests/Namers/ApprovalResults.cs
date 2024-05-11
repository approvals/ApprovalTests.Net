namespace ApprovalTests.Namers;

public static class ApprovalResults
{
    public static IDisposable UniqueForDotNetVersion() =>
        NamerFactory.AsEnvironmentSpecificTest(GetDotNetVersion());

    public static string GetDotNetVersion() => "Net_v" + Environment.Version;

#if !NETFRAMEWORK
        public static IDisposable UniqueForRuntime(bool throwOnError = true) =>
            NamerFactory.AsEnvironmentSpecificTest(GetDotNetRuntime(throwOnError));

        public static string GetDotNetRuntime(bool throwOnError)
        {
            var frameworkDescription = System.Runtime.InteropServices.RuntimeInformation.FrameworkDescription;
            return GetDotNetRuntime(throwOnError, frameworkDescription);
        }
#endif

    public static string GetDotNetRuntime(bool throwOnError, string frameworkDescription)
    {
        if (frameworkDescription.StartsWith(".NET Framework", StringComparison.OrdinalIgnoreCase))
        {
            var version = Version.Parse(frameworkDescription.Replace(".NET Framework ", ""));
            return $"Net_{version.Major}.{version.Minor}";
        }

        if (frameworkDescription.StartsWith(".NET Core", StringComparison.OrdinalIgnoreCase))
        {
            var version = Version.Parse(frameworkDescription.Replace(".NET Core ", ""));
            var map = new Dictionary<string, string>
            {
                {
                    "4.6","2.1"
                }
            };
            if (map.TryGetValue($"{version.Major}.{version.Minor}", out var result))
            {
                return "NetCore_" + result;
            }
        }

        if (throwOnError)
        {
            throw new NotImplementedException(
                $$"""
                  Your current framework is not properly handled by ApprovalTests
                  Framework: {{frameworkDescription}}.
                  To suppress this error and make the test pass using the full FrameworkDescription use:
                  using (Namers.ApprovalResults.UniqueForRuntime(throwOnError: true)){
                      //The code being tested
                  }
                  To help ApprovalTest please submit a new issue using the following link:
                  https://github.com/approvals/ApprovalTests.Net/issues/new?title=Unknown%3A+%27Runtime%27&body={{frameworkDescription}}

                  """);
        }

        return frameworkDescription;
    }

    public static IDisposable UniqueForMachineName() =>
        NamerFactory.AsEnvironmentSpecificTest(GetMachineName());

    public static string GetMachineName() =>
        "ForMachine." + Environment.MachineName;

    public static string GetOsName()
    {
        var name = TransformEasyOsName(OsUtils.GetFullOsNameFromWmi());
        return name.Trim().Replace(' ', '_');
    }

    public static string GetFullOsName()
    {
        var name = OsUtils.GetFullOsNameFromWmi();
        return name.Trim().Replace(' ', '_');
    }

    public static string TransformEasyOsName(string captionName)
    {
        string[] known = ["XP", "2000", "Vista", "7", "8", "Server 2003", "Server 2008", "Server 2012"];
        var matched = known.FirstOrDefault(s => captionName.StartsWith("Microsoft Windows " + s));
        if (matched != null)
        {
            return "Windows " + matched;
        }

        return captionName;
    }

    public static IDisposable UniqueForOs() =>
        NamerFactory.AsEnvironmentSpecificTest(GetOsName());

    public static string GetUserName() =>
        "ForUser." + Environment.UserName;

    public static IDisposable UniqueForUserName() =>
        NamerFactory.AsEnvironmentSpecificTest(GetUserName());

    public static IDisposable ForScenario(string data)
    {
        var name = "ForScenario." + Scrub(data);
        return NamerFactory.AsEnvironmentSpecificTest(name);
    }

    public static IDisposable ForScenario(params object[] dataPoints) =>
        ForScenario(dataPoints.JoinWith("."));

    public static string Scrub(string data)
    {
        var invalid = Path.GetInvalidFileNameChars().ToArray();
        var chars = data.Select(c => invalid.Contains(c) ? '_' : c).ToArray();
        return new(chars);
    }
}