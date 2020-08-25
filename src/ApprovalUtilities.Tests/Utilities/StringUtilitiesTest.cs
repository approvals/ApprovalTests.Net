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
        public void TestRemoveIndentation()
        {
            var text = @"

                      ^^ Blank line above ^^
                      
                      Here is some text
                        1. with some indentation
                        2. and more
                          a. even more
                        3. little less
                      
                      VV Blank line Below VV 
                      
                      ".RemoveIndentation();
          //  Assert.True( text.EndsWith("VV\n"),text.Replace("\n","\\n"));
            Approvals.Verify(text);
        }
        [Fact]
        public void TestRemoveIndentationEnding()
        {
            var text = @"
                      #1 list
                      #2 list
                      ".RemoveIndentation();

            Assert.Equal("#1 list\n#2 list", text);
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