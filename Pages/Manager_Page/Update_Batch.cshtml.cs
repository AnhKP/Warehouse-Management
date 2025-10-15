using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PRN221_Project_02.Models;

namespace PRN221_Project_02.Pages.Manager_Page
{
    public class Update_BatchModel : PageModel
    {
        public int userid { get; set; }
        public string fullname { get; set; }
        public string username { get; set; }

        public Batch batch { get; set; }
        public List<Supplier> suppliers { get; set; }
        public  List<User> users { get; set; }
        public void OnGet(int bid)
        {
            var userIdObj = HttpContext.Session.GetInt32("UserID");

            if (userIdObj.HasValue)
            {
                username = HttpContext.Session.GetString("Username");
                fullname = HttpContext.Session.GetString("Fullname");

                var manager = PRN221_Project_02Context.instance.Users
                                .FirstOrDefault(x => x.UserID == userIdObj);
                batch = PRN221_Project_02Context.instance.Batches.Find(bid);
                suppliers= PRN221_Project_02Context.instance.Suppliers.ToList();
                users= PRN221_Project_02Context.instance.Users.Where(x=>x.WarehouseId == manager.WarehouseId && x.RoleId==3).ToList();
            }
        }

        public IActionResult OnPost()
        {
            // Lấy dữ liệu từ form và sử dụng int.TryParse để tránh lỗi FormatException
            if (!int.TryParse(Request.Form["batchId"], out int batchId))
            {
                ModelState.AddModelError("batchId", "Invalid Batch ID.");
                return Page();
            }

            // Tương tự, bạn có thể dùng TryParse cho các giá trị số khác
            string batchNumber = Request.Form["batchNumber"];
            string productName = Request.Form["productName"];

            if (!DateTime.TryParse(Request.Form["manufactureDate"], out DateTime manufactureDate))
            {
                ModelState.AddModelError("manufactureDate", "Invalid Manufacture Date.");
                return Page();
            }

            if (!DateTime.TryParse(Request.Form["expiryDate"], out DateTime expiryDate))
            {
                ModelState.AddModelError("expiryDate", "Invalid Expiry Date.");
                return Page();
            }

            if (!int.TryParse(Request.Form["quantity"], out int quantity))
            {
                ModelState.AddModelError("quantity", "Invalid Quantity.");
                return Page();
            }

            if (!int.TryParse(Request.Form["supplier"], out int supplierId))
            {
                ModelState.AddModelError("supplier", "Invalid Supplier ID.");
                return Page();
            }

            if (!int.TryParse(Request.Form["status"], out int statusId))
            {
                ModelState.AddModelError("status", "Invalid Status ID.");
                return Page();
            }

            // Kiểm tra trạng thái và lấy thông tin nhân viên hoặc lý do từ chối
            int? userId = null;
            string rejectReason = null;

            if (statusId == 16) // Import
            {
                if (!string.IsNullOrEmpty(Request.Form["user"]) && int.TryParse(Request.Form["user"], out int parsedUserId))
                {
                    userId = parsedUserId;
                    rejectReason = "";
                }
            }
            else if (statusId == 8) // Reject
            {
                rejectReason = Request.Form["rejectReason"];
            }

            // Tìm batch trong cơ sở dữ liệu
            var existingBatch = PRN221_Project_02Context.instance.Batches.Find(batchId);
            if (existingBatch == null)
            {
                ModelState.AddModelError("batchId", "Batch not found.");
                return Page(); // Nếu không tìm thấy batch, quay lại trang và thông báo lỗi
            }

            // Cập nhật các trường của batch
            existingBatch.BatchNumber = batchNumber ?? existingBatch.BatchNumber;
            existingBatch.ManufactureDate = manufactureDate;
            existingBatch.ExpiryDate = expiryDate;
            existingBatch.Quantity = quantity;
            existingBatch.SupplierId = supplierId;
            existingBatch.StatusId = statusId;
            existingBatch.UserID = userId;
            existingBatch.RejectReason = rejectReason;
            existingBatch.UpdateDate = DateTime.Now;

            // Lưu thay đổi vào cơ sở dữ liệu
            PRN221_Project_02Context.instance.SaveChanges();

            return RedirectToPage("Manager_Batch"); // Chuyển hướng về trang quản lý lô hàng
        }




    }
}
