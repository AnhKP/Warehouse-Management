using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PRN221_Project_02.Models;

namespace PRN221_Project_02.Pages.Admin_Page
{
    public class Admin_ProductListModel : PageModel
    {
        public List<Product> products { get; set; }
        public void OnGet()
        {
            products = PRN221_Project_02Context.instance.Products.Include(x => x.Category)
                            .Include(x => x.Status)
                            .Include(x => x.User)
                            .Include(x => x.Category.Warehouse)
                            .Include(x => x.Supplier)
                            .Include(x => x.TemperatureLogs).ToList();
        }
    }
}
