using System;
using System.Collections.Generic;
using ApprovalTests.Core;
using ApprovalTests.Reporters;
using NUnit.Framework;
using System.Linq;

namespace ApprovalTests.Tests.Reporters
{
	[TestFixture]
	[UseReporter(typeof(ClassLevelReporter))]
	public class ReporterFactoryTest
	{
		[Test]
		public void TestSingletonOnAllReporters()
		{
			var reporters = GetSingletonReporterTypes();
			foreach (var r in reporters)
			{
				Assert.IsInstanceOf(r, UseReporterAttribute.GetSingleton(r));
			}
		}
		private static IEnumerable<Type> GetSingletonReporterTypes()
		{
			var types = typeof(UseReporterAttribute).Assembly.GetTypes();
			var reporters = types.Where(r => r.GetInterfaces().Contains(typeof(IApprovalFailureReporter)));
			var singletons = reporters.Where(r => r.GetConstructor(new Type[0]) != null);
			return singletons;
		}
		[Test]
		public void TestClassLevel()
		{
			Assert.AreEqual(typeof(ClassLevelReporter), Approvals.GetReporter().GetType());
		}

		[Test]
		[UseReporter(typeof(MethodLevelReporter))]
		public void TestMethodOverride()
		{
			Assert.AreEqual(typeof(MethodLevelReporter), Approvals.GetReporter().GetType());
		}
		[Test]
		[UseReporter(typeof(MethodLevelReporter), typeof(ClassLevelReporter))]
		public void TestMultipleReporters()
		{
			Assert.AreEqual(typeof(MultiReporter), Approvals.GetReporter().GetType());
		}
		[Test]
		[UseReporter(typeof(MethodLevelReporter))]
		public void TestMethodOverrideWithSubMethod()
		{
			SubMethod();
		}

		private void SubMethod()
		{
			Assert.AreEqual(typeof(MethodLevelReporter), Approvals.GetReporter().GetType());
		}
	}


	public class MethodLevelReporter : IApprovalFailureReporter
	{

		public void Report(string approved, string received)
		{
		}


	}

	public class ClassLevelReporter : IApprovalFailureReporter
	{

		public void Report(string approved, string received)
		{
		}



	}
}