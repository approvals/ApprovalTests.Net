#if !NETCORE
using System.Windows.Forms;
using ApprovalUtilities.Utilities;
using NUnit.Framework;

namespace ApprovalUtilities.Tests.Utilities
{
	public class ClipboardUtilitiesTest
	{
		[Ignore("This messes up your clipboard")]
        [Test]
		public void TestClipboard()
		{
			ClipboardUtilities.CopyToClipboard("fred");
			var altered = Clipboard.GetText();
			Assert.AreEqual("fred", altered);
		}
	}
}
#endif