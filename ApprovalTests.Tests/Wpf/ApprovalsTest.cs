using System.Windows.Controls;
using System.Windows;
using System.Windows.Data;
using ApprovalTests.Reporters;
using ApprovalTests.Wpf;
using NUnit.Framework;
using System;

namespace ApprovalTests.Tests.Wpf
{
    [TestFixture]
    [UseReporter(typeof(AllFailingTestsClipboardReporter), typeof(DiffReporter))]
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

        class ViewModel
        {
            public string Text { get { return "Hello"; } }
        }

        [Test]
        [STAThread]
        public void TestWindowDataBinding()
        {
            var button = CreateButtonWithBinding();

            var window = new Window { Content = button, Width = 200, Height = 200, DataContext = new ViewModel() };
            WpfApprovals.Verify(window);
        }

        [Test]
        [STAThread]
        public void TestControlDataBinding()
        {
            var button = CreateButtonWithBinding();

            object viewModel = new ViewModel();
            WpfApprovals.Verify(button, viewModel);
        }

        private static Button CreateButtonWithBinding()
        {
            var button = new Button();
            var myBinding = new Binding("Text");
            BindingOperations.SetBinding(button, Button.ContentProperty, myBinding);
            return button;
        }
    }
}