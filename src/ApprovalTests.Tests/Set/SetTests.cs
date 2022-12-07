using ApprovalTests.Set;
using System.Text.RegularExpressions;

namespace ApprovalTests.Tests.Set;

[TestFixture]
public class SetTests
{
    [Test]
    public void TestListString()
    {
        // Approved file has order apple, banana, carrot
        var list = new List<string> { "carrot", "apple", "banana" };
        SetApprovals.VerifySet(list, string.Empty);
    }

    public class Foo: IComparable<Foo>
    {
        public string Bar { get; set; }

        public int CompareTo(Foo other)
        {
            return Bar.CompareTo(other.Bar);
        }
    }

    [Test]
    public void TestListObject()
    {
        var list = new List<Foo>
        {
            new() { Bar = "carrot" },
            new() { Bar = "apple" },
            new() { Bar = "banana" },
        };
        SetApprovals.VerifySet(list, string.Empty, f => f.Bar);
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