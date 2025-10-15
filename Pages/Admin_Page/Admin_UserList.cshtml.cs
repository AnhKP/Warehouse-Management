using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PRN221_Project_02.Models;

namespace PRN221_Project_02.Pages.Admin_Page
{
    public class Admin_UserListModel : PageModel
    {
        public List<User> Users { get; set; }

        public List<Role>   roles { get; set; }
        public void OnGet(string uid, string role)
        {
            if (uid != null && role != null)
            {
                int roleID = int.Parse(role);
                int UID = int.Parse(uid);
                var uObject = PRN221_Project_02Context.instance.Users.FirstOrDefault(x => x.UserID == UID);
                if (uObject != null)
                {
                    uObject.RoleId = roleID;
                    PRN221_Project_02Context.instance.Update(uObject);
                    PRN221_Project_02Context.instance.SaveChanges();
                }
            }
            var user = PRN221_Project_02Context.instance.Users.Include(x => x.Role).Include(x => x.Status).Where(x => x.RoleId != 1).ToList();
            Users = user;
            roles = PRN221_Project_02Context.instance.Roles.Where(x => x.RoleId != 1).ToList();
        }
        public IActionResult OnGetDeleteUser(int userId)
        {
            var user = PRN221_Project_02Context.instance.Users.Find(userId);
            if (user != null)
            {
                user.StatusId = 1;
                PRN221_Project_02Context.instance.SaveChanges();
            }
            return RedirectToPage("/Admin_Page/Admin_UserList");
        }
    }
}
