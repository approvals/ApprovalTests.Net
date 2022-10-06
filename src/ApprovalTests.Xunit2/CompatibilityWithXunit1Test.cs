public class CompatibilityWithXunit1Test
{
    [Fact]
    [UseReporter(typeof(FrameworkAssertReporter))]
    public void Xunit2ShouldWorkWithFrameworkAssertReporter()
    {
        Assert.Throws<EqualException>(() =>
            Approvals.Verify("this should work"));
    }
}