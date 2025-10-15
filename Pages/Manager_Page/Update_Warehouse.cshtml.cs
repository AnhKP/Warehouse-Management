using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PRN221_Project_02.Models;

namespace PRN221_Project_02.Pages.Manager_Page
{
    public class Update_WarehouseModel : PageModel
    {
        public int userid { get; set; }
        public string fullname { get; set; }
        public string username { get; set; }

        public Warehouse warehouse { get; set; }
        public int warehouseID {  get; set; }
        public void OnGet( int wid)
        {
            warehouseID = wid;
            warehouse= FindWarehouseByID(wid);
            var userIdObj = HttpContext.Session.GetInt32("UserID");
            if (userIdObj.HasValue)
            {
                username = HttpContext.Session.GetString("Username");
                var manager = PRN221_Project_02Context.instance.Users.FirstOrDefault(x => x.UserID == userIdObj);
                userid = userIdObj.Value;
                fullname = HttpContext.Session.GetString("Fullname");
                warehouse = PRN221_Project_02Context.instance.Warehouses
                    .FirstOrDefault(x => x.WarehouseId == wid);
            }
        }

        public Warehouse FindWarehouseByID(int id)
        {
            return PRN221_Project_02Context.instance.Warehouses
                .FirstOrDefault(x => x.WarehouseId == id);
        }
        public IActionResult OnPost(int warehouseid)
        {
            warehouseID = warehouseid;
            var u = FindWarehouseByID(warehouseID);

            if (u != null)
            {
                u.WarehouseName = Request.Form["name"];
                u.Location = Request.Form["location"];
                u.Capacity =int.Parse(Request.Form["capacity"]);
                u.TemperatureRange = Request.Form["temp"];

                
                if (int.TryParse(Request.Form["status"], out int statusId))
                {
                    u.StatusId = statusId;
                }

                PRN221_Project_02Context.instance.SaveChanges();
                return RedirectToPage("/Manager_Page/Manager_Warehouse");
            }

            Console.WriteLine("Cannot find warehouse");
            return Page();
        }

        
    }
}
