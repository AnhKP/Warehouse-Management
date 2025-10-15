using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using PRN221_Project_02.Models;

namespace PRN211_Project_02.Pages.Manager_Page
{
    public class Update_CategoryModel : PageModel
    {
        public int userid { get; set; }
        public string fullname { get; set; }
        public string username { get; set; }
        public Category Category { get; set; }
        public int CategoryId { get; set; }


        public void OnGet(int cid)
        {
            var userIdObj = HttpContext.Session.GetInt32("UserID");
            if (userIdObj.HasValue)
            {
                username = HttpContext.Session.GetString("Username");
                var manager = PRN221_Project_02Context.instance.Users.FirstOrDefault(x => x.UserID == userIdObj);
                userid = userIdObj.Value;
                fullname = HttpContext.Session.GetString("Fullname");
            }
            CategoryId = cid;
            Category = FindCategoryByID(CategoryId);
        }

        public Category FindCategoryByID(int id)
        {
            return PRN221_Project_02Context.instance.Categories
                .FirstOrDefault(x => x.CategoryId == id);
        }

        public IActionResult OnPost(int categoryId)
        {
            CategoryId = categoryId;
            var cate = FindCategoryByID(CategoryId);
            if (cate != null)
            {
                cate.CategoryName = Request.Form["categoryName"];
                cate.StatusId = int.Parse(Request.Form["status"]);

                PRN221_Project_02Context.instance.SaveChanges();

                return RedirectToPage("/Manager_Page/Manager_Category");
            }
            Console.WriteLine("Category not found.heheh");
            return Page();
        }

    }
}
