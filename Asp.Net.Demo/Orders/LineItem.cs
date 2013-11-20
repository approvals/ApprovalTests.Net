namespace Asp.Net.Demo.Orders
{
	public class LineItem
	{
		public string ItemName { get; set; }
		public double ItemPrice { get; set; }
		public double ItemQuantity { get; set; }

		public double SubTotal
		{
			get { return ItemPrice*ItemQuantity; }
		}
	}

}
