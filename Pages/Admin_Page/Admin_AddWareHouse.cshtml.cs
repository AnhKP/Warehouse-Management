using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PRN221_Project_02.Models;

namespace PRN221_Project_02.Pages.Admin_Page
{
    public class Admin_WareHouseListModel : PageModel
    {
        public List<Category> categories { get; set; }

        public List<Status> status { get; set; }

        public List<User> users { get; set; }
        public void OnGet()
        {
            var cate = PRN221_Project_02Context.instance.Categories.Include(x => x.Status).Include(x => x.Warehouse).Take(5).OrderBy(x => x.CategoryId).ToList();
            categories = cate;
            status = PRN221_Project_02Context.instance.Statuses.OrderBy(x => x.StatusId).ToList();
            users = PRN221_Project_02Context.instance.Users.Where(x => x.RoleId == 2).ToList();
        }
        public IActionResult OnPost()
        {
            var warehouseName = Request.Form["warehouseName"];
            var location = Request.Form["location"];
            var capacity = Request.Form["capacity"];
            var temperatureRange = Request.Form["temperatureRange"];
            var statusID = Request.Form["status"];
            var userID = Request.Form["username"];

            var warehouse = new Warehouse
            {
                WarehouseName = warehouseName,
                Location = location,
                Capacity = int.Parse(capacity),
                TemperatureRange = temperatureRange,
                StatusId = int.Parse(statusID),
                UserId = int.Parse(userID)
            };

            PRN221_Project_02Context.instance.Warehouses.Add(warehouse);
            PRN221_Project_02Context.instance.SaveChanges();
            return RedirectToPage("/Admin_Page/Admin_WareHouseList");
        }
    }
}
