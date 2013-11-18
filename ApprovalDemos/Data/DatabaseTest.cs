using System.Collections.Generic;
using System.Linq;
using ApprovalTests;
using ApprovalUtilities.Persistence;
using ApprovalUtilities.Utilities;
using NUnit.Framework;

namespace ApprovalDemos.Data
{
	[TestFixture]
	public class DatabaseTest
	{
		private void Approve<T>(IAdoLoader<T> loader)
		{
			Approvals.Verify(new AdoLoaderWrapper<T>(loader));
		}

		public string DoAuthorLookup()
		{
			return AuthorLookup(new AuthorLoader());
		}

		private string AuthorLookup(ILoader<IEnumerable<Author>> authorLoader)
		{
			return authorLoader.Load().First().FirstName;
		}

		[Test]
		public void TestAuthorLookup()
		{
			var loader = new MockLoader<IEnumerable<Author>>(
				new[]
					{
						new Author {FirstName = "Tom", LastName = "Jones"},
						new Author {FirstName = "Mark", LastName = "Twian"}
					});

			Assert.AreEqual("hiya", AuthorLookup(loader));
		}

		[Test]
		public void TestAuthorNameLoader()
		{
			Approvals.Verify(new AuthorNameLoader());
		}

		[Test]
		public void TestQuery()
		{
			Approve(new AuthorLoader());
		}

		[Test]
		public void TestLambdas()
		{
			string tom = "ApprovalDemos.Data.Author[].Where(a => (a.FirstName = \"Tom\"))";
			string mark = "ApprovalDemos.Data.Author[].Where(a => (a.FirstName = \"Mark\"))";

			Assert.AreEqual("author[0] = Tom Jones", new AuthorNameLoader().ExecuteQuery(tom));
			Assert.AreEqual("author[0] = Mark Twain", new AuthorNameLoader().ExecuteQuery(mark));
		}
	}

	public class AuthorNameLoader : IExecutableQuery
	{
		public string GetQuery()
		{
			return GetQueryExtracted().Expression.ToString();
		}

		public string ExecuteQuery(string query)
		{
			return StringUtils.Write(GetQueryExtracted(), "author");
		}


		private static Author[] GetAuthors()
		{
			var tom = new Author {FirstName = "Tom", LastName = "Jones"};
			var mark = new Author {FirstName = "Mark", LastName = "Twian"};

			return new[] {tom, mark};
		}

		private static IQueryable<Author> GetQueryExtracted()
		{
			IQueryable<Author> query =
				from a in GetAuthors().AsQueryable()
				where a.FirstName == "Tom"
				select a;

			return query;
		}
	}
}