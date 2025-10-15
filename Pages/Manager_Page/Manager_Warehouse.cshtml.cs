using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PRN221_Project_02.Models;

namespace PRN211_Project_02.Pages.Manager_Page
{
    public class Manager_WarehouseModel : PageModel
    {
        public int userid { get; set; }
        public string fullname { get; set; }
        public string username { get; set; }

        public Warehouse warehouse { get; set; }

        public List<Warehouse> listWarehouse { get; set; }
        public void OnGet()
        {
            var userIdObj = HttpContext.Session.GetInt32("UserID");
            if (userIdObj.HasValue)
            {
                username = HttpContext.Session.GetString("Username");
                var manager = PRN221_Project_02Context.instance.Users.FirstOrDefault(x => x.UserID == userIdObj);
                userid = userIdObj.Value;
                fullname = HttpContext.Session.GetString("Fullname");
                warehouse = PRN221_Project_02Context.instance.Warehouses
                    .FirstOrDefault(x => x.WarehouseId == manager.WarehouseId);
                listWarehouse = PRN221_Project_02Context.instance.Warehouses
                    .Include(x=>x.Status)
                    .Where(x=>x.WarehouseId == manager.WarehouseId).ToList();
            }
        }
    }
}
