using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PRN221_Project_02.Models;

namespace PRN221_Project_02.Pages.User_Page
{
    public class User_UpdateProductModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public int id { get; set; }

        [BindProperty(SupportsGet = true)]
        public int userId { get; set; }
        public Product product { get; set; }

        public TemperatureLog TemperatureLog { get; set; }
        public List<Status> Status { get; set; }

        public void OnGet()
        {
          product = PRN221_Project_02Context.instance.Products.Include(x => x.Category)
                                            .Include(x => x.Status)
                                            .Include(x => x.User)
                                            .Include(x => x.Category.Warehouse)
                                            .Include(x => x.Supplier)
                                            .Include(x => x.TemperatureLogs)
                                            .Where(x => x.UserId == userId && x.StatusId != 1 && x.ProductId == id).FirstOrDefault();
            TemperatureLog = PRN221_Project_02Context.instance.TemperatureLogs.Where(x => x.ProductId == product.ProductId).FirstOrDefault();
            Status = PRN221_Project_02Context.instance.Statuses.ToList();
        }
            public IActionResult OnPost(IFormCollection form)
            {
                var productId = form["productID"];
                var productName = form["productName"];
                var categoryName = form["categoryName"];
                var quantity = form["quantity"];
                var price = form["price"];
                var expiryDate = form["expiryDate"];
                var supplier = form["supplier"];
                var createdAt = form["createdAt"];
                var updatedAt = form["updatedAt"];
                var temperature = form["temperature"];
                var statusId = form["statusID"];
                var userID = form["userID"];

                // Find the product to update
                var productToUpdate = PRN221_Project_02Context.instance.Products
                    .Include(p => p.Category)
                    .Include(p => p.Supplier)
                    .FirstOrDefault(p => p.ProductId == int.Parse(productId));
                productToUpdate.StatusId = Convert.ToInt32(statusId);         
                PRN221_Project_02Context.instance.Products.Update(productToUpdate);
                PRN221_Project_02Context.instance.SaveChanges();
            // Handle Temperature Log if required
            if (!string.IsNullOrEmpty(temperature))
            {
                var tempUpdate = PRN221_Project_02Context.instance.TemperatureLogs.FirstOrDefault(x => x.ProductId == productToUpdate.ProductId);
                // Assuming TemperatureLog is a separate entity
                if (tempUpdate != null)
                {
                    tempUpdate.RecordedTemperature = Convert.ToDecimal(temperature);
                    tempUpdate.RecordedAt = DateTime.Now;
                    PRN221_Project_02Context.instance.TemperatureLogs.Update(tempUpdate);
                    PRN221_Project_02Context.instance.SaveChanges();
                }
                else
                {
                    tempUpdate.ProductId = productToUpdate.ProductId;
                    tempUpdate.RecordedAt = DateTime.Now;
                    tempUpdate.RecordedTemperature = Convert.ToDecimal(temperature);
                    PRN221_Project_02Context.instance.TemperatureLogs.Add(tempUpdate);
                    PRN221_Project_02Context.instance.SaveChanges();
                }
            }     
                return RedirectToPage("/User_Page/User_Home");
            }
    }    
    }
