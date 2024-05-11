using ApprovalUtilities.Reflection;

namespace ApprovalTests.Events;

public static class EventApprovals
{
    public static void VerifyEvents(object value) =>
        Approvals.Verify(WriteEventsToString(value, ""));

    public static IEnumerable<CallbackDescriptor> GetEventsInformationFor(object value) =>
        value.GetPocoEvents().Concat(value.GetEventHandlerListEvents()).OrderBy(e => e.EventName);

    public static string WriteEventsToString(object value, string label)
    {
        var events = GetEventsInformationFor(value);

        var builder = new StringBuilder();
        builder.AppendLine($"Event Configuration for {value.GetType().Name} {label}");
        builder.AppendLine();

        foreach (var ev in events)
        {
            builder.AppendLine(ev.ToString());
        }

        return builder.ToString();
    }
}