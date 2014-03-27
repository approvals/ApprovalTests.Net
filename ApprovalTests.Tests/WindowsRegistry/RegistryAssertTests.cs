using ApprovalTests.WindowsRegistry;
using ApprovalUtilities.Utilities;
using NUnit.Framework;

namespace ApprovalTests.Tests.WindowsRegistry
{
	[TestFixture]
	public class RegistryAssertTests
	{
		private const string KeyName = @"Software\TestDummy";

		[Test]
		public void Success()
		{
			WindowsRegistryAssert.HasDword("Console", "TrimLeadingZeros", 0, "You should probably Have This");
		}

		[Test]
		public void BadValue()
		{
			var e =
				ExceptionUtilities.GetException(
					() => WindowsRegistryAssert.HasDword("Console", "TrimLeadingZeros", 1984, "This should not be this value"));
			Approvals.Verify(e.Message);
		}

		[Test]
		public void MissingKey()
		{
			var e =
				ExceptionUtilities.GetException(
					() => WindowsRegistryAssert.HasDword(@"Console\IMadeThisUp\AlsoThis", "", 1, "This Value is gone"));
			Approvals.Verify(e.Message);
		}	
		[Test]
		public void MissingValue()
		{
			var e =
				ExceptionUtilities.GetException(
					() => WindowsRegistryAssert.HasDword(@"Console", "IMadeThisUp", 1, "This Value is gone"));
			Approvals.Verify(e.Message);
		}
	}
}