using System.Data.Common;
using System.Linq;
using System.Text.RegularExpressions;
using ApprovalUtilities.Persistence.Database;
using ApprovalUtilities.Reflection;
using NHibernate;
using NHibernate.AdoNet.Util;
using NHibernate.Engine;
using NHibernate.Hql.Ast.ANTLR;
using NHibernate.Linq;
using NHibernate.Linq.Visitors;

namespace ApprovalUtilities.Persistence.NHibernate
{
    public class NhQueryableAdaptor<T> : IDatabaseToExecuteableQueryAdaptor
    {
        private readonly NhQueryable<T> queryable;

        public NhQueryableAdaptor(NhQueryable<T> queryable)
        {
            this.queryable = queryable;
        }

        public string GetQuery()
        {
            return GetGeneratedSql(queryable, GetSession(queryable));
        }

        public string GetGeneratedSql(IQueryable queryable, ISession session)
        {
            var sessionImp = (ISessionImplementor)session;
            var nhLinqExpression = new NhLinqExpression(queryable.Expression, sessionImp.Factory);
            var translatorFactory = new ASTQueryTranslatorFactory();
            var translators = translatorFactory.CreateQueryTranslators(nhLinqExpression.Key, nhLinqExpression, null, false,
                                                                       sessionImp.EnabledFilters, sessionImp.Factory);

            var sql = translators.First().SQLString;
            var formamttedSql = FormatStyle.Basic.Formatter.Format(sql);
            int i = 0;
            var map = ExpressionParameterVisitor.Visit(queryable.Expression, sessionImp.Factory).ToArray();
            formamttedSql = Regex.Replace(formamttedSql, @"\?", m => map[i++].Key.ToString().Replace('"', '\''));

            return formamttedSql;
        }

        private static ISession GetSession<TResult>(NhQueryable<TResult> nhQueryable)
        {
            var queryProvider = nhQueryable.Provider;
            return ReflectionUtilities.GetValueForProperty<ISession>(queryProvider, "Session");
        }

        public DbConnection GetConnection()
        {
            return (DbConnection)GetSession(queryable).Connection;
        }
    }
}