using System;
using System.Collections.Generic;

namespace ApprovalUtilities.Utilities
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
		public static void ForEach<T>(this IEnumerable<T> ie, Action<T> action)
		{
			foreach (var i in ie)
			{
				action(i);
			}
		}
	}
}