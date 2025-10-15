using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PRN221_Project_02.Models;

namespace PRN221_Project_02.Pages.User_Page
{
    public class User_ExportProductModel : PageModel
    {
        public Product product { get; set; }
        [BindProperty(SupportsGet = true)]
        public int userid { get; set; }
        [BindProperty(SupportsGet = true)]
        public int id { get; set; }

        public Agent Agent { get; set; }
        public string fullname { get; set; }

        public void OnGet(string id)
        {
            var userIdObj = HttpContext.Session.GetInt32("UserID");
            if (userIdObj.HasValue)
            {
                userid = userIdObj.Value;
                fullname = HttpContext.Session.GetString("Fullname");
            }
                int ID = int.Parse(id);

            product = PRN221_Project_02Context.instance.Products.Include(x => x.Category).Include(x => x.Supplier).FirstOrDefault(x => x.ProductId == ID && x.UserId == userid);
        }
        public IActionResult OnPost(IFormCollection form)
        {
            int userID = Convert.ToInt32(form["userID"]);
            int productID = Convert.ToInt32(form["productid"]);
            string fullName = form["fullName"];
            string phone = form["phone"];
            string email = form["email"];
            string address = form["address"];
            int productQuantity = Convert.ToInt32(form["productQuantity"]);
            DateTime movementDate = DateTime.Parse(form["movementDate"]);
            var product = PRN221_Project_02Context.instance.Products.Include(x => x.Category).Include(x => x.Category.Warehouse).FirstOrDefault(x => x.ProductId ==  productID);
            var agency = PRN221_Project_02Context.instance.Agents.FirstOrDefault(x => x.Email.ToUpper().Equals(email.ToUpper()));
            if(agency == null)
            {
                agency = new Agent
                {
                    Email = email,
                    Address = address,
                    AgentName = fullName,
                    ContactNumber = phone
                };
                PRN221_Project_02Context.instance.Agents.Add(agency);
                PRN221_Project_02Context.instance.SaveChanges();
            }
            if (product != null)
            {
                if (product.Quantity > productQuantity)
                {
                    product.Quantity = product.Quantity - productQuantity;
                    product.UpdatedAt = DateTime.Now;

                }
                else if (product.Quantity == productQuantity)
                {
                    product.Quantity = 0;
                    product.StatusId = 18;
                    product.UpdatedAt = DateTime.Now;
                }
                PRN221_Project_02Context.instance.Products.Update(product);
                PRN221_Project_02Context.instance.SaveChanges();
            }
            StockExport export = new StockExport();
            export.AgentId = agency.AgentId;
            export.ProductId = productID;
            export.WarehouseId = product.Category.WarehouseId;
            export.UserId = userID;
            export.Quantity = productQuantity;
            export.MovementDate = movementDate;
            export.StatusId = 5;
            PRN221_Project_02Context.instance.StockExports.Add(export);
            PRN221_Project_02Context.instance.SaveChanges();
            return RedirectToPage("/User_Page/User_HistoryExport");
        }
    }
}
