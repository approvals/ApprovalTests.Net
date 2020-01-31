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

                "I am ten chars",
                "**************",
            };
            var samples = new[] { "boo","with \"quotes\".", example1.JoinWith("\n"), };

            Approvals.VerifyAll("C# Strings", samples, s => InlineTextReporter.ConvertToCSharp(s));
        }

        [Test]
        public void Makerheadertest()
        {
            var header = new Header();
            var actual = header.MakeHeading("I am ten chars");
            var expected = new[]{
                "**************",
                "I am ten chars",
                "**************",                };
            Approvals.AssertText(expected, actual);
        }



    }

    public class Header
    {
        public string MakeHeading(string iAmTenChars)
        {
           return new[]
           {
               "**************",

               "I am ten chars",
               "**************",
           }.JoinWith("\n");
        }
    }
}
