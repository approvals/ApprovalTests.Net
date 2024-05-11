﻿[TestFixture]
[UseReporter(typeof(ClipboardReporter))]
public class StringEncodingTest
{
    [Test]
    public void TestUtf8()
    {
        var text = "UƬf8";
        Approvals.Verify(text);
    }

    [Test]
    public void TestAscii()
    {
        var text = "ascii";
        Approvals.Verify(text);
    }

#if(NETFRAMEWORK)
    [Test]
    public void TestUnicode()
    {
        var text = System.Text.Encoding.Default.GetString([101, 235, 110, 116]);
        Approvals.Verify(text);
    }
#endif
}