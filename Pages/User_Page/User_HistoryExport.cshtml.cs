using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PRN221_Project_02.Models;

namespace PRN221_Project_02.Pages.User_Page
{
    public class User_HistoryExportModel : PageModel
    {
        public List<StockExport> listExport {  get; set; } = new List<StockExport>();
        public int userid { get; set; }
        [BindProperty(SupportsGet = true)]
        public int id { get; set; }

        public decimal totalprice { get; set; } = 0;
        public string fullname { get; set; }

        public void OnGet()
        {
            var userIdObj = HttpContext.Session.GetInt32("UserID");
            if (userIdObj.HasValue)
            {
                userid = userIdObj.Value;
                fullname = HttpContext.Session.GetString("Fullname");
                listExport = PRN221_Project_02Context.instance.StockExports.Include(x => x.Status)
                                                                          .Include(x => x.Product)
                                                                          .Include(x => x.Product.Category)
                                                                          .Include(x => x.Agent)
                                                                          .Where(x => x.UserId == userIdObj).ToList();
            }
            foreach (var item in listExport)
            {
                totalprice += ((decimal)item.Quantity * (decimal)item.Product.Price);
            }
        }

        public IActionResult OnPost(IFormCollection form)
        {
            int exportID = int.Parse(form["exportid"]);
            int userID = int.Parse(form["user_id"]);
            var exp = PRN221_Project_02Context.instance.StockExports.FirstOrDefault(x => x.UserId == userID && x.ExportId == exportID);
            if (exp != null)
            {
                exp.StatusId = 1;
                PRN221_Project_02Context.instance.StockExports.Update(exp);
                PRN221_Project_02Context.instance.SaveChanges();
            }
            var userIdObj = HttpContext.Session.GetInt32("UserID");
            if (userIdObj.HasValue)
            {
                userid = userIdObj.Value;
                fullname = HttpContext.Session.GetString("Fullname");
                listExport = PRN221_Project_02Context.instance.StockExports.Include(x => x.Product)
                                                                          .Include(x => x.Product.Category)
                                                                          .Include(x => x.Agent)
                                                                          .Include(x => x.Status)
                                                                          .Where(x => x.UserId == userIdObj).ToList();
            }
            foreach (var item in listExport)
            {
                totalprice += ((decimal)item.Quantity * (decimal)item.Product.Price);
            }
            return Page();
        }
    }
}
