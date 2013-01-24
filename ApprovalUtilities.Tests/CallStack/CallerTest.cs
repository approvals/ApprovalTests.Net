using System.Linq;
using ApprovalTests;
using ApprovalUtilities.CallStack;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ApprovalUtilities.Tests.CallStack
{
	[TestClass]
	public class CallerTest
	{
		[TestMethod]
		public void TestGetCaller()
		{
			var caller = GetCaller();
			Assert.AreEqual("TestGetCaller", caller.Method.Name);
			Assert.AreEqual("CallerTest", caller.Class.Name);
		}

		private Caller GetCaller()
		{
			return new Caller();
		}

		[TestMethod]
		public void TestCallStack()
		{
			var caller = GetDeepCaller();
			var callers = caller.Callers.Where(c => c.Method.DeclaringType == this.GetType());
			Approvals.VerifyAll(callers, c => c.Method.ToStandardString());
		}
		[TestMethod]
		public void TestName()
		{
			var caller = GetDeepCaller();
			var methods = caller.Methods.Where(m => m.DeclaringType == this.GetType());
			Approvals.VerifyAll(methods, m => m.ToStandardString());
		}

		private Caller GetDeepCaller()
		{
			return A();
		}

		private Caller A()
		{
			return B();
		}

		private Caller B()
		{
			return C();
		}

		private Caller C()
		{
			return D();
		}

		private Caller D()
		{
			return E();
		}

		private Caller E()
		{
			return GetCaller();
		}
	}


}