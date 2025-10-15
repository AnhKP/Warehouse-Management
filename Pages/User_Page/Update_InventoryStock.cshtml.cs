using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PRN221_Project_02.Models;

namespace PRN221_Project_02.Pages.User_Page
{
    public class Update_InventoryStockModel : PageModel
    {
        public Inventory inventory { get; set; } = new Inventory();

        public string userID { get; set; }
        public void OnGet(string invenid, string userId)
        {
            userID = userId;
            var invent = PRN221_Project_02Context.instance.Inventories
                                                          .Include(x => x.Product)
                                                          .Include(x => x.Product.User)
                                                          .FirstOrDefault(x => x.InventoryId == int.Parse(invenid) && x.Product.UserId == int.Parse(userId));
            inventory = invent;
        }
        public IActionResult OnPost(IFormCollection form)
        {
            try
            {
                string productName = form["productName"];
                int quantity = int.Parse(form["proQuanity"]);
                decimal temperature = decimal.Parse(form["proQuantity"]);
                DateTime expiryDate = DateTime.ParseExact(form["experiedDate"], "dd/MM/yyyy", null);
                string usename = form["user"];
                int uID = int.Parse(userID);
                var inventory = PRN221_Project_02Context.instance.Inventories
                    .Include(x => x.Product)
                    .FirstOrDefault(x => x.Product.ProductName == productName);

                if (inventory != null)
                {
                    inventory.Quantity = quantity;
                    inventory.Temperature = temperature;
                    inventory.ExpiryDate = expiryDate;
                    inventory.CheckedBy = uID;

                    PRN221_Project_02Context.instance.Inventories.Update(inventory);
                    PRN221_Project_02Context.instance.SaveChanges();
                }

                return RedirectToPage("/User_Page/User_HistoryInventory");
            }
            catch (Exception ex)
            {
              
                ModelState.AddModelError(string.Empty, "An error occurred while processing your request.");
                return Page();
            }
        }
    }
}
