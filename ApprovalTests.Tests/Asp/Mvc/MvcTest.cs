using System;
using System.Collections.Specialized;
using ApprovalTests.Asp;
using ApprovalTests.Asp.Mvc;
using ApprovalTests.Reporters;
using MvcApplication1;
using MvcApplication1.Controllers;
using MvcApplication1.Models;
using NUnit.Framework;

namespace ApprovalTests.Tests.Asp.Mvc
{
    public static class MvcTest
    {
        [TestFixture]
        [UseReporter(typeof(TortoiseDiffReporter), typeof(FileLauncherWithDelayReporter))]
        public class TryingMvcViewApproval : ServerDependentTest
        {
            public TryingMvcViewApproval() :
                base(MvcApplication.Directory, 11624)
            {
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
            public void TestingPostView()
            {
                MvcApprovals.VerifyUrlViaPost(String.Format("http://localhost:{0}/Cool/SaveName", PortFactory.MvcPort), new NameValueCollection { { "Name", "Henrik" } });
            }

            [Test]
            public void TestingSomeMvcView()
            {
                //AspApprovals.VerifyUrlViaPost("http://localhost:11624/Cool/Index");
                MvcApprovals.VerifyMvcPage(new CoolController().Index);
            }

# if DEBUG

            [Test]
            public void TestWithName()
            {
                MvcApprovals.VerifyMvcPage(new CoolController().TestName);
            }

#endif
        }
    }
}