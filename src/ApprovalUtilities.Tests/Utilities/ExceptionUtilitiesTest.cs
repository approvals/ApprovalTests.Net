public class ExceptionUtilitiesTest
{
    [Fact]
    public void TestGetException() =>
        AssertException<NotFiniteNumberException>(() => throw new NotFiniteNumberException());

    static void AssertException<T>(Action action) where T : Exception =>
        Assert.IsType<T>(ExceptionUtilities.GetException(action));
}