using System.Windows.Forms;
using ApprovalTests.Events;
using ApprovalTests;
using System.Text;
using ApprovalUtilities.Reflection;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using ApprovalUtilities.Utilities;

namespace ApprovalTests.WinForms
{
    public class WinFormsApprovals
    {
        private static string GetLabelForChild(object parent, object child)
        {
            FieldInfo field = ReflectionUtilities.GetFieldForChild(parent, child);
            return "({0}.{1})".FormatWith(parent.GetType().Name, field.Name);
        }

        public static void VerifyEventsFor(Form form)
        {
            var sb = new StringBuilder();
            sb.Append(EventApprovals.WriteEventsToString(form, ""));

            foreach (var o in GetSubEvents(form))
            {
                sb.Append(EventApprovals.WriteEventsToString(o, GetLabelForChild(form, o)));
            }

            ApprovalTests.Approvals.Verify(sb.ToString());
        }

        private static IEnumerable<object> GetSubEvents(Form form)
        {
            return form.GetInstanceFields()
                .Select(fi => fi.GetValue(form))
                .Where(o => EventApprovals.GetEventsInformationFor(o).Count() > 0);
        }

        public static void Verify(Form form)
        {
            ApprovalTests.Approvals.Verify(new ApprovalFormWriter(form));
        }

        public static void Verify(Control control)
        {
            ApprovalTests.Approvals.Verify(new ApprovalControlWriter(control));
        }
    }
}