using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PRN221_Project_02.Models;

namespace PRN211_Project_02.Pages.Manager_Page
{
    public class Manager_EmployeeModel : PageModel
    {
        public int userid { get; set; }
        public string fullname { get; set; }
        public string username { get; set; }
        public List<User> Users { get; set; }
        public void OnGet()
        {
            var userIdObj = HttpContext.Session.GetInt32("UserID");
            if (userIdObj.HasValue)
            {
                username = HttpContext.Session.GetString("Username");
                var manager = PRN221_Project_02Context.instance.Users.FirstOrDefault(x => x.UserID == userIdObj);
                userid = userIdObj.Value;
                fullname = HttpContext.Session.GetString("Fullname");

                var user = PRN221_Project_02Context.instance.Users
                .Include(x => x.Role)
                .Include(x => x.Status)
                .Where(x => x.WarehouseId.Equals(manager.WarehouseId) && x.RoleId == 3)
                .OrderByDescending(x => x.StatusId)
                .ThenBy(x => x.UserID)
                .ToList();
                Users = user;
            }
                if (Request.Query.ContainsKey("action") && Request.Query["action"] == "delete")
            {
                int userId = int.Parse(Request.Query["userId"]);
                var u = PRN221_Project_02Context.instance.Users.Find(userId);
                if (u != null)
                {
                    u.StatusId = 1;
                    PRN221_Project_02Context.instance.SaveChanges();
                }
            }
            
        }
        public IActionResult OnGetDeleteUser(int userId)
        {
            var user = PRN221_Project_02Context.instance.Users.Find(userId);
            if (user != null)
            {
                user.StatusId = 1;
                PRN221_Project_02Context.instance.SaveChanges();
            }
            return RedirectToPage("/Manager_Page/Manager_Employee");
        }
    }
}
