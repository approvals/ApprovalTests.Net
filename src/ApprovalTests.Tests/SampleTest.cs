// begin-snippet: sample_test
[UseReporter(typeof(VisualStudioReporter))]
[TestFixture]
public class SampleTest
{
    [Test]
    public void TestList()
    {
        var names = new[] {"Llewellyn", "James", "Dan", "Jason", "Katrina"};
        Array.Sort(names);
        Approvals.VerifyAll(names, label: "");
    }
}

// end-snippet