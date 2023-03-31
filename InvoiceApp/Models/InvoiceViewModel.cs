
using InvoiceApp.Entities;

namespace InvoiceApp.Models
{
	public class InvoiceViewModel
	{
		public List<Invoice> Invoices { get; set; }
		public Invoice ActiveInvoice { get; set; }
		
		public Vendor ActiveVendor { get; set; }

		public List<PaymentTerm> PaymentTerms { get; set; }

		public List<InvoiceLineItem> InvoiceLineItems { get; set; }
		//lineitem
		public InvoiceLineItem ActiveLineItem { get; set; }
		public PaymentTerm ActiveTerm { get; set; }
		


	}
}
