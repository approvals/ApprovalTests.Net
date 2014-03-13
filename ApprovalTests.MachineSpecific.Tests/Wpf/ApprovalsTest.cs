using System;
using System.Windows;
using System.Windows.Controls;
using ApprovalTests.Reporters;
using ApprovalTests.Wpf;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ApprovalTests.MachineSpecific.Tests.Wpf
{
	[TestClass]
	[UseReporter(typeof (AllFailingTestsClipboardReporter), typeof (DiffReporter))]
	public class ApprovalsTest
	{
		[TestMethod]
		[STAThread]
		public void TestFormApproval()
		{
			var button = new Button {Content = "Hello"};
			var window = new Window {Content = button, Width = 200, Height = 200};
			WpfApprovals.Verify(window);
		}

		[TestMethod]
		[STAThread]
		public void TestContextMenu()
		{
			var menu = new ContextMenu();
			menu.Items.Add(new MenuItem() {Header = "Add Element"});
			menu.Items.Add(new MenuItem() {Header = "Delete"});
			menu.Items.Add(new MenuItem() {Header = "Edit"});
			menu.IsOpen = true;
			WpfApprovals.Verify(menu);
		}

		[TestMethod]
		[STAThread]
		public void TestButton()
		{
			WpfApprovals.Verify(new Button {Content = "Hello"});
		}
	}
}