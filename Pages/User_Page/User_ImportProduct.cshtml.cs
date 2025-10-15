using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PRN221_Project_02.Models;

namespace PRN221_Project_02.Pages.User_Page
{
    public class User_ImportProductModel : PageModel
    {
        public List<Product> products { get; set; }

        public int? wareHouseID { get; set; }
        public string fullname { get; set; }
        public Batch Batch { get; set; }

        public Product Product { get; set; }
        public int userid { get; set; }
        [BindProperty(SupportsGet = true)]
        public string batchid { get; set; }

        [BindProperty(SupportsGet = true)]
        public int userId { get; set; }
        public List<Status> Status { get; set; }
        public void OnGet(string batchid)
        {
            int batchID = int.Parse(batchid);
            Batch = PRN221_Project_02Context.instance.Batches.Include(x => x.Product)
                                                             .Include(x => x.Status)
                                                             .Include(x => x.User)
                                                             .Include(x => x.Warehouse)
                                                             .Include(x => x.Supplier)
                                                             .Include(x => x.Product.TemperatureLogs)
                                                             .FirstOrDefault(x => x.BatchId == batchID);
            Product = PRN221_Project_02Context.instance.Products.FirstOrDefault(x => x.ProductId == Batch.ProductId);
            Status = PRN221_Project_02Context.instance.Statuses.ToList();
        }
        public IActionResult OnPost(IFormCollection form)
        {
            string temper = form["temperature"];
            int degreeIndex = temper.IndexOf("°");
            string numericPartString = temper.Substring(0, degreeIndex).Trim();
            decimal temperature = decimal.Parse(numericPartString);
            var batchName = form["batchName"];
            int batchQuantity = int.Parse(form["batchquantity"]);
            int productQuantity = int.Parse(form["productquantity"]);
            decimal productPrice = decimal.Parse(form["price"]);
            DateTime expiryDate = DateTime.Parse(form["expiryDate"]);
            int statusID = int.Parse(form["statusID"]);
            int productID = int.Parse(form["productID"]);
            int batchID = int.Parse(form["batchID"]);
            int userID = int.Parse(form["userID"]);

            var product = PRN221_Project_02Context.instance.Products.FirstOrDefault(x => x.ProductId == productID);
            var batchObj = PRN221_Project_02Context.instance.Batches.FirstOrDefault(x => x.BatchId == batchID);

            if (product != null)
            {
                product.SupplierId = batchObj.SupplierId;
                product.Quantity = product.Quantity + productQuantity;
                product.ExpiryDate = expiryDate;
                product.CreatedAt = DateTime.Now;
                product.UpdatedAt = DateTime.Now;
                product.StatusId = statusID;
                PRN221_Project_02Context.instance.Products.Update(product);
                PRN221_Project_02Context.instance.SaveChanges();
            }
            int proID = product.ProductId;
            var temp = PRN221_Project_02Context.instance.TemperatureLogs.FirstOrDefault(x => x.ProductId == proID);
            if (temp != null) {
                temp.RecordedTemperature = temperature;
                temp.RecordedAt = DateTime.Now;
                PRN221_Project_02Context.instance.TemperatureLogs.Update(temp);
                PRN221_Project_02Context.instance.SaveChanges();
            }
            else
            {
                temp.ProductId = proID;
                temp.RecordedTemperature = temperature;
                temp.RecordedAt = DateTime.Now;
                PRN221_Project_02Context.instance.TemperatureLogs.Add(temp);
                PRN221_Project_02Context.instance.SaveChanges();
            }
            if (batchObj != null) {
                if(productQuantity < batchQuantity)
                {
                    batchObj.Quantity = batchQuantity - productQuantity;
                    PRN221_Project_02Context.instance.Batches.Update(batchObj);
                    PRN221_Project_02Context.instance.SaveChanges();
                }
                else
                {
                    batchObj.Quantity = 0;
                    batchObj.StatusId = 6;
                    PRN221_Project_02Context.instance.Batches.Update(batchObj);
                    PRN221_Project_02Context.instance.SaveChanges();
                }
            }

            StockImport stock = new StockImport();
            stock.ProductId = proID;
            stock.UserId = batchObj.UserID;
            stock.SupplierId = batchObj.SupplierId;
            stock.WarehouseId = batchObj.WarehouseId;
            stock.Quantity = productQuantity;
            stock.StatusId = 7;
            stock.MovementDate = DateTime.Now.Date;
            PRN221_Project_02Context.instance.StockImports.Add(stock);
            PRN221_Project_02Context.instance.SaveChanges();
            return RedirectToPage("/User_Page/User_Home");
        }
    }
}
