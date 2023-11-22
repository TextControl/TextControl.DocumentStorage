namespace DocumentStorage.Models
{
	public class Invoice
	{
		public string InvoiceID { get; set; }
		public Customer Customer { get; set; }
		public string InvoiceNumber { get; set; }
		public string InvoiceDate { get; set; }

		public List<LineItem> LineItems { get; set; }
	}

	public class LineItem
	{
		public string LineItemID { get; set; }
		public string Description { get; set; }
		public string Quantity { get; set; }
		public string Price { get; set; }
		public string Tax { get; set; }
		public string Total { get; set; }
	}

	public class Customer
	{
		public string CustomerID { get; set; }
		public string Name { get; set; }
		public string Email { get; set; }
		public string Phone { get; set; }

		public string Address { get; set; }
		public string City { get; set; }
		public string State { get; set; }

		public string Zip { get; set; }
		public string Country { get; set; }
	}
}
