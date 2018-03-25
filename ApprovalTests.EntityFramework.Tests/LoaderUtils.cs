using System;
using System.Linq;
using ApprovalUtilities.Persistence.EntityFramework;

namespace ApprovalTests.EntityFramework.Tests
{
	public class LoaderUtils
	{
		public static LambdaEnumerableLoader<T, ModelContainer> Load<T>(
			Func<ModelContainer, IQueryable<T>> func)
		{
			return Loaders.Create(() => new ModelContainer(), func);
		}
	}
}