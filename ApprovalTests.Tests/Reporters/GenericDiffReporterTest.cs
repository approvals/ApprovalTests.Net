using ApprovalTests.Reporters;
using ApprovalUtilities.Utilities;
using NUnit.Framework;
using System.IO;
using ApprovalTests.Reporters.Windows;

namespace ApprovalTests.Tests.Reporters
{
    [TestFixture]
    public class GenericDiffReporterTest
    {
        [Test]
        public void TestGetActualProgramFileEchos()
        {
            var NoneExistingFile = @"C:\ThisDirectoryShouldNotExist\ThisFileShouldNotExist.exe";
            Assert.AreEqual(NoneExistingFile, GenericDiffReporter.GetActualProgramFile(NoneExistingFile));
        }

        [Test]
        [UseReporter(typeof(MachineSpecificReporter))]
        public void TestMissingDots()
        {
            using (Namers.ApprovalResults.UniqueForOs())
            {
                var e = ExceptionUtilities.GetException(() => GenericDiffReporter.RegisterTextFileTypes(".exe", "txt", ".error", "asp"));
                Approvals.Verify(e);
            }
        }

        [Test]
        public void TestProgramsExist()
        {
            Assert.IsFalse(new GenericDiffReporter("this_should_never_exist", "").IsWorkingInThisEnvironment("any.txt"));
        }

        [Test]
        public void TestRegisterWorks()
        {
            var r = new CodeCompareReporter();
            GenericDiffReporter.RegisterTextFileTypes(".myCrazyExtension");
            Assert.IsTrue(r.IsValidFileType("file.myCrazyExtension"));
        }

        [Test]
        [UseReporter(typeof(ClipboardReporter))]
        public void TestEnsureFileExist()
        {
            var imageFile = PathUtilities.GetAdjacentFile("TestImage.png");
            if (File.Exists(imageFile))
            {
                File.Delete(imageFile);
            }

            GenericDiffReporter.EnsureFileExists(imageFile);
            Approvals.VerifyFile(imageFile);
        }
    }
}