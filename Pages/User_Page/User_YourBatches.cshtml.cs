using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PRN221_Project_02.Models;

namespace PRN221_Project_02.Pages.User_Page
{
    public class User_YourBatchesModel : PageModel
    {
        public List<Product> products { get; set; }

        public List<Batch> listBatch { get; set; } = new List<Batch>();
        public Warehouse Warehouse { get; set; }

        public int? wareHouseID { get; set; }
        public string fullname { get; set; }
        public int userid { get; set; }       
        [BindProperty(SupportsGet = true)]
        public string batchid { get; set; }
        public void OnGet()
        {
            var userIdObj = HttpContext.Session.GetInt32("UserID");
            if (userIdObj.HasValue)
            {
                userid = userIdObj.Value;
                fullname = HttpContext.Session.GetString("Fullname");
                wareHouseID = PRN221_Project_02Context.instance.Users.Include(x => x.Warehouse).Where(x => x.UserID == userid).Select(x => x.WarehouseId).FirstOrDefault();
                listBatch = PRN221_Project_02Context.instance.Batches.Include(x => x.Product)
                                                                     .Include(x => x.Product.Category)
                                                                     .Include(x => x.Status)
                                                                     .Include(x => x.User)
                                                                     .Include(x => x.Warehouse)
                                                                     .Include(x => x.Supplier)
                                                                     .Include(x => x.Product.TemperatureLogs)
                                                                     .Where(x => x.UserID == userid && x.Warehouse.WarehouseId == wareHouseID && x.StatusId != 1).ToList();
            }
        }

        public IActionResult OnPost(IFormCollection form)
        {
            var userIdObj = HttpContext.Session.GetInt32("UserID");
            if (userIdObj.HasValue)
            {
                userid = userIdObj.Value;
                fullname = HttpContext.Session.GetString("Fullname");
                wareHouseID = PRN221_Project_02Context.instance.Users.Include(x => x.Warehouse).Where(x => x.UserID == userid).Select(x => x.WarehouseId).FirstOrDefault();
              
            }
            var batch = form["batchID"];
            var status = form["statusName"];
            var batchObject = PRN221_Project_02Context.instance.Batches.FirstOrDefault(x => x.BatchId == int.Parse(batch));
            if(batchObject != null)
            {
                batchObject.StatusId = int.Parse(status);
                PRN221_Project_02Context.instance.Batches.Update(batchObject);
                PRN221_Project_02Context.instance.SaveChanges();
            }
            listBatch = PRN221_Project_02Context.instance.Batches.Include(x => x.Product)
                                                                     .Include(x => x.Product.Category)
                                                                     .Include(x => x.Status)
                                                                     .Include(x => x.User)
                                                                     .Include(x => x.Warehouse)
                                                                     .Include(x => x.Supplier)
                                                                     .Include(x => x.Product.TemperatureLogs)
                                                                     .Where(x => x.UserID == userid && x.Warehouse.WarehouseId == wareHouseID && x.StatusId != 1).ToList();
            return Page();
        }
       
    }
}
