using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PRN221_Project_02.Models;

namespace PRN211_Project_02.Pages.Manager_Page
{
    public class Manager_ProductModel : PageModel
    {
        public int userid { get; set; }
        public string fullname { get; set; }
        public string username { get; set; }
        public List<Product> products { get; set; }

        public void OnGet()
        {
            var userIdObj = HttpContext.Session.GetInt32("UserID");

            if (userIdObj.HasValue)
            {
                username = HttpContext.Session.GetString("Username");
                fullname = HttpContext.Session.GetString("Fullname");

                var manager = PRN221_Project_02Context.instance.Users
                                .FirstOrDefault(x => x.UserID == userIdObj);

                if (manager != null)
                {
                    var managerWarehouseID = manager.WarehouseId;

                    if (managerWarehouseID != null)
                    {
                        products = PRN221_Project_02Context.instance.Products
                                    .Include(p => p.User)
                                    .Include(p => p.Status)
                                    .Include(p => p.Supplier)
                                    .Where(p => p.User.WarehouseId == managerWarehouseID  )
                                    .ToList();
                    }
                }
            }
        }

        public IActionResult OnGetDeleteProduct(int productId)
        {
            var pro = PRN221_Project_02Context.instance.Products.Find(productId);
            if (pro != null)
            {
                pro.StatusId = 1; // Cập nhật trạng thái là đã xóa hoặc trạng thái mong muốn
                PRN221_Project_02Context.instance.SaveChanges();
            }
            return RedirectToPage("/Manager_Page/Manager_Product");
        }

    }
}
