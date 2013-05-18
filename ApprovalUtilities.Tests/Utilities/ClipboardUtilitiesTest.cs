using System.Windows.Forms;
using ApprovalUtilities.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ApprovalUtilities.Tests.Utilities
{
	[TestClass]
	public class ClipboardUtilitiesTest
	{
		// This messes up your clipboard
		[Ignore]
		[TestMethod]
		public void TestClipboard()
		{
			ClipboardUtilities.CopyToClipboard("fred");
			var altered = Clipboard.GetText();
			Assert.AreEqual("fred", altered);
		}
	}
}