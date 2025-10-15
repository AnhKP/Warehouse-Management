using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PRN221_Project_02.Models;

namespace PRN221_Project_02.Pages.Admin_Page
{
    public class Admin_SupplierListModel : PageModel
    {
        public List<Supplier> list {  get; set; }
        public void OnGet()
        {
            list = PRN221_Project_02Context.instance.Suppliers.ToList();
        }
    }
}
