using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PRN221_Project_02.Models;

namespace PRN221_Project_02.Pages.Admin_Page
{
    public class Admin_ListWareHouseB : PageModel
    {
        public List<Warehouse> warehouselist { get; set; } = new List<Warehouse>();
        public List<Status> statuses { get; set; } = new List<Status>();
        public void OnGet(string wid, string status)
        {
            if (wid != null && status != null)
            {
                int wareID = int.Parse(wid);
                int sID = int.Parse(status);
                var wObject = PRN221_Project_02Context.instance.Warehouses.FirstOrDefault(x => x.WarehouseId == wareID);
                if (wObject != null)
                {
                    wObject.StatusId = sID;
                    PRN221_Project_02Context.instance.Update(wObject);
                    PRN221_Project_02Context.instance.SaveChanges();
                }
            }
            warehouselist = PRN221_Project_02Context.instance.Warehouses.Include(x => x.Status).ToList();
            statuses = PRN221_Project_02Context.instance.Statuses.ToList();
        }
    }
}
