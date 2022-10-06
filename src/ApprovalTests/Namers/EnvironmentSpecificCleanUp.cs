namespace ApprovalTests.Namers;

public class EnvironmentSpecificCleanUp : IDisposable
{
    public void Dispose()
    {
        NamerFactory.Clear();
    }
}