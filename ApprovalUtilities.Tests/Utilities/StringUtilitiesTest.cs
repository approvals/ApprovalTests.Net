﻿using ApprovalTests;
using ApprovalUtilities.Utilities;
using NUnit.Framework;

namespace ApprovalUtilities.Tests.Utilities
{
	public class StringUtilitiesTest
	{
		[Test]
		public void TestToReadableString()
		{
			Assert.AreEqual("[]", new int[0].ToReadableString());
			Assert.AreEqual("[1, 2, 3]", new int[] {1, 2, 3}.ToReadableString());
		    int[] empty = null;
			Assert.AreEqual("[]",empty.ToReadableString());
		}

		[Test]
		public void TestGrid()
		{
			string grid = StringUtils.DisplayGrid(4, 4, (x, y) => x == y ? "x" : "_");
			Approvals.Verify(grid);
		}

		[Test]
		public void WritePropertiesToStringTest()
		{
			var anonymous = new
												{
													SomeString = "Hello",
													SomeInt = 10
												};
			Approvals.Verify(anonymous.WritePropertiesToString());
		}

		[Test]
		public void WriteOnlyPropertyTest()
		{
			var target = new TestingObject {WriteOnlyString = "Hello", ReadWriteInt = 10};
			Approvals.Verify(target.WritePropertiesToString());
		}

		[Test]
		public void TestWriteFields()
		{
			var target = new TestingObject {ThisShouldHaveBeenAProperty = "FooBar"};
			Approvals.Verify(target.WriteFieldsToString());
		}

		[Test]
		public void TestJoinWith()
		{
			var numbers = new[]
											{"one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten", "eleven", "twelve"};
			Approvals.Verify(numbers.JoinWith(" - "));
		}

		[Test]
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