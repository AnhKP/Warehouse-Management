using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PRN221_Project_02.Models;

namespace PRN211_Project_02.Pages.Manager_Page
{
    public class Create_EmployeeModel : PageModel
    {
        public int userid { get; set; }
        public string fullname { get; set; }
        public string username { get; set; }
        public List<Role> Roles { get; set; }
        public User Manger { get; set; }
        public void OnGet()
        {
            var userIdObj = HttpContext.Session.GetInt32("UserID");
            if (userIdObj.HasValue)
            {
                username = HttpContext.Session.GetString("Username");
                var manager = PRN221_Project_02Context.instance.Users.FirstOrDefault(x => x.UserID == userIdObj);
                userid = userIdObj.Value;
                fullname = HttpContext.Session.GetString("Fullname");
                Manger = manager;
            }
                Roles = PRN221_Project_02Context.instance.Roles.ToList();
        }
        public IActionResult OnPost()
        {
            var newUser = new User
            {
                Username = Request.Form["username"],
                Email = Request.Form["email"],
                Password = Request.Form["password"],
                FullName = Request.Form["fullName"],
                PhoneNumber = Request.Form["phoneNumber"],
                Dob = DateTime.Parse(Request.Form["dob"]),
                RoleId = 3,
                WarehouseId = int.Parse(Request.Form["warehouseId"]),
                StatusId = int.Parse(Request.Form["status"])
            };
            PRN221_Project_02Context.instance.Users.Add(newUser);
            PRN221_Project_02Context.instance.SaveChanges();

            return RedirectToPage("Manager_Employee");
        }
    }
}
