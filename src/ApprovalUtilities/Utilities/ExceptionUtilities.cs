namespace ApprovalUtilities.Utilities;

public static class ExceptionUtilities
{
    public static string FormatException(Exception exception, params string[] additional) =>
        string.Join("\n", GetExceptionLines(exception, additional));

    public static string[] GetExceptionLines(Exception except, params string[] additional)
    {
        var lines = new List<string>
        {
            $"Exception: '{except.TargetSite}' | '{except.Source}'",
            except.Message,
            except.StackTrace
        };
        lines.AddRange(additional);

        if (except.InnerException != null)
        {
            lines.AddRange(GetExceptionLines(except.InnerException));
        }

        return lines.ToArray();
    }

    public static string FormatAsError(params string[] lines) =>
        StringUtils.FormatFrame('*', lines);

    public static string FormatError(this Exception except, params string[] additional) =>
        FormatAsError(GetExceptionLines(except, additional));

    public static Exception GetException(Action action)
    {
        try
        {
            action.Invoke();
            return null;
        }
        catch (Exception ex)
        {
            return ex;
        }
    }
}