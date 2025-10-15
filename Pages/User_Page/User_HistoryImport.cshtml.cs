using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PRN221_Project_02.Models;

namespace PRN221_Project_02.Pages.User_Page
{
    public class User_HistoryImportModel : PageModel
    {
        public List<StockImport>  liststock { get; set; } = new List<StockImport>();
        public int userid { get; set; }
        [BindProperty(SupportsGet = true)]
        public int id { get; set; }
        public string fullname { get; set; }

        public void OnGet()
        {
            var userIdObj = HttpContext.Session.GetInt32("UserID");
            if (userIdObj.HasValue)
            {
                userid = userIdObj.Value;
                fullname = HttpContext.Session.GetString("Fullname");
                liststock = PRN221_Project_02Context.instance.StockImports.Include(x => x.Status)
                                                                          .Include(x => x.Product)
                                                                          .Include(x => x.Product.Category)
                                                                          .Include(x => x.Supplier)
                                                                          .Where(x => x.UserId == userIdObj).ToList();
            }
        }
    }
}
