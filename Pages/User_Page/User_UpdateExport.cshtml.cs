using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PRN221_Project_02.Models;

namespace PRN221_Project_02.Pages.User_Page
{
    public class User_UpdateExportModel : PageModel
    {
        public StockExport StockExport { get; set; } = new StockExport();
        public string fullname { get; set; }
        public int userid { get; set; }
        public void OnGet(string exportid)
        {
            var userIdObj = HttpContext.Session.GetInt32("UserID");
            if (userIdObj.HasValue)
            {

                userid = userIdObj.Value;
                fullname = HttpContext.Session.GetString("Fullname");

            }
            int exportID = int.Parse(exportid);
            StockExport = PRN221_Project_02Context.instance.StockExports.Include(x => x.Product)
                                                                        .Include(x => x.Product.Supplier)
                                                                        .Include(x => x.Agent).FirstOrDefault(x => x.ExportId == exportID);
        }
        public IActionResult OnPost(IFormCollection form)
        {
            string address = form["address"];
            int productQuantity = int.Parse(form["productQuantity"]);
            DateTime moveDate = DateTime.Parse(form["movementDate"]);
            int productID = int.Parse(form["prodictID"]);
            int expID = int.Parse(form["ExportId"]);
            int uID = int.Parse(form["UserId"]);
            int AgenID = int.Parse(form["AgenID"]);

            var agen = PRN221_Project_02Context.instance.Agents.FirstOrDefault(x => x.AgentId == AgenID);
            if(agen != null)
            {
                agen.Address = address;
                PRN221_Project_02Context.instance.Agents.Update(agen);
                PRN221_Project_02Context.instance.SaveChanges();
            }
            var expObj = PRN221_Project_02Context.instance.StockExports.FirstOrDefault(x => x.ExportId == expID);
            if(expObj != null)
            {
                expObj.Quantity = productQuantity;
                expObj.MovementDate = moveDate;
                PRN221_Project_02Context.instance.StockExports.Update(expObj);
                PRN221_Project_02Context.instance.SaveChanges();
            }
            int quantityUpdate = (int)expObj.Quantity - productQuantity;
            var prod = PRN221_Project_02Context.instance.Products.FirstOrDefault(x => x.ProductId == productID);
            if (prod != null)
            {
                prod.Quantity = prod.Quantity - quantityUpdate;
                PRN221_Project_02Context.instance.Products.Update(prod);
                PRN221_Project_02Context.instance.SaveChanges();
            }
            return RedirectToPage("/User_Page/User_HistoryExport");
        }
    }
}
