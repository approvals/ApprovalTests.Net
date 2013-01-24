using System;
using System.Diagnostics;
using ApprovalTests.Reporters;
using ApprovalTests.StackTraceParsers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace ApprovalTests.MSTest
{
	[TestClass]
	[UseReporter(typeof(DiffReporter))]
	public class NamerTest
	{
		
		[TestMethod]
		public void MSTestVS2010()
		{
			
			Approvals.Verify("2010");
		}
	}

	
}