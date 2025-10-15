using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PRN221_Project_02.Models;

namespace PRN221_Project_02.Pages.Admin_Page
{
    public class Admin_InventoryListModel : PageModel
    {
        public List<Inventory> listInven { get; set; }
        public void OnGet()
        {
            listInven = PRN221_Project_02Context.instance.Inventories.Include(x => x.Product).Include(x => x.Product.User).Include(x => x.Warehouse).Where(x => x.CheckedBy == x.Product.User.UserID).ToList();

        }
    }
}
