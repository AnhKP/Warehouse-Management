using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PRN221_Project_02.Models;

namespace PRN211_Project.Pages.Admin_Page
{
    public class Create_CategoryModel : PageModel
    {
        public int userid { get; set; }
        public string fullname { get; set; }
        public string username { get; set; }

        public User Manager { get; set; }
        public void OnGet()
        {
            var userIdObj = HttpContext.Session.GetInt32("UserID");
            if (userIdObj.HasValue)
            {
                username = HttpContext.Session.GetString("Username");
                var manager = PRN221_Project_02Context.instance.Users.FirstOrDefault(x => x.UserID == userIdObj);
                userid = userIdObj.Value;
                fullname = HttpContext.Session.GetString("Fullname");
                Manager = manager;
            }
        }

        public IActionResult OnPost()
        {
            // Tạo mới Category
            var newCategory = new Category
            {
                CategoryName = Request.Form["categoryName"],
                StatusId = int.Parse(Request.Form["status"]),
                WarehouseId = int.Parse(Request.Form["warehouseId"])

            };

            PRN221_Project_02Context.instance.Categories.Add(newCategory);
            PRN221_Project_02Context.instance.SaveChanges(); 

            return RedirectToPage("Manager_Category"); // Chuyển hướng về trang danh sách danh mục
        }
    }
}

    

    


