using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PRN221_Project_02.Models;

namespace PRN221_Project_02.Pages.Admin_Page
{
    public class Admin_HomePageModel : PageModel
    {
        public List<Category> Categories { get; set; }
        public List<User> Users { get; set; }
        public List<Product> listProducts { get; set; }
        [BindProperty]
        public int totalProduct { get; set; }
        [BindProperty]
        public int totalUser { get; set; }
        [BindProperty]
        public int totalCate { get; set; }
        public void OnGet()
        {
            var cate = PRN221_Project_02Context.instance.Categories.Include(x => x.Status).Include(x => x.Warehouse).Take(5).OrderBy(x => x.CategoryId).ToList();
            Categories = cate;
            var user = PRN221_Project_02Context.instance.Users
                .Include(x => x.Status)
                .Include(x => x.Role)
                .Take(5)
                .OrderBy(x => x.UserID)
                .ToList();
            Users = user;
            totalCate = PRN221_Project_02Context.instance.Categories.ToList().Count();
            totalProduct = PRN221_Project_02Context.instance.Products.ToList().Count();
            totalUser = PRN221_Project_02Context.instance.Users.Where(x => x.RoleId != 1 && x.StatusId != 1).Count();
            listProducts = PRN221_Project_02Context.instance.Products.Include(x => x.Category).Include(x => x.Status).Include(x => x.Supplier).Include(x => x.Category.Warehouse).Where(x => x.StatusId != 1 && x.StatusId != 3).OrderByDescending(p => p.CreatedAt).Take(5).ToList();
        }
    }
}
