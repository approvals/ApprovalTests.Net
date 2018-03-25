using ApprovalTests.Maintenance;
using NUnit.Framework;

namespace ApprovalUtilities.Tests
{
	public class MaintenanceTest
	{
		[Test]
		public void EnsureNoAbandonedFiles()
		{
#if NETCORE
            ApprovalMaintenance.VerifyNoAbandonedFiles(
                "HandlerListEntryTest.BecomeNullObjectWhenItemIsWrongType.approved.txt",
                "HandlerListEntryTest.ProxyNonPublicMembers.approved.txt",
                "HandlerListUtilitiesTest.EnumerateList.approved.txt",
                "HandlerListUtilitiesTest.GetListHead.approved.txt",
                "ReflectionUtilitiesTest.ControlWithLocalAndBaseKeys.approved.txt",
                "ReflectionUtilitiesTest.GetControlNonPublicStaticFields.approved.txt",
                "ReflectionUtilitiesTest.GetInheritedNonPublicStaticFields.approved.txt",
                "ReflectionUtilitiesTest.GetNonPublicInstanceProperties.approved.txt",
                "ReflectionUtilitiesTest.GetNonPublicInstancePropertiesNamed.approved.txt"
                );
#else
             ApprovalMaintenance.VerifyNoAbandonedFiles();
#endif
        }
    }
}
