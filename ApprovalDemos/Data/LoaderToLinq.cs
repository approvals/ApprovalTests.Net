using System.Linq;
using NUnit.Framework;
namespace ApprovalDemos.Data.Sample
{
	[TestFixture]
	public class LoaderToLinqTests
	{
		[Test]
		public void TestLambdas()
		{
			string tom = "ApprovalDemos.Data.Sample.Author[].Where(a => (a.FirstName = \"Tom\"))";
			string mark = "ApprovalDemos.Data.Sample.Author[].Where(a => (a.FirstName = \"Mark\"))";

			Assert.AreEqual( "Tom Jones", new AuthorNameLoader().ExecuteQuery( tom ) );
			Assert.AreEqual( "Mark Twain", new AuthorNameLoader().ExecuteQuery( mark ) );
		}
	}

	public class Author
	{
		public string ID { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }

		public override string ToString()
		{
			return string.Format( "{0} {1}", FirstName, LastName );
		}
	}

	public class AuthorNameLoader 
	{
		public string ExecuteQuery(string query)
		{
			// Question is: How do I create this linq statement
			// not directly from c# as in GenerateLinqQuery()
			// but rather by parsing the string 'query' which can look like
			//
			// ApprovalDemos.Data.Sample.Author[].Where(a => (a.FirstName = \"Mark\"))
			//
			IQueryable<Author> linqQuery = GenerateLinqQuery();

			return linqQuery.First().ToString();
		}

		private static Author[] GetAuthors()
		{
			var tom = new Author { FirstName = "Tom", LastName = "Jones" };
			var mark = new Author { FirstName = "Mark", LastName = "Twian" };

			return new[] { tom, mark };
		}

		private static IQueryable<Author> GenerateLinqQuery()
		{
			IQueryable<Author> query =
				from a in GetAuthors().AsQueryable()
				where a.FirstName == "Tom"
				select a;

			return query;
		}
	}
}