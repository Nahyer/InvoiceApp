using InvoiceApp.Entities;
using InvoiceApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InvoiceApp.Controllers
{
	public class VendorsController : Controller
	{
		[HttpGet("/Vendors")]
		public IActionResult GetAllVendors(int id)
		{
			ViewBag.Id = id;
			// get all vendors from the database
			var allVendors = _context.Vendors.ToList();
			return View("Vendors", allVendors);
		}

		[HttpGet("/Vendors/Request-Vendor")]
		public IActionResult GetNewVendor()
		{
			VendorsViewModel vendorsViewModel = new()
			{
				ActiveVendor = new Vendor()
			};
			return View("AddVendor", vendorsViewModel);
		}

		[HttpPost("/Vendors/Add-New-Vendor")]
		public IActionResult AddVendor(VendorsViewModel vendorsViewModel)
		{
			if (ModelState.IsValid)
			{
				_context.Vendors.Add(vendorsViewModel.ActiveVendor);
				_context.SaveChanges();
				return RedirectToAction("GetAllVendors");
			}
			//add vendor to the database
			return View("AddVendor", vendorsViewModel);
		}

		[HttpGet("/Vendors/{id}/Edit-request")]
		public IActionResult GetEditVendor(int id)
		{
			VendorsViewModel vendorsViewModel = new()
			{
				ActiveVendor = _context.Vendors.Find(id)
			};
			return View("EditVendor", vendorsViewModel);
		}

		[HttpPost("/Vendors/Edit-Vendor")]
		public IActionResult EditVendor(VendorsViewModel vendorsViewModel)
		{
			Console.WriteLine(vendorsViewModel.ActiveVendor.VendorId);
			if (ModelState.IsValid)
			{
				_context.Vendors.Update(vendorsViewModel.ActiveVendor);
				_context.SaveChanges();
				return RedirectToAction("GetAllVendors");
			}
			else
			{
				return View("EditVendor", vendorsViewModel);
			}				
		}

		[HttpGet("/Vendors/Get-Invoice")]
		public IActionResult GetVendorInvoice(int id)
		{
			// get all invoices from the database
			var vendor = _context.Vendors.Include(m => m.Invoices)
				.Where(m => m.VendorId == id)
				.FirstOrDefault();

			var invoice = _context.Invoices.Include(m => m.InvoiceLineItems)
				.Include(p => p.PaymentTerms)
				.Where(v => v.VendorId == id)
				.FirstOrDefault();

			var Terms = _context.PaymentTerms.ToList();

			if (invoice != null)
			{
				ViewBag.Total = (decimal)invoice.InvoiceLineItems.Sum(a => a.Amount);
				ViewBag.invoid = invoice.InvoiceId;

				InvoiceViewModel invoiceViewModel = new()
				{

					Invoices = vendor.Invoices.OrderBy(m => m.InvoiceId).ToList(),
					ActiveInvoice = invoice,
					PaymentTerms = Terms,
					ActiveVendor = invoice.Vendor,
					InvoiceLineItems = invoice.InvoiceLineItems.ToList(),
					ActiveTerm = invoice.PaymentTerms

				};
				return View("Invoices", invoiceViewModel);
			}
			else
			{
				InvoiceViewModel invoiceViewModel = new()
				{

					PaymentTerms = Terms,
					ActiveVendor = _context.Vendors.Find(id),
				};
				return View("Invoices", invoiceViewModel);
			}
	
		}

		[HttpGet("/Vendors/Get-Invoice1")]
		public IActionResult GetVendorInvoice1(int param1, int param2)
		{
		// get all invoices from the database
			var vendor = _context.Vendors.Include(m => m.Invoices)
				.Where(m => m.VendorId == param1)
				.FirstOrDefault();

			var Terms = _context.PaymentTerms.ToList();
			var invoice = _context.Invoices
			   .Include(m => m.InvoiceLineItems)
			   .Include(p => p.PaymentTerms)
			   .Where(i => i.InvoiceId == param2)
			   .FirstOrDefault();

			ViewBag.Total = (decimal)invoice.InvoiceLineItems.Sum(a => a.Amount);
			ViewBag.invoid = invoice.InvoiceId;
			
			if (invoice == null)
			{
				return NotFound();
			}

			InvoiceViewModel invoiceViewModel = new()
			{

				Invoices = vendor.Invoices.OrderBy(m => m.InvoiceId).ToList(),
				ActiveInvoice = invoice,
				PaymentTerms = Terms,
				ActiveVendor = invoice.Vendor,
				InvoiceLineItems = invoice.InvoiceLineItems.Where(i => i.InvoiceId == param2).ToList(),
				ActiveTerm = invoice.PaymentTerms

			};

			return View("Invoices", invoiceViewModel);
		}

		[HttpPost("/Invoice/Add-Invoice1")]
		public IActionResult AddInvoice( InvoiceViewModel invoiceViewModel, int id, string InvoDate)
		{
				invoiceViewModel.ActiveInvoice.InvoiceDate = DateTime.Parse(InvoDate);
				_context.Invoices.Add(invoiceViewModel.ActiveInvoice);
				_context.SaveChanges();
				return RedirectToAction("GetVendorInvoice", new { Id = id });
			
		}

		[HttpPost("/Invoice/Add-LineItem")]
		public IActionResult AddLineitem(InvoiceViewModel invoiceViewModel, int id)
		{
			_context.InvoiceLines.Add(invoiceViewModel.ActiveLineItem);
			_context.SaveChanges();

			return RedirectToAction("GetVendorInvoice", new { Id = id });

		}
        //delete vendor
        [HttpGet("/Vendors/Delete-Request")]
        public IActionResult GetDeleteVendor(int id)
        {
            var vendor = _context.Vendors.Find(id);
            return RedirectToAction("Delete", vendor);
        }

        public IActionResult Delete(Vendor vendor)
        {
            //set isdelted to true
            vendor.IsDeleted = true;
            _context.Vendors.Update(vendor);
            _context.SaveChanges();

            TempData["LastActionMessage"] = $"The Vendor \"{vendor.Name}\" was deleted.";
            var vId = vendor.VendorId;


            return RedirectToAction("GetAllVendors", new { Id = vId });
        }

        [HttpPost("/Vendors/Undo-Delete")]
		public IActionResult Undodelete(int id)
		{
            
            Console.WriteLine(id);
			var vendor = _context.Vendors.Find(id);
			vendor.IsDeleted = false;
			_context.Vendors.Update(vendor);
			_context.SaveChanges();

			return RedirectToAction("GetAllVendors");

		}

		public InvoiceDbContext _context;

		public VendorsController(InvoiceDbContext context)
		{
			_context = context;
		}
	}
}
