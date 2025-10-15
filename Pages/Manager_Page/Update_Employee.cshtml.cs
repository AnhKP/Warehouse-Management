using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PRN221_Project_02.Models;

namespace PRN211_Project_02.Pages.Manager_Page
{
    public class Update_EmployeeModel : PageModel
    {
        public int userid { get; set; }
        public string fullname { get; set; }
        public string username { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }
        public List<Role> Roles { get; set; }

        public List<Warehouse> Warehouse { get; set; }
        public void OnGet(int uid)
        {
            UserId = uid;
            User = FindUserByID(UserId);
           Warehouse = PRN221_Project_02Context.instance.Warehouses.ToList();

            var userIdObj = HttpContext.Session.GetInt32("UserID");
            if (userIdObj.HasValue)
            {
                username = HttpContext.Session.GetString("Username");
                var manager = PRN221_Project_02Context.instance.Users.FirstOrDefault(x => x.UserID == userIdObj);
                userid = userIdObj.Value;
                fullname = HttpContext.Session.GetString("Fullname");
            }
        }

            public User FindUserByID(int id)
        {
            return PRN221_Project_02Context.instance.Users
                .FirstOrDefault(x => x.UserID == id);
        }

        public IActionResult OnPost(int userId)
        {
            UserId = userId;
            var u = FindUserByID(UserId);

            if (u != null)
            {
                u.Username = Request.Form["username"];
                u.Email = Request.Form["email"];
                u.FullName = Request.Form["fullName"];
                u.PhoneNumber = Request.Form["phoneNumber"];

                if (DateTime.TryParse(Request.Form["dob"], out DateTime dob))
                {
                    u.Dob = dob;
                }

                if (int.TryParse(Request.Form["warehouse"], out int warehouseId))
                {
                    u.WarehouseId = warehouseId;
                }

                if (int.TryParse(Request.Form["status"], out int statusId))
                {
                    u.StatusId = statusId;
                }

                PRN221_Project_02Context.instance.SaveChanges();
                return RedirectToPage("/Manager_Page/Manager_Employee");
            }

            Console.WriteLine("Cannot find user");
            return Page();
        }


    }
}
