using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PRN221_Project_02.Models;

namespace PRN221_Project_02.Pages.Admin_Page
{
    public class Admin_ExportListModel : PageModel
    {
        public List<StockExport> listEx {  get; set; }
        public void OnGet()
        {
            listEx = PRN221_Project_02Context.instance.StockExports.Include(x => x.User)
                                                                   .Include(x => x.Product)
                                                                   .Include(x => x.Agent).Include(x => x.Status).ToList();
        }
    }
}
