using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PRN221_Project_02.Models;
using System.Collections.Generic;

namespace PRN211_Project_02.Pages.Manager_Page
{
    public class Create_ProductModel : PageModel
    {
        public int userid { get; set; }
        public string fullname { get; set; }
        public string username { get; set; }

        public List<Category> categories { get; set; }
        public List<Supplier> suppliers { get; set; }
        public void OnGet()
        {
            
            var userIdObj = HttpContext.Session.GetInt32("UserID");

            if (userIdObj.HasValue)
            {
                username = HttpContext.Session.GetString("Username");
                fullname = HttpContext.Session.GetString("Fullname");

                // Lấy thông tin Manager dựa trên UserID từ session
                var manager = PRN221_Project_02Context.instance.Users
                                .FirstOrDefault(x => x.UserID == userIdObj);
                categories = PRN221_Project_02Context.instance.Categories
                    .Where(x => x.StatusId != 1 && x.WarehouseId == manager.WarehouseId).ToList();
                suppliers = PRN221_Project_02Context.instance.Suppliers.ToList();

            }
        }

        public IActionResult OnPost()
        {
            var id = HttpContext.Session.GetInt32("UserID");
            // Retrieve form values from Request.Form
            string productName = Request.Form["name"];
            int categoryId = int.Parse(Request.Form["category"]);
            int supplierId = int.Parse(Request.Form["supplier"]);
            int quantity = int.Parse(Request.Form["quantity"]);
            decimal price = decimal.Parse(Request.Form["price"]);
            DateTime expiryDate = DateTime.Parse(Request.Form["expiry"]);
            DateTime createDate = DateTime.Parse(Request.Form["create"]);
            DateTime updateDate = DateTime.Parse(Request.Form["update"]);
            int status = int.Parse(Request.Form["status"]);
            int userId = id.Value;

            var newProduct = new Product
            {
                ProductName = productName,
                CategoryId = categoryId,
                SupplierId = supplierId,
                Quantity = quantity,
                Price = price,
                ExpiryDate = expiryDate,
                CreatedAt = createDate,
                UpdatedAt = updateDate,
                StatusId = status,
                UserId = userId
            };

            PRN221_Project_02Context.instance.Products.Add(newProduct);
            PRN221_Project_02Context.instance.SaveChanges();

            return RedirectToPage("Manager_Product"); // Redirect to a page of your choice
        }


    }
}
