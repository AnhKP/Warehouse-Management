using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PRN221_Project_02.Models;
using PRN221_Project_02.Services;
using System.Collections.Generic;

namespace PRN221_Project_02.Pages.Manager_Page
{
    public class Manager_ImportModel : PageModel
    {
        private readonly ZohoInvoiceService _invoiceService;

        public Manager_ImportModel()
        {
            _invoiceService = new ZohoInvoiceService();
        }

        public int userid { get; set; }
        public string fullname { get; set; }
        public string username { get; set; }
        public List<StockImport> stockImports { get; set; }

        public void OnGet()
        {
            var userIdObj = HttpContext.Session.GetInt32("UserID");

            if (userIdObj.HasValue)
            {
                username = HttpContext.Session.GetString("Username");
                fullname = HttpContext.Session.GetString("Fullname");

                var manager = PRN221_Project_02Context.instance.Users
                                .FirstOrDefault(x => x.UserID == userIdObj);

                stockImports = PRN221_Project_02Context.instance.StockImports
                    .Include(x => x.Warehouse)
                    .Include(x => x.Supplier)
                    .Include(x => x.Status)
                    .Include(x => x.User)
                    .Where(x => x.WarehouseId == manager.WarehouseId).ToList();
            }
        }

        // Phương thức xử lý khi nhấn nút "Invoice" gửi yêu cầu POST
        public IActionResult OnPostGenerateInvoice(int movementId)
        {
            // Tìm stockImport theo movementId
            var stockImport = PRN221_Project_02Context.instance.StockImports
                .Include(x => x.Product)
                .Include(x => x.Warehouse)
                .Include(x => x.Supplier)
                .Include(x => x.Status)
                .Include(x => x.User)
                .FirstOrDefault(x => x.MovementId == movementId);

            if (stockImport != null)
            {
                _invoiceService.OpenAndFillZohoInvoice(
                    stockImport.Product.ProductName,
                    stockImport.Warehouse.WarehouseName,
                    stockImport.Quantity.ToString(),
                    stockImport.Supplier.SupplierName,
                    stockImport.MovementDate?.ToString("dd/MM/yyyy"),
                    stockImport.Status.StatusName,
                    stockImport.User.FullName,
                    stockImport.Supplier.Address,
                    stockImport.Supplier.ContactPhone,
                    stockImport.Supplier.Email,
                    stockImport.MovementId,
                    stockImport.Product.Price
                );
            }

            // Chuyển hướng lại trang sau khi hoàn thành
            return RedirectToPage("Manager_Import");
        }
    }
}
