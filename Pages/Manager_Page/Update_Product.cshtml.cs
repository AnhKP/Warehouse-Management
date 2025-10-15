using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PRN221_Project_02.Models;
using System.Collections.Generic;

namespace PRN211_Project_02.Pages.Manager_Page
{
    public class Update_ProductModel : PageModel
    {
        public int userid { get; set; }
        public string fullname { get; set; }
        public string username { get; set; }
        [BindProperty]
        public Product product { get; set; }
        public List<Supplier> suppliers { get; set; }
        public void OnGet(int pid)
        {
            var userIdObj = HttpContext.Session.GetInt32("UserID");

            if (userIdObj.HasValue)
            {
                username = HttpContext.Session.GetString("Username");
                fullname = HttpContext.Session.GetString("Fullname");

                var manager = PRN221_Project_02Context.instance.Users.FirstOrDefault(x => x.UserID == userIdObj);
                product = PRN221_Project_02Context.instance.Products
                    .Include(x=>x.Category)
                    .Include(x=>x.Status)
                    .Include(x=>x.Supplier)
                    .FirstOrDefault(x => x.ProductId == pid);
                suppliers = PRN221_Project_02Context.instance.Suppliers.ToList();
            }
        }
        public Product FindProductByID(int id)
        {
            return PRN221_Project_02Context.instance.Products
                .FirstOrDefault(x => x.ProductId == id);
        }
        public IActionResult OnPost(int productId)
        {
            var pro = FindProductByID(productId);
            if (pro != null )
            {
                pro.ProductName= Request.Form["name"];
                pro.Quantity =int.Parse( Request.Form["quantity"]);
                pro.Price = decimal.Parse(Request.Form["price"]);

                if (DateTime.TryParse(Request.Form["expiry"], out DateTime expiry))
                {
                    pro.ExpiryDate = expiry;
                }

                if (DateTime.TryParse(Request.Form["create"], out DateTime create))
                {
                    pro.CreatedAt = create;
                }

                pro.UpdatedAt = DateTime.Now;
                pro.SupplierId = int.Parse(Request.Form["supplier"]);
                pro.StatusId= int.Parse(Request.Form["status"]);

                PRN221_Project_02Context.instance.SaveChanges();
                return RedirectToPage("/Manager_Page/Manager_Product");


            }
            return Page();
        }
    }
}
