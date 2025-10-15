using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PRN221_Project_02.Models;
using System.Security.Cryptography;

namespace PRN221_Project_02.Pages.Manager_Page
{
    public class Create_BatchModel : PageModel
    {
        public int userid { get; set; }
        public string fullname { get; set; }
        public string username { get; set; }
        public List<Supplier> suppliers { get; set; }
        public List<Product> products { get; set; }
        public Warehouse ware {  get; set; }
        public void OnGet()
        {
            var userIdObj = HttpContext.Session.GetInt32("UserID");

            if (userIdObj.HasValue)
            {
                username = HttpContext.Session.GetString("Username");
                fullname = HttpContext.Session.GetString("Fullname");

                var manager = PRN221_Project_02Context.instance.Users
                                .FirstOrDefault(x => x.UserID == userIdObj);
                ware = PRN221_Project_02Context.instance.Warehouses.FirstOrDefault(x => x.WarehouseId == manager.WarehouseId);
                if (manager != null)
                {
                    var managerWarehouseID = manager.WarehouseId;

                    if (managerWarehouseID != null)
                    {
                        products = PRN221_Project_02Context.instance.Products
                                    .Include(p => p.User)
                                    .Include(p => p.Status)
                                    .Include(p => p.Supplier)
                                    .Where(p => p.User.WarehouseId == managerWarehouseID)
                                    .ToList();
                    }
                }
                suppliers = PRN221_Project_02Context.instance.Suppliers.ToList();
            }
        }

        public  IActionResult OnPost()
        {
            
            
                // Create a new Batch instance and populate it with values from the form
                var batch = new Batch
            {
                BatchNumber = Request.Form["batchNumber"],
                ProductId = int.Parse(Request.Form["productName"]),
                ManufactureDate = DateTime.Parse(Request.Form["manufactureDate"]),
                ExpiryDate = DateTime.Parse(Request.Form["expiryDate"]),
                Quantity = int.Parse(Request.Form["quantity"]),
                WarehouseId = int.Parse(Request.Form["warehouse"]), // Assuming ware is set with the warehouse in OnGet
                SupplierId = int.Parse(Request.Form["supplier"]),
                UpdateDate = DateTime.Now,
                StatusId = int.Parse(Request.Form["status"]),
                RejectReason = ""
            };

            // Add the new batch to the database
            PRN221_Project_02Context.instance.Batches.Add(batch);

            // Save changes to the database
             PRN221_Project_02Context.instance.SaveChanges();

            // Redirect to the Manager_Batch page after saving
            return RedirectToPage("Manager_Batch");
        }
    }
}

