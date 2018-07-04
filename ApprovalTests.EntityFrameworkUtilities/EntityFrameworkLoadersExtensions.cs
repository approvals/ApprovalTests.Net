using System.Collections.Generic;
using System.Data.Objects;

namespace ApprovalTests.EntityFrameworkUtilities
{
    public static class EntityFrameworkLoadersExtensions
    {
        public static LambdaSingleLoader<T, C> Singleton<T, C>(this EntityFrameworkLoader<T, IEnumerable<T>, C> otherLoader)
            where C : ObjectContext
        {
            return new LambdaSingleLoader<T, C>(otherLoader);
        }
    }
}