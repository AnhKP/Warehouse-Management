using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PRN221_Project_02.Models;
using System;

namespace PRN221_Project.Pages.Manager_Page
{
    public class Manager_HomeModel : PageModel
    {
        public int userid {  get; set; }
        public string fullname { get; set; }
        public string username { get; set; }

        public int numberEmp {  get; set; }
        public int numberPro {  get; set; }

        public int? capacityFull { get; set; }
        public int? capacityNow { get; set; }

        public List<User> Users { get; set; }
        public List<Product> Products { get; set; }
        public List<Batch> Batchs { get; set; }
        public void OnGet()
        {
            var userIdObj = HttpContext.Session.GetInt32("UserID");
            if (userIdObj.HasValue)
            {
                username = HttpContext.Session.GetString("Username");
                var manager = PRN221_Project_02Context.instance.Users.FirstOrDefault(x => x.UserID == userIdObj);
                userid = userIdObj.Value;
                fullname = HttpContext.Session.GetString("Fullname");
                numberEmp= PRN221_Project_02Context.instance.Users.Where(x=>x.WarehouseId.Equals(manager.WarehouseId) && x.RoleId==3).Count();
                Users = PRN221_Project_02Context.instance.Users.Include(x=>x.Status).Where(x => x.WarehouseId.Equals(manager.WarehouseId) && x.RoleId == 3).ToList();
                
                
                capacityFull =  PRN221_Project_02Context.instance.Users
                            .Include(x => x.Warehouse)
                            .Where(x => x.UserID == userid)
                            .Select(x => x.Warehouse.Capacity)
                            .FirstOrDefault();

                // Lấy warehouse mà manager quản lý
                var warehouse = PRN221_Project_02Context.instance.Users
                    .Where(u => u.UserID == userid && u.RoleId == 2) // Check user là manager
                    .Join(
                        PRN221_Project_02Context.instance.Warehouses,
                        user => user.WarehouseId,
                        warehouse => warehouse.WarehouseId,
                        (user, warehouse) => warehouse
                    )
                    .FirstOrDefault(); // Chọn kho đầu tiên quản lý bởi manager

                // Nếu warehouse tồn tại, tiếp tục lấy tổng số lượng sản phẩm
                
                if (warehouse != null)
                {
                    capacityNow = PRN221_Project_02Context.instance.Products
                        .Where(product => product.Category.WarehouseId == warehouse.WarehouseId && product.StatusId==2) // Lọc sản phẩm thuộc kho
                        .Select(product => product.Quantity)
                        .Sum(); // Tính tổng số lượng sản phẩm

                    Products = PRN221_Project_02Context.instance.Products
                        .Include(x=>x.Category)
                        .Where(product => product.Category.WarehouseId == warehouse.WarehouseId).ToList();

                    Batchs = PRN221_Project_02Context.instance.Batches
                          .Include(x => x.Supplier)
                          .Include(x=>x.Product)
                          .Include(x=>x.User)
                          .Include(x=>x.Status)
                          .Where(Batch => Batch.WarehouseId == warehouse.WarehouseId)
                        .ToList();


                }



                // Lấy danh sách WarehouseId của manager
                var warehouseIds = PRN221_Project_02Context.instance.Users
                    .Where(u => u.UserID == userid && u.RoleId == 2) // Chỉ lấy User là manager
                    .Select(u => u.WarehouseId)
                    .ToList();
                // Lấy tổng số lượng sản phẩm trong các kho thuộc quản lý của manager
                numberPro = PRN221_Project_02Context.instance.Products
                    .Where(p => warehouseIds.Contains(p.Category.WarehouseId)) // Lọc các sản phẩm thuộc kho của manager
                    .Count();
            }
        }
    }
}
