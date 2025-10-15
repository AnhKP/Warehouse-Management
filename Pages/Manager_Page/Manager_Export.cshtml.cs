using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PRN221_Project_02.Models;
using PRN221_Project_02.Services;

namespace PRN221_Project_02.Pages.Manager_Page
{
    public class Manager_ExportModel : PageModel
    {
        public int userid { get; set; }
        public string fullname { get; set; }
        public string username { get; set; }
        public List<StockExport> stockExports { get; set; }

        public void OnGet()
        {
            var userIdObj = HttpContext.Session.GetInt32("UserID");

            if (userIdObj.HasValue)
            {
                username = HttpContext.Session.GetString("Username");
                fullname = HttpContext.Session.GetString("Fullname");

                var manager = PRN221_Project_02Context.instance.Users
                                .FirstOrDefault(x => x.UserID == userIdObj);

                stockExports = PRN221_Project_02Context.instance.StockExports
                    .Include(x=>x.Product)
                    .Include(x => x.Warehouse)
                    .Include(x => x.Agent)
                    .Include(x => x.Status)
                    .Include(x => x.User)
                    .Where(x => x.WarehouseId == manager.WarehouseId).ToList();
            }
        }

        
        
    }
}

