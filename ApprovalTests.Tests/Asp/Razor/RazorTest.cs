using System.IO;
using System.Web.UI.WebControls;
using ApprovalTests.Reporters;
using ApprovalUtilities.Utilities;
using NUnit.Framework;

namespace ApprovalTests.Tests.Asp.Razor
{
	[TestFixture]
	[UseReporter(typeof(FileLauncherReporter))]
	public class RazorTest
	{
		[Test]
		public void TestRenderTest()
		{
			var template = File.ReadAllText(PathUtilities.GetAdjacentFile("Details.cshtml"));
		//	Approvals.ApproveHtml(RazorEngine.Razor.Parse(template, new PageModel() { PatientName = "bob"}));
		}
	}
	
}