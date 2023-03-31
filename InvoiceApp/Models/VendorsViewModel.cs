using InvoiceApp.Entities;

namespace InvoiceApp.Models
{
    public class VendorsViewModel
    {

        public List<Vendor> ? Vendors { get; set; }
        public Vendor ? ActiveVendor { get; set; }
    }
}
