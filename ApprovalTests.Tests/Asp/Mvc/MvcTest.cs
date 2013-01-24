using System.Collections.Specialized;
using ApprovalTests.Asp;
using ApprovalTests.Asp.Mvc;
using ApprovalTests.Reporters;
using MvcApplication1.Controllers;
using MvcApplication1.Models;
using NUnit.Framework;

namespace ApprovalTests.Tests.Asp.Mvc
{
	public class MvcTest
	{
		[TestFixture]
		[UseReporter(typeof(TortoiseDiffReporter), typeof(FileLauncherReporter))]
		public class TryingMvcViewApproval
		{
			[SetUp]
			public void Setup()
			{
				PortFactory.MvcPort = 11624;
			}
			[Test]
			public void TestingSomeMvcView()
			{
				//AspApprovals.VerifyUrlViaPost("http://localhost:11624/Cool/Index");
				MvcApprovals.VerifyMvcPage(new CoolController().Index);
			}

			[Test]
			public void TestingPostView()
			{
				MvcApprovals.VerifyUrlViaPost("http://localhost:11624/Cool/SaveName", new NameValueCollection { { "Name", "Henrik" } });
			}

			[Test]
			public void TestingMvcWithPost()
			{
				MvcApprovals.VerifyMvcViaPost<Person>(new CoolController().SaveName, new NameValueCollection { { "Name", "Henrik" } });
			}

			[Test]
			public void TestingMvcWithPost2()
			{
				MvcApprovals.VerifyMvcViaPost<Person>(new CoolController().SaveName, new Person { Name = "Henrik" });
			}

			[Test]
			public void TestWithName()
			{
				MvcApprovals.VerifyMvcPage(new CoolController().TestName);

			}
		}
	}

}