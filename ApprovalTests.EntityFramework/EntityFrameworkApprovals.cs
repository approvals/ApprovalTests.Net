﻿using System.Data.Objects;
using System.Linq;
using ApprovalTests.Persistence;

namespace ApprovalTests.EntityFramework
{
	public class EntityFrameworkApprovals
	{
		public static void Verify<T>(ObjectContext db, IQueryable<T> queryable)
		{
			DatabaseApprovals.Verify(new ObjectContextAdaptor<T>(db, queryable));
		}
	}
}
