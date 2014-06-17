using System;
using System.Globalization;
using System.Threading;
using System.Web.UI;
using ApprovalTests.Asp;
using ApprovalUtilities.Utilities;

namespace Asp.Net.Demo.Orders
{
	public partial class InvoiceView : Page
	{
		private Invoice invoice;

		public Invoice Invoice
		{
			get { return invoice; }
			set
			{
			    invoice = value;
			    GridView1.DataSourceID = null;
			    GridView1.DataSource = invoice.GetLineItems();
			    GridView1.DataBind();
			    lblCompanyNameValue.Text = Invoice.CompanyName;
				lblOrderNumberValue.Text = Invoice.OrderNumber;
				lblOrderDateValue.Text = Invoice.OrderDate.ToShortDateString();
			}
		}


		protected void Page_Load(object sender, EventArgs e)
		{
			if (AspTestingUtils.DivertTestCall(this))
			{
				return;
			}
			LoadInvoiceById();
		}

		private void LoadInvoiceById()
		{
			Invoice = new InvoiceLoaderById(Page.ClientQueryString).Load();
		}

# if DEBUG
		public void TestSimpleInvoice()
		{
            CultureUtilities.ForceCulture();
			var invoice = new Invoice()
			              	{CompanyName = "Sammy Sweet Shop", OrderNumber = "123", OrderDate = new DateTime(1592, 3, 14)};
			invoice.AddLineItem("Candy Bars", 0.50, 200);
			invoice.AddLineItem("Fizzy Straws", 0.70, 100);
			Invoice = invoice;
		}
#endif

	}
}