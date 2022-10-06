#if NET48
using System.Collections.Generic;
using ApprovalUtilities.Utilities;
using Xunit;

public class CollectionUtilsTest
{
    [Fact]
    public void TestDictionary()
    {
        var d = new Dictionary<int, string> {{1, "one"}};
        Assert.Equal("one", d.GetValueOrDefault(1));
        Assert.Null(d.GetValueOrDefault(2));
    }
}
#endif