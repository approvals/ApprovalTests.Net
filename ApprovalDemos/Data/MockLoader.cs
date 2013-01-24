using ApprovalTests.Persistence;

namespace ApprovalDemos.Data
{
	public class MockLoader<T> : ILoader<T>
	{
		private T t;

		public MockLoader(T t)
		{
			this.t = t;
		}

		public T Load()
		{
			return t;
		}

	}
}