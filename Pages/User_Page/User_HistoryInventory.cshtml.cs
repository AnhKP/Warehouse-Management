using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PRN221_Project_02.Models;

namespace PRN221_Project_02.Pages.User_Page
{
    public class User_HistoryInventoryModel : PageModel
    {
        public List<Inventory> list { get; set; } = new List<Inventory>();
        public string fullname { get; set; }
        public int userid { get; set; }
        public void OnGet(string id)
        {
            var userIdObj = HttpContext.Session.GetInt32("UserID");
            if (userIdObj.HasValue)
            {
                userid = userIdObj.Value;
                fullname = HttpContext.Session.GetString("Fullname");
                list = PRN221_Project_02Context.instance.Inventories.Include(x => x.Product).Include(x => x.Product.User).Where(x => x.Product.User.UserID == userid).ToList();
            }
        }
    }
}
