using DocumentStorage.Models;
using Newtonsoft.Json;

namespace DocumentStorage.Helpers
{
	public static class InvoiceData
	{
		public static string GetDummyInvoiceData()
		{
			Invoice invoice = new Invoice
			{
				InvoiceID = "123456789",
				InvoiceNumber = "123456789",
				InvoiceDate = "2020-01-01",
				Customer = new Customer
				{
					CustomerID = "123456789",
					Name = "John Doe",
					Email = ""
				},
				LineItems = new List<LineItem>
				{
					new LineItem
					{
						LineItemID = "123456789",
						Description = "Item 1",
						Quantity = "1",
						Price = "100.00",
						Tax = "10.00",
						Total = "110.00"
					},
					new LineItem
					{
						LineItemID = "123456789",
						Description = "Item 2",
						Quantity = "1",
						Price = "100.00",
						Tax = "10.00",
						Total = "110.00"
					},
					new LineItem
					{
						LineItemID = "123456789",
						Description = "Item 3",
						Quantity = "1",
						Price = "100.00",
						Tax = "10.00",
						Total = "110.00"
					}
				}
			};

			List<Invoice> invoices = new List<Invoice>();
			invoices.Add(invoice);

			return JsonConvert.SerializeObject(invoices);
		}
	}
}
