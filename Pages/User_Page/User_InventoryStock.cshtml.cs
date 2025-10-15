using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PRN221_Project_02.Models;

namespace PRN221_Project_02.Pages.User_Page
{
    public class User_InventoryStockModel : PageModel
    {
        public List<Inventory> Inventories { get; set; }// List to hold the multiple products and their data

        public List<Product> Products { get; set; }

        public List<Status> Statuss { get; set; }
        public string fullname { get; set; }
        public int userid { get; set; }
        [BindProperty(SupportsGet = true)]
        public int id { get; set; }
        public void OnGet()
        {
            var userIdObj = HttpContext.Session.GetInt32("UserID");
            if (userIdObj.HasValue)
            {
                userid = userIdObj.Value;
                fullname = HttpContext.Session.GetString("Fullname");
                Products = PRN221_Project_02Context.instance.Products.Include(x => x.Status).Where(x=> x.UserId == userid && x.StatusId != 1).ToList();
                Statuss =  PRN221_Project_02Context.instance.Statuses.ToList();
            }
        }
        public IActionResult OnPost(IFormCollection form)
        {
            Inventories = new List<Inventory>();

            var userIdObj = HttpContext.Session.GetInt32("UserID");
            if (userIdObj.HasValue)
            {
                userid = userIdObj.Value;
            }
            Products = PRN221_Project_02Context.instance.Products.Include(x => x.Status).Where(x => x.UserId == userid && x.StatusId != 1).ToList();
            for (int i = 0; i < Products.Count; i++)
            {
                var productIdList = form["productId"].ToString().Split(',');
                var proNameList = form["productName"].ToString().Split(',');
                var quantityList = form["quantity"].ToString().Split(',');
                var tempList = form["temperature"].ToString().Split(',');
                var expList = form["ExperiedDate"].ToString().Split(',');
                int proID = Convert.ToInt32(productIdList[i].Trim());
                var proName = proNameList[i].Trim();
                int quantity = Convert.ToInt32(quantityList[i].Trim());
                decimal temp = decimal.Parse(tempList[i].Trim());
                DateTime exp = DateTime.Parse(expList[i].Trim());

                var checkAt = DateTime.Now;
                string checkBy = userid.ToString();

                var product = PRN221_Project_02Context.instance.Products
                                .Include(x => x.Category)
                                .Include(x => x.Category.Warehouse)
                                .FirstOrDefault(x => x.ProductId == proID);
                var tempObject = PRN221_Project_02Context.instance.TemperatureLogs.FirstOrDefault(x => x.ProductId == proID);
                if(tempObject != null)
                {
                    tempObject.RecordedTemperature = temp;
                    tempObject.RecordedAt = DateTime.Now;
                }
                if (product != null)
                {
                    product.Quantity = quantity;
                    product.ExpiryDate = exp;
                    PRN221_Project_02Context.instance.Products.Update(product);
                    PRN221_Project_02Context.instance.SaveChanges();

                    Inventories.Add(new Inventory
                    {
                        ProductId = proID,
                        WarehouseId = product.Category.Warehouse.WarehouseId,
                        Quantity = quantity,
                        ExpiryDate = exp,
                        Temperature = temp,
                        CheckedAt = checkAt,
                        CheckedBy = userid 
                    });
                }
            }
            // Save the Inventories list to the database
            PRN221_Project_02Context.instance.Inventories.AddRange(Inventories);
            PRN221_Project_02Context.instance.SaveChanges();

            return RedirectToPage("/User_Page/User_HistoryInventory");
        }
    }
}
