using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using ApprovalUtilities.Persistence;

namespace ApprovalTests.EntityFrameworkUtilities
{
    public abstract class MultiRowEntityFrameworkLoader<T, DatabaseContextType> : EntityFrameworkLoader<T, IEnumerable<T>, DatabaseContextType>
        where DatabaseContextType : ObjectContext
    {
        protected MultiRowEntityFrameworkLoader(Func<DatabaseContextType> dbCreator)
            : base(dbCreator)
        {
        }

        protected MultiRowEntityFrameworkLoader(DatabaseContextType nonDisposableDatabaseContext)
            : base(nonDisposableDatabaseContext)
        {
        }
    }

    public class FirstLoader<T> : ILoader<T>
    {
        private readonly ILoader<IEnumerable<T>> loader;

        public FirstLoader(ILoader<IEnumerable<T>> loader)
        {
            this.loader = loader;
        }

        public T Load()
        {
            return loader.Load().First();
        }
    }

    public class OrderedLoader<T, TKey> : ILoader<IOrderedEnumerable<T>>
    {
        private readonly Func<T, TKey> keySelector;
        private readonly ILoader<IQueryable<T>> loader;

        public OrderedLoader(ILoader<IQueryable<T>> loader, Func<T, TKey> keySelector)
        {
            this.loader = loader;
            this.keySelector = keySelector;
        }

        public IOrderedEnumerable<T> Load()
        {
            return loader.Load().OrderBy(keySelector);
        }
    }

    public class PaginatedLoader<T> : ILoader<IEnumerable<T>>
    {
        private readonly ILoader<IOrderedEnumerable<T>> loader;
        private readonly int currentPage;
        private readonly int pageSize;

        public PaginatedLoader(ILoader<IOrderedEnumerable<T>> loader, int currentPage, int pageSize)
        {
            this.loader = loader;
            this.currentPage = currentPage;
            this.pageSize = pageSize;
        }

        public IEnumerable<T> Load()
        {
            var values = loader.Load();
            PageCount = values.Count();
            return values.Skip(currentPage).Take(pageSize);
        }

        public int PageCount { get; private set; }
    }
}