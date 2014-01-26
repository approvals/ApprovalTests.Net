using System.Collections.Generic;

namespace ApprovalTests.Reporters
{
	public static class CollectionUtilities
	{
		public static ICollection<T> AddAll<T>(this ICollection<T> collection, IEnumerable<T> additions)
		{
			foreach (var addition in additions)
			{
				collection.Add(addition);
			}
			return collection;
		}
	}
}