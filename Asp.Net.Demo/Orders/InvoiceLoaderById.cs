using System;
using ApprovalUtilities.Persistence;

namespace Asp.Net.Demo.Orders
{
	public class InvoiceLoaderById: ILoader<Invoice>
	{
		private readonly string id;

		public InvoiceLoaderById(string id)
		{
			this.id = id;
		}

		public Invoice Load()
		{
			throw new Exception("Database is not Connected");
		}
	}
}