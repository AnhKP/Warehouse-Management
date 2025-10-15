using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PRN221_Project_02.Models;

namespace PRN211_Project_02.Pages.Common_Page
{
    public class SignUpModel : PageModel
    {
        PRN221_Project_02Context context = new PRN221_Project_02Context();

        public string mess { get; set; }
        public void OnGet()
        {
        }
        public IActionResult OnPost()
        {
            string username = Request.Form["username"];
            string email = Request.Form["email"];
            string password = Request.Form["password"];
            string repeat_password = Request.Form["repeat_password"];
            string fullname = Request.Form["fullname"];
            string phone = Request.Form["phone"];
            string dob = Request.Form["dob"];
            int roleID = 2;
            int statusID = 2;
            var existingUsername = context.Users.FirstOrDefault(x => x.Username.Equals(username));
            var existingEmail = context.Users.FirstOrDefault(x => x.Email.Equals(email));

            if (existingUsername != null)
            {
                mess = "This username is already taken.";
            }
            else if (existingEmail != null)
            {
                mess = "This email is already registered.";
            }
            else if (!repeat_password.Equals(password))
            {
                mess = "Your repeat password does not match your password.";
            }
            else
            {
               // var u = new User(username, password, email, fullname, phone, DateTime.Parse(dob), roleID, statusID);
               // context.Add(u);
                context.SaveChanges();
            }



            return RedirectToPage("/Common_Page/Login");
        }
    }
}
