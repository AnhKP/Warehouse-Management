using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PRN221_Project_02.Models;

namespace PRN221_Project_02.Pages.Admin_Page
{
    public class Admin_CategoryListModel : PageModel
    {
        public List<Category> Categories { get; set; }
        public void OnGet()
        {
            Categories = PRN221_Project_02Context.instance.Categories.Include(x => x.Warehouse).Include(x => x.Status).ToList();
        }
    }
}
