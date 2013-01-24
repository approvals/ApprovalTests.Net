using System.Collections.Generic;
using System.Linq;
using System.Text;
using ApprovalUtilities.Reflection;
using ApprovalUtilities.Utilities;

namespace ApprovalTests.Events
{
    public static class EventApprovals
    {
        public static void VerifyEvents(object value)
        {
            Approvals.Verify(WriteEventsToString(value, ""));
        }

        public static IEnumerable<CallbackDescriptor> GetEventsInformationFor(object value)
        {
            return value.GetPocoEvents().Concat(value.GetEventHandlerListEvents()).OrderBy(e => e.EventName);
        }

        public static string WriteEventsToString(object value, string label)
        {
            var events = GetEventsInformationFor(value);

            var sb = new StringBuilder();
            sb.AppendLine("Event Configuration for {0} {1}".FormatWith(value.GetType().Name, label));
            sb.AppendLine();

            foreach (var ev in events)
            {
                sb.AppendLine(ev.ToString());
            }
            return sb.ToString();
        }
    }
}