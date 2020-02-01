using System;
using ApprovalTests.Core;
using ApprovalTests.Reporters;
using ApprovalTests.Utilities;
using ApprovalUtilities.Utilities;
using NUnit.Framework;

namespace ApprovalTests.Tests.Reporters
{
    [TestFixture]
    public class InlineTextReporterTest
    {
        [Test]
        public void TestComment()
        {
            var actual = "Hello";
            var expected = "Hello";
            Approvals.AssertText(expected, actual);
        }

        [Test]
        public void CSharpStrings()
        {
            var example1 = new[]
            {
                "**************",
                "I am a heading",
                "**************",
            };
            var samples = new[] { "boo","with \"quotes\".", example1.JoinWith("\n"), };

            Approvals.VerifyAll("C# Strings", samples, s => InlineTextReporter.ConvertToCSharp(s));
        }

        [Test]
        public void AssertTextTest()
        {
            // begin-snippet: assert_text
            var header = new Header();
            var actual = header.MakeHeading("I am a heading");
            var expected = new[]{
                "**************",
                "I am a heading",
                "**************",
            };
            Approvals.AssertText(expected, actual);
            // end-snippet
        }

        [Test]
        public void AssertTextArraysTest()
        {
            // begin-snippet: assert_text_arrays
            var header = new Header();
            var actual = header.MakeArrayHeading("I am a heading");
            var expected = new[]{
                "**************",
                "I am a heading",
                "**************",
            };
            Approvals.AssertText(expected, actual);
            // end-snippet
        }


        public void MakerheaderBefore()
        {
            // begin-snippet: assert_text_before
            var header = new Header();
            var actual = header.MakeHeading("I am a heading");
            var expected = "";
            Approvals.AssertText(expected, actual);
            // end-snippet
        }
    }

    public class Header
    {
        public string MakeHeading(string heading)
        {
           return MakeArrayHeading(heading).JoinWith("\n");
        }
        public string[] MakeArrayHeading(string heading)
        {
            return new[]
            {
               "**************",
               "I am a heading",
               "**************",
           };
        }

    }
}
