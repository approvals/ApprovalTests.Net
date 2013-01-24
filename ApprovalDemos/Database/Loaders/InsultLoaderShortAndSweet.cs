using ApprovalUtilities.Persistence.Database;
using ApprovalUtilities.Utilities;

namespace ApprovalDemos.Database.Loaders
{
	public class InsultLoaderShortAndSweet : SqlLoader<Insults[]>
	{
		private readonly int maxWords;
		private readonly int minBurnLevel;

		public InsultLoaderShortAndSweet(int maxWords, int minBurnLevel, string connectionString) : base(connectionString)
		{
			this.maxWords = maxWords;
			this.minBurnLevel = minBurnLevel;
		}

		public override Insults[] Load()
		{
			var insults = DatabaseUtils.Query(GetQuery(), ConnectionString, r => new Insults() {Insult = r.GetString(0), BurnLevel = r.GetInt32(1)});
			return insults.ToArray();
		}

		public override string GetQuery()
		{
			var sql =
				@"
SELECT i.Insult, i.BurnLevel
		FROM Insults i
		WHERE {0} <= i.BurnLevel 
				AND i.NumberOfWords <= {1}"
					.FormatWith(minBurnLevel, maxWords);
			return sql;
		}
	}
	public class Insults
	{
		public string Insult { get; set; }

		public int BurnLevel { get; set; }
	}
}