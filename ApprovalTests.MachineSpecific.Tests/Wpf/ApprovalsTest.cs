using System;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using ApprovalTests.Reporters;
using ApprovalTests.Wpf;
using NUnit.Framework;

namespace ApprovalTests.MachineSpecific.Tests.Wpf
{
    [TestFixture]
    [UseReporter(typeof (AllFailingTestsClipboardReporter), typeof (TortoiseDiffReporter))]
    public class ApprovalsTest
    {
        [Test]
        [RequiresThread(ApartmentState.STA)]
        public void TestFormApproval()
        {
            var button = new Button {Content = "Hello"};
            var window = new Window {Content = button, Width = 200, Height = 200};
            WpfApprovals.Verify(window);
        }

        [Test]
        [RequiresThread(ApartmentState.STA)]
        public void TestWindowFunc()
        {
            WpfApprovals.Verify(() => new Window {Content = new Button {Content = "Hello from Lambdas"}, Width = 200, Height = 200});
        }


        [Test]
        [RequiresThread(ApartmentState.STA)]
        public void TestContextMenu()
        {
            var menu = new ContextMenu();
            menu.Items.Add(new MenuItem {Header = "Add Element"});
            menu.Items.Add(new MenuItem {Header = "Delete"});
            menu.Items.Add(new MenuItem {Header = "Edit"});
            menu.IsOpen = true;
            WpfApprovals.Verify(menu);
        }

        [Test]
        [RequiresThread(ApartmentState.STA)]
        public void TestButton()
        {
            WpfApprovals.Verify(new Button {Content = "Hello"});
        }

        [Test]
        [RequiresThread(ApartmentState.STA)]
        public void TestButtonFunc()
        {
            WpfApprovals.Verify(() => new Button {Content = "Hello From Lambdas"});
        }
    }
}