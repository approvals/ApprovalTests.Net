using System.Windows;
using ApprovalTests.Reporters;
using ApprovalTests.Wpf;
using NUnit.Framework;
using Button = System.Windows.Controls.Button;
using ContextMenu = System.Windows.Controls.ContextMenu;
using MenuItem = System.Windows.Controls.MenuItem;
using System;

namespace ApprovalTests.Tests.Wpf
{
    [TestFixture]
    [UseReporter(typeof(AllFailingTestsClipboardReporter), typeof(FileLauncherReporter))]
    public class ApprovalsTest
    {
        [Test]
        [STAThread]
        public void TestFormApproval()
        {
            var button = new Button { Content = "Hello" };
            var window = new Window { Content = button, Width = 200, Height = 200 };
            WpfApprovals.Verify(window);
        }

        [Test]
        [STAThread]
        public void TestContextMenu()
        {
            var menu = new ContextMenu();
            menu.Items.Add(new MenuItem() { Header = "Add Element" });
            menu.Items.Add(new MenuItem() { Header = "Delete" });
            menu.Items.Add(new MenuItem() { Header = "Edit" });
            menu.IsOpen = true;
            WpfApprovals.Verify(menu);
        }

        [Test]
        [STAThread]
        public void TestButton()
        {
            WpfApprovals.Verify(new Button { Content = "Hello" });
        }
    }
}