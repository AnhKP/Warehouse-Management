using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PRN221_Project_02.Models;

namespace PRN221_Project_02.Pages.Admin_Page
{
    public class Admin_AddSupplierModel : PageModel
    {
        public void OnGet()
        {
        }
        
        public IActionResult OnPost()
        {
            string supplierName = Request.Form["supplierName"];
            string contactName = Request.Form["contactName"];
            string contactPhone = Request.Form["contactPhone"];
            string address = Request.Form["address"];
            string email = Request.Form["email"];
            string website = Request.Form["website"];

            // Create a new Supplier object and populate it with form data
            var newSupplier = new Supplier
            {
                SupplierName = supplierName,
                ContactName = contactName,
                ContactPhone = contactPhone,
                Address = address,
                Email = email,
                Website = website
            };

            // Add the new supplier to the database
            try
            {
                using (var context = new PRN221_Project_02Context())
                {
                    context.Suppliers.Add(newSupplier);
                    context.SaveChanges();
                }

                // Redirect or show success message after successful addition
                return RedirectToPage("/Admin_Page/Admin_SupplierList"); // Replace with your appropriate page
            }
            catch (Exception ex)
            {
                // Log the error and handle it appropriately
                ModelState.AddModelError(string.Empty, "An error occurred while adding the supplier. Please try again.");
                return Page();
            }
        }
    }
}
