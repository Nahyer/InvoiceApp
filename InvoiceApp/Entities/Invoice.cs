using System.ComponentModel.DataAnnotations;

namespace InvoiceApp.Entities
{
	public class Invoice
	{
		public int InvoiceId { get; set; }

		[DataType(DataType.Date)]
		public DateTime? InvoiceDate { get; set; }

		public DateTime? InvoiceDueDate
		{
			get
			{
				return InvoiceDate?.AddDays((int)Convert.ToDouble(PaymentTerms?.DueDays));
			}
		}

		public double? PaymentTotal { get; set; } = 0.0;

		public DateTime? PaymentDate { get; set; }

		// FK:
		public int PaymentTermsId { get; set; }

		// Nav to terms:
		public PaymentTerm PaymentTerms { get; set; }

		// FK:
		public int VendorId { get; set; }

		// Nav to vendor:
		public Vendor Vendor { get; set; }

		//Nav pop to line items:

		public ICollection<InvoiceLineItem> InvoiceLineItems { get; set; }
	}
}
