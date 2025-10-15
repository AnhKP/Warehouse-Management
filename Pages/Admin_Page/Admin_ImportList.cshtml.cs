using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PRN221_Project_02.Models;

namespace PRN221_Project_02.Pages.Admin_Page
{
    public class Admin_ImportListModel : PageModel
    {
        public List<StockImport> listIm {  get; set; } = new List<StockImport>();
        public void OnGet()
        {
            listIm = PRN221_Project_02Context.instance.StockImports.Include(x => x.Status)
                                                                   .Include(x => x.Product)
                                                                   .Include(x => x.Supplier)
                                                                   .Include(x => x.Warehouse).ToList();
        }
    }
}
