using ApprovalTests;
using ApprovalTests.Reporters;
using ApprovalUtilities.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ApprovalUtilities.Tests.Utilities
{
	[TestClass]
	public class StringUtilitiesTest
	{
		[TestMethod]
		public void TestToReadableString()
		{
			Assert.AreEqual("[]", new int[0].ToReadableString());
			Assert.AreEqual("[1, 2, 3]", new int[] {1, 2, 3}.ToReadableString());
		}

		[TestMethod]
		public void TestGrid()
		{
			string grid = StringUtils.DisplayGrid(4, 4, (x, y) => x == y ? "x" : "_");
			Approvals.Verify(grid);
		}

		[TestMethod]
		public void WritePropertiesToStringTest()
		{
			var anonymous = new
												{
													SomeString = "Hello",
													SomeInt = 10
												};
			Approvals.Verify(anonymous.WritePropertiesToString());
		}

		[TestMethod]
		public void WriteOnlyPropertyTest()
		{
			var target = new TestingObject() {WriteOnlyString = "Hello", ReadWriteInt = 10};
			Approvals.Verify(target.WritePropertiesToString());
		}

		[TestMethod]
		public void TestWriteFields()
		{
			var target = new TestingObject() {ThisShouldHaveBeenAProperty = "FooBar"};
			Approvals.Verify(target.WriteFieldsToString());
		}

		[TestMethod]
		public void TestJoinWith()
		{
			var numbers = new[]
											{"one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten", "eleven", "twelve"};
			Approvals.Verify(numbers.JoinWith(" - "));
		}

		[TestMethod]
		public void TestJoinWithTransform()
		{
			var numbers = new[]
											{10,20,30,40,50,60,70,80,90,100,110,120};
			Approvals.Verify(numbers.JoinStringsWith(n => (n/10).ToString(), " - "));
		}

		public class TestingObject
		{
			public string ThisShouldHaveBeenAProperty;
			private string _WriteOnlyString;

			public string WriteOnlyString
			{
				set { _WriteOnlyString = value; }
			}

			public int ReadWriteInt { get; set; }
		}
	}
}