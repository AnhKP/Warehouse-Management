using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PRN221_Project_02.Models;

namespace PRN211_Project_02.Pages.Manager_Page
{
    public class Manager_CategoryModel : PageModel
    {
        public int userid { get; set; }
        public string fullname { get; set; }
        public string username { get; set; }
        public List<Category> Categories { get; set; }
        public void OnGet()
        {

            var userIdObj = HttpContext.Session.GetInt32("UserID");
            if (userIdObj.HasValue)
            {
                username = HttpContext.Session.GetString("Username");
                var manager = PRN221_Project_02Context.instance.Users.FirstOrDefault(x => x.UserID == userIdObj);
                userid = userIdObj.Value;
                fullname = HttpContext.Session.GetString("Fullname");
                var cate = PRN221_Project_02Context.instance.Categories.Include(x=>x.Warehouse)
               .Where(x => x.WarehouseId == manager.WarehouseId)
               .ToList();
                Categories = cate;
            }
            if (Request.Query.ContainsKey("action") && Request.Query["action"] == "delete")
            {
                int categoryId = int.Parse(Request.Query["categoryId"]);
                var category = PRN221_Project_02Context.instance.Categories.Find(categoryId);
                if (category != null)
                {
                    category.StatusId = 1; 
                    PRN221_Project_02Context.instance.SaveChanges();
                }
            }

           

        }
        public IActionResult OnGetDeleteCategory(int categoryId)
        {
            var category = PRN221_Project_02Context.instance.Categories.Find(categoryId);
            if (category != null)
            {
                category.StatusId = 1; 
                PRN221_Project_02Context.instance.SaveChanges();
            }
            return RedirectToPage("/Manager_Page/Manager_Category"); 
        }
    }
}
