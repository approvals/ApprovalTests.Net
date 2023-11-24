#if NET48

using ApprovalTests.Core.Exceptions;
using ApprovalTests.TheoryTests;
[TestFixture]
public class SerializableExceptionsTest
{
    [Test]
    public void TestSerializable()
    {
        var r = "received";
        var a = "approved";
        Verify(new ApprovalMissingException(r, a));
        Verify(new ApprovalMismatchException(r, a));
        Verify(new ApprovalException(r, a));
    }

    static void Verify(object o) =>
        SerializableTheory.Verify(o, Assert.AreEqual);
}
#endif