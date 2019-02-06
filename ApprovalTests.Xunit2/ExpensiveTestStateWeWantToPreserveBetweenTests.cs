namespace ApprovalTests.Xunit2
{
    public class ExpensiveTestStateWeWantToPreserveBetweenTests : System.IDisposable
    {
        public string ConnectionString { get; }

        public ExpensiveTestStateWeWantToPreserveBetweenTests() {
            // pretend we have created a localdb database and seeded it with test data
            ConnectionString = "foo";
        }

        public void Dispose() {
            // would typically ensure the database is deleted here.
        }
    }
}