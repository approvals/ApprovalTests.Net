using System;

namespace ApprovalTests.Namers
{
	public class UseApprovalSubdirectoryAttribute : Attribute
	{
		public UseApprovalSubdirectoryAttribute(string subdirectory)
		{
			Subdirectory = subdirectory;
		}
		public string Subdirectory { get; private set; }
	}
}