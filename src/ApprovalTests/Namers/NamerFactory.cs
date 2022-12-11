namespace ApprovalTests.Namers;

public static class NamerFactory
{
    static AsyncLocal<string> additionalInformation = new();

    public static string AdditionalInformation
    {
        get => additionalInformation.Value;
        set => additionalInformation.Value = value;
    }

    public static IDisposable AsEnvironmentSpecificTest(string label)
    {
        if (AdditionalInformation == null)
        {
            AdditionalInformation = label;
        }
        else
        {
            AdditionalInformation += "." + label;
        }

        return new EnvironmentSpecificCleanUp();
    }

    public static void Clear() => AdditionalInformation = null;
}