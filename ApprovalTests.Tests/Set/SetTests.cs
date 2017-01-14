using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using ApprovalTests.Set;
using ApprovalTests.Reporters;
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
            var list = new List<string>() { "carrot", "apple", "banana" };
            SetApprovals.VerifySet(list, String.Empty);
        }

        public class Foo: IComparable<Foo>
        {
            public string Bar { get; set; }

            public int CompareTo(Foo other)
            {
                return this.Bar.CompareTo(other.Bar);
            }

            public override string ToString()
            {
                return this.Bar;
            }
        }

        [Test]
        public void TestListObjectWithComparable()
        {
            var list = new List<Foo>()
            {
                new Foo() { Bar = "carrot" },
                new Foo() { Bar = "apple" },
                new Foo() { Bar = "banana" },
            };
            SetApprovals.VerifySet(list, String.Empty);
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

        [Test]
        public void TestListWithFormatter()
        {
            var list = new List<string>() { "frobber", "gewgaw", "horse" };        
            SetApprovals.VerifySet(list, s => s.Length.ToString());
        }

        public class ObjectWithProperties
        {
            public string Name { get; set; }
            public int Index { get; set; }
        }

        [Test]
        public void TestListWithProperties()
        {
            var list = new List<ObjectWithProperties>()
            {
                new ObjectWithProperties() { Name = "Jordan", Index = 2 },
                new ObjectWithProperties() { Name = "Barbara", Index = 1 },
            };
            SetApprovals.VerifySet(list, i => StringUtils.WritePropertiesToString(i));
        }
    }
}
