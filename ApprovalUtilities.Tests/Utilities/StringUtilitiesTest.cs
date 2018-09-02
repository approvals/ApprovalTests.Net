using ApprovalTests;
using ApprovalUtilities.Utilities;
using Xunit;

namespace ApprovalUtilities.Tests.Utilities
{
    public class StringUtilitiesTest
    {
        [Fact]
        public void TestToReadableString()
        {
            Assert.Equal("[]", new int[0].ToReadableString());
            Assert.Equal("[1, 2, 3]", new[] {1, 2, 3}.ToReadableString());
            int[] empty = null;
            Assert.Equal("[]", empty.ToReadableString());
        }

        [Fact]
        public void TestGrid()
        {
            var grid = StringUtils.DisplayGrid(4, 4, (x, y) => x == y ? "x" : "_");
            Approvals.Verify(grid);
        }

        [Fact]
        public void WritePropertiesToStringTest()
        {
            var anonymous = new
            {
                SomeString = "Hello",
                SomeInt = 10
            };
            Approvals.Verify(anonymous.WritePropertiesToString());
        }

        [Fact]
        public void WriteOnlyPropertyTest()
        {
            var target = new TestingObject
            {
                WriteOnlyString = "Hello",
                ReadWriteInt = 10
            };
            Approvals.Verify(target.WritePropertiesToString());
        }

        [Fact]
        public void TestWriteFields()
        {
            var target = new TestingObject {ThisShouldHaveBeenAProperty = "FooBar"};
            Approvals.Verify(target.WriteFieldsToString());
        }

        [Fact]
        public void TestJoinWith()
        {
            var numbers = new[]
                {"one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten", "eleven", "twelve"};
            Approvals.Verify(numbers.JoinWith(" - "));
        }

        [Fact]
        public void TestJoinWithTransform()
        {
            var numbers = new[]
                {10, 20, 30, 40, 50, 60, 70, 80, 90, 100, 110, 120};
            Approvals.Verify(numbers.JoinStringsWith(n => (n / 10).ToString(), " - "));
        }

        public class TestingObject
        {
            public string ThisShouldHaveBeenAProperty;
            private string _WriteOnlyString;

            public string WriteOnlyString
            {
                set => _WriteOnlyString = value;
            }

            public int ReadWriteInt { get; set; }
        }
    }
}