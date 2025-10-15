using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using PRN221_Project_02.Models;
using System.Text.RegularExpressions;

namespace PRN211_Project.Pages.Common_Page
{
    public class ChangeNewPasswordModel : PageModel
    {
        public User user {  get; set; }
        public IActionResult OnGet()
        {
            return Page();
        }
        public IActionResult OnPost(IFormCollection f)
        {
            string message = string.Empty;
            string email = f["email"];
            string otpcode = f["codepwd"];
            string password = f["password"];
            string cfpassword = f["cfpassword"];
            user = PRN221_Project_02Context.instance.Users.FirstOrDefault(x => x.Email.Equals(email));
            if(user != null)
            {
                if (checkPassword(password) == true)
                {
                    if (cfpassword.Equals(password))
                    {
                        if (otpcode.Equals(user.CodePwd))
                        {
                            user.Password = password;
                            PRN221_Project_02Context.instance.Users.Update(user);
                            PRN221_Project_02Context.instance.SaveChanges();
                            message = "Change your password successfully!";
                        }
                        else
                        {
                            message = "OTP not correct!";
                        }
                    }
                    else
                    {
                        message = "Confirm password is not same password!";
                    }
                }
                else
                {
                    message = "Your password must contain one capital letter, one special character, and be at least 8 characters!";
                }
            }
            else
            {
                message = "Your email is not exist!";
            }
            ViewData["Message"] = message;
            return Page();
        }

        public bool checkPassword(string pass)
        {
            return pass.Length >= 8 && !pass.Contains(" ") && Regex.IsMatch(pass, ".*[A-Z].*") && Regex.IsMatch(pass, ".*[^A-Za-z0-9].*");
        }
    }
}
