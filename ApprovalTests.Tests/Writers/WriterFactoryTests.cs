using ApprovalTests.Writers;
using NUnit.Framework;

namespace ApprovalTests.Tests.Writers
{
	[TestFixture]
	public class WriterFactoryTests
	{
		[Test]
		public void TestTextWriter()
		{
			Assert.IsInstanceOf<ApprovalTextWriter>(WriterFactory.CreateTextWriter("foo"));
			WriterFactory.SetTextWriterCreator((t) => new MyTextWriter(t));
			Assert.IsInstanceOf<MyTextWriter>(WriterFactory.CreateTextWriter("foo"));
		}

		[Test]
		public void TestTextWriterWithExtension()
		{
			Assert.IsInstanceOf<ApprovalTextWriter>(WriterFactory.CreateTextWriter("foo", ".txt"));
			WriterFactory.SetTextWriterCreator((t, e) => new MyTextWriter(t, e));
			Assert.IsInstanceOf<MyTextWriter>(WriterFactory.CreateTextWriter("foo", "txt"));
		}
	}


	public class MyTextWriter : ApprovalTextWriter
	{
		public MyTextWriter(string data)
			: base(data)
		{
		}

		public MyTextWriter(string data, string extensionWithoutDot)
			: base(data, extensionWithoutDot)
		{
		}
	}
}