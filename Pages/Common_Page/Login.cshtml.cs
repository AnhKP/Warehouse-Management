using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PRN221_Project_02.Models;

namespace PRN211_Project.Pages.Common_Page
{
    public class LoginModel : PageModel
    {
        public string mess { get; set; }
        PRN221_Project_02Context context= new PRN221_Project_02Context();
        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            string username = Request.Form["username"];
            string password = Request.Form["password"];
            var user = context.Users.FirstOrDefault(u => (u.Username.Equals(username) || u.Email.Equals(username)) && u.Password.Equals(password));

            if (user != null)
            {
                HttpContext.Session.SetString("Username", user.Username);
                HttpContext.Session.SetString("Fullname", user.FullName);
                HttpContext.Session.SetInt32("UserID", user.UserID);

                if (user.RoleId == 1)
                {
                    return RedirectToPage("/Admin_Page/Admin_HomePage");
                }
                else
                {
                    if (user.RoleId == 2)
                    {
                        return RedirectToPage("/Manager_Page/Manager_Home");
                    }
                    else
                    {
                            return RedirectToPage("/User_Page/User_Home");
                    }
                }
                    
            }
            else
            {
                mess = "Invalid username or password.";
                return Page();
            }
        }

    }
}
