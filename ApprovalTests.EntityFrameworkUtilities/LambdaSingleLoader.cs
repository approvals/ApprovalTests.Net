using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;

namespace ApprovalTests.EntityFrameworkUtilities
{
    public class LambdaSingleLoader<T, C> : EntityFrameworkLoader<T, T, C>
        where C : ObjectContext
    {
        private readonly EntityFrameworkLoader<T, IEnumerable<T>, C> loader;


        public LambdaSingleLoader(EntityFrameworkLoader<T, IEnumerable<T>, C> loader)
            : base((C) null)
        {
            this.loader = loader;
        }

        public override IQueryable<T> GetLinqStatement()
        {
            return loader.GetLinqStatement().Take(1);
        }

        public override T Load()
        {
            return GetLinqStatement().FirstOrDefault();
        }

        public override string ExecuteQuery(string query)
        {
            return loader.ExecuteQuery(query);
        }
    }
}