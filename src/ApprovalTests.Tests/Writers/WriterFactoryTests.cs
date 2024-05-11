using ApprovalTests.Writers;

[TestFixture]
public class WriterFactoryTests
{
    [Test]
    public void TestTextWriter()
    {
        ClassicAssert.IsInstanceOf<ApprovalTextWriter>(WriterFactory.CreateTextWriter("foo"));
        WriterFactory.SetTextWriterCreator(t => new MyTextWriter(t));
        ClassicAssert.IsInstanceOf<MyTextWriter>(WriterFactory.CreateTextWriter("foo"));
    }

    [Test]
    public void TestTextWriterWithExtension()
    {
        ClassicAssert.IsInstanceOf<ApprovalTextWriter>(WriterFactory.CreateTextWriter("foo", ".txt"));
        WriterFactory.SetTextWriterCreator((t, e) => new MyTextWriter(t, e));
        ClassicAssert.IsInstanceOf<MyTextWriter>(WriterFactory.CreateTextWriter("foo", "txt"));
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