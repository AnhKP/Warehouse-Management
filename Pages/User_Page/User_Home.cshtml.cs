using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PRN221_Project_02.Models;

namespace PRN211_Project_02.Pages.User_Page
{
    public class User_HomeModel : PageModel
    {
        public List<Product> products {  get; set; }
        public Warehouse Warehouse { get; set; }
        
        public int? wareHouseID { get; set; }
        public string fullname { get; set; }
        public int userid { get; set; }
        [BindProperty(SupportsGet = true)]
        public int id { get; set; }
        PRN221_Project_02Context context = new PRN221_Project_02Context();
        public User_HomeModel(PRN221_Project_02Context _Project_02Context)
        {
            products = new List<Product>();
            context = _Project_02Context;
        }
        public void OnGet()
        {
            var userIdObj = HttpContext.Session.GetInt32("UserID");
            if (userIdObj.HasValue)
            {
                var pro = context.Products.Include(x => x.Category).Include(x => x.Status).ToList();
                var currentDate = DateTime.Now;
                foreach (var item in pro)
                {
                    if (item.ExpiryDate < currentDate && item.StatusId != 1)
                    {
                        item.StatusId = 3;
                    }
                }
                userid = userIdObj.Value;
                fullname = HttpContext.Session.GetString("Fullname");
                wareHouseID = context.Users.Include(x => x.Warehouse).Where(x => x.UserID == userid).Select(x => x.WarehouseId).FirstOrDefault();
                products = context.Products.Include(x => x.Category)
                                            .Include(x => x.Status)
                                            .Include(x => x.User)
                                            .Include(x => x.Category.Warehouse)
                                            .Include(x => x.Supplier)
                                            .Include(x => x.TemperatureLogs) 
                                            .Where(x => x.UserId == userid && x.Category.Warehouse.WarehouseId == wareHouseID && x.StatusId != 1).ToList();
            }
        }
        public IActionResult OnPost(IFormCollection form)
        {
            var userIdObj = HttpContext.Session.GetInt32("UserID");
            if (userIdObj.HasValue)
            {
                var pro = context.Products.Include(x => x.Category).Include(x => x.Status).ToList();
                var currentDate = DateTime.Now;
                foreach (var item in pro)
                {
                    if (item.ExpiryDate < currentDate && item.StatusId != 1)
                    {
                        item.StatusId = 3;
                    }
                }
            }
                var pID = form["id"];
            string message = string.Empty;
            var product = context.Products.FirstOrDefault(x => x.ProductId == int.Parse(pID) && x.StatusId == 3);
            if (product != null)
            {
                product.StatusId = 1;
                context.Products.Update(product);
                context.SaveChanges();
            }
            else
            {
                message = "You can not delete product in date. You can remove product outdate!";
            }
            ViewData["Message"] = message;       
            if (userIdObj.HasValue)
            {
                userid = userIdObj.Value;
                fullname = HttpContext.Session.GetString("Fullname");
                wareHouseID = context.Users.Include(x => x.Warehouse).Where(x => x.UserID == userid).Select(x => x.WarehouseId).FirstOrDefault();
                products = context.Products.Include(x => x.Category)
                                            .Include(x => x.Status)
                                            .Include(x => x.User)
                                            .Include(x => x.Category.Warehouse)
                                            .Include(x => x.Supplier)
                                            .Include(x => x.TemperatureLogs)
                                            .Where(x => x.UserId == userid && x.Category.Warehouse.WarehouseId == wareHouseID && x.StatusId != 1).ToList();
            }
            return Page();
        }
    }
}
