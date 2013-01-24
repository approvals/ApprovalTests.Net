using System.Collections.Generic;
using System.Linq;
using ApprovalUtilities.Persistence.EntityFramework;

namespace ApprovalTests.Tests.EntityFramework
{
	public abstract class MultiLoader<T> : EntityFrameworkLoader<T, IEnumerable<T>, ModelContainer>
	{
		public MultiLoader() : base(() => new ModelContainer())
		{
		}


		public override IEnumerable<T> Load()
		{
			return GetLinqStatement().ToArray();
		}
	}
}