using System.ComponentModel.DataAnnotations;

namespace InvoiceApp.Entities
{
	public class InvoiceLineItem
	{
		public int InvoiceLineItemId { get; set; }

		public double Amount { get; set; }

		public string Description { get; set; }

		// Nav prop to invoice:
		public int? InvoiceId { get; set; }
		public Invoice? Invoice { get; set; }

	}
}
