<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InvoiceView.aspx.cs" Inherits="Asp.Net.Demo.Orders.InvoiceView" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
		<title></title>
		<link rel="stylesheet" type="text/css" href="Styles\Site.css" />
</head>
<body>
		<form id="form1" runat="server">
		<div>
		
			<asp:Image ID="Image1" runat="server" Height="25%" 
				ImageUrl="~/images/acmelogo.gif" Width="25%" />
			<br />
			<asp:Label ID="lblCompanyName" runat="server" Text="Company Name : "></asp:Label>
			<asp:Label ID="lblCompanyNameValue" runat="server"></asp:Label>
			<br />
			<br />
			<asp:Label ID="lblOrderNumber" runat="server" Text="Order Number : "></asp:Label>
			<asp:Label ID="lblOrderNumberValue" runat="server"></asp:Label>
			<br />
			<br />
			<asp:Label ID="lblOrderDate" runat="server" Text="Order Date : "></asp:Label>
			<asp:Label ID="lblOrderDateValue" runat="server"></asp:Label>
			<br />
			<asp:ObjectDataSource ID="ObjectDataSource2" runat="server" 
				SelectMethod="GetLineItems" TypeName="Asp.Net.Demo.Orders.InvoiceView">
			</asp:ObjectDataSource>
			<br />
			<asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
				DataSourceID="ObjectDataSource2">
				<Columns>
					<asp:BoundField DataField="ItemName" HeaderText="ItemName" 
						SortExpression="ItemName" />
					<asp:BoundField DataField="ItemPrice" HeaderText="ItemPrice" 
						SortExpression="ItemPrice" DataFormatString="${0:0.00}" />
					<asp:BoundField DataField="ItemQuantity" HeaderText="ItemQuantity" 
						SortExpression="ItemQuantity" DataFormatString="${0:0.00}" />
					<asp:BoundField DataField="SubTotal" HeaderText="SubTotal" ReadOnly="True" 
						SortExpression="SubTotal" DataFormatString="${0:0.00}" />
				</Columns>
			</asp:GridView>
		
		</div>
		`</form>
</body>
</html>
