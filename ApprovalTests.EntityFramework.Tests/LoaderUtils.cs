using System;
using System.Linq;
using ApprovalTests.EntityFrameworkUtilities;

namespace ApprovalTests.Tests.EntityFramework
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