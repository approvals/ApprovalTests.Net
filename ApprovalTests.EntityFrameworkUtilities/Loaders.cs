using System;
using System.Data.Objects;
using System.Linq;

namespace ApprovalTests.EntityFrameworkUtilities
{
    public class Loaders
    {
        public static LambdaEnumerableLoader<T, C> Create<T, C>(C modelContainer, Func<C, IQueryable<T>> func)
            where C : ObjectContext
        {
            return new LambdaEnumerableLoader<T, C>(modelContainer, func);
        }

        public static LambdaEnumerableLoader<T, C> Create<T, C>(Func<C> modelContainer, Func<C, IQueryable<T>> func)
            where C : ObjectContext
        {
            return new LambdaEnumerableLoader<T, C>(modelContainer, func);
        }

        public static LambdaSingleLoader<T, C> CreateSingle<T, C>(Func<C> modelContainer, Func<C, IQueryable<T>> func)
            where C : ObjectContext
        {
            return new LambdaSingleLoader<T, C>(Create(modelContainer, func));
        }
    }
}