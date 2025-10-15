using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PRN221_Project_02.Models;

namespace PRN221_Project_02.Pages.Manager_Page
{
    public class Manager_BatchModel : PageModel
    {
        public int userid { get; set; }
        public string fullname { get; set; }
        public string username { get; set; }

        public List<Batch> batches { get; set; }
        public void OnGet()
        {
            var userIdObj = HttpContext.Session.GetInt32("UserID");

            if (userIdObj.HasValue)
            {
                username = HttpContext.Session.GetString("Username");
                fullname = HttpContext.Session.GetString("Fullname");

                var manager = PRN221_Project_02Context.instance.Users
                                .FirstOrDefault(x => x.UserID == userIdObj);
                int? managerid = manager.WarehouseId;

                batches = PRN221_Project_02Context.instance.Batches
                    .Include(x=>x.Product)
                    .Include(x=>x.Supplier)
                    .Include(x=>x.Status)
                    .Include(x=>x.Warehouse)
                    .Where(x=>x.WarehouseId==managerid).ToList();
            }
        }
    }
}
