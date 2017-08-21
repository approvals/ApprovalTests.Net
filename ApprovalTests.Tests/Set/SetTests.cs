using System;
using System.Collections.Generic;
using NUnit.Framework;
using ApprovalTests.Set;
using ApprovalUtilities.Utilities;
using System.Text.RegularExpressions;

namespace ApprovalTests.Tests.Set
{
    [TestFixture]
    public class SetTests
    {
        [Test]
        public void TestListString()
        {
            // Approved file has order apple, banana, carrot
            var list = new List<string> { "carrot", "apple", "banana" };
            SetApprovals.VerifySet(list, String.Empty);
        }

        public class Foo: IComparable<Foo>
        {
            public string Bar { get; set; }

            public int CompareTo(Foo other)
            {
                return this.Bar.CompareTo(other.Bar);
            }
        }

        [Test]
        public void TestListObject()
        {
            var list = new List<Foo>
            {
                new Foo { Bar = "carrot" },
                new Foo { Bar = "apple" },
                new Foo { Bar = "banana" },
            };
            SetApprovals.VerifySet(list, String.Empty, f => f.Bar);
        }

        [Test]
        public void TestFile()
        {
            var path = PathUtilities.GetDirectoryForCaller();
            var file = path + "a.txt";
            SetApprovals.VerifyFileAsSet(file);
        }

        [Test]
        public void TestFileWithScrubber()
        {
            var path = PathUtilities.GetDirectoryForCaller();
            var file = path + "a.txt";
            Func<string, string> scrubber =  s => Regex.Replace(s, @"^[^\|]*", "");
            SetApprovals.VerifyFileAsSet(file, scrubber);
        }
    }
}
