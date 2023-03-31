using System.ComponentModel.DataAnnotations;

namespace InvoiceApp.Entities
{
	public class Vendor
	{

		public int VendorId { get; set; }

		[Required(ErrorMessage = "Please enter a name.")]
		public string Name { get; set; } = null!;

		[Required(ErrorMessage = "Please enter an Address1.")]
		public string? Address1 { get; set; }

		public string? Address2 { get; set; }

		[Required(ErrorMessage = "Please enter a City.")]
		public string? City { get; set; } = null!;

		[Required(ErrorMessage = "Please enter a Province Or State.")]
		public string? ProvinceOrState { get; set; } = null!;

		[Required(ErrorMessage = "Please enter a ZipOrPostalCode.")]
		public string? ZipOrPostalCode { get; set; } = null!;

		[Required(ErrorMessage = "Please enter a Phone.")]
		public string? VendorPhone { get; set; }

		public string? VendorContactLastName { get; set; }

		public string? VendorContactFirstName { get; set; }

		[EmailAddress]
		public string? VendorContactEmail { get; set; }

		public bool? IsDeleted { get; set; } = false;

		// Nav to all Invoices:
		public ICollection<Invoice>? Invoices { get; set; }
	}
}
