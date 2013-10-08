using ApprovalTests.Events;
using ApprovalUtilities.Reflection;
using ApprovalUtilities.Utilities;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace ApprovalTests.WinForms
{
    public class WinFormsApprovals
    {
        public static void VerifyEventsFor(Form form)
        {
            var sb = new StringBuilder();
            sb.Append(EventApprovals.WriteEventsToString(form, ""));

            foreach (var o in GetSubEvents(form))
            {
                sb.Append(EventApprovals.WriteEventsToString(o, GetLabelForChild(form, o)));
            }

            Approvals.Verify(sb.ToString());
        }

        public static void Verify(Form form)
        {
            Approvals.Verify(new ApprovalFormWriter(form));
        }

        public static void Verify(Control control)
        {
            Approvals.Verify(new ApprovalControlWriter(control));
        }

        private static string GetLabelForChild(object parent, object child)
        {
            FieldInfo field = ReflectionUtilities.GetFieldForChild(parent, child);
            return "({0}.{1})".FormatWith(parent.GetType().Name, field.Name);
        }

        private static IEnumerable<object> GetSubEvents(Form form)
        {
            return form.GetInstanceFields()
                .Select(fi => fi.GetValue(form))
                .Where(o => EventApprovals.GetEventsInformationFor(o).Count() > 0);
        }
    }
}