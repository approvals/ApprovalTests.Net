using System.IO;
using ApprovalTests.Reporters;
using ApprovalTests.Writers;
using ApprovalUtilities.Utilities;
using NUnit.Framework;

namespace ApprovalTests.Tests.Writers
{
	[TestFixture]
	[UseReporter(typeof(DiffReporter))]
	public class ExistingFileTest
	{

		[Test]
		public void TestExistFileIsApproved()
		{
			var basePath = PathUtilities.GetDirectoryForCaller() + ".." + Path.DirectorySeparatorChar;

			var original = basePath + "a.png";
			var copy = basePath + "a1.png";
			File.Copy(original, copy,true);
			Approvals.Verify(new ExistingFileWriter(copy), Approvals.GetDefaultNamer(), Approvals.GetReporter());
		}

	}
}