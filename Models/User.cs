using System;
using System.Collections.Generic;

namespace PRN221_Project_02.Models
{
    public partial class User
    {
        public User()
        {
            Batches = new HashSet<Batch>();
            Inventories = new HashSet<Inventory>();
            Products = new HashSet<Product>();
            StockExports = new HashSet<StockExport>();
            StockImports = new HashSet<StockImport>();
        }

        public int UserId { get; set; }
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? FullName { get; set; }
        public string? PhoneNumber { get; set; }
        public DateTime? Dob { get; set; }
        public int? RoleId { get; set; }
        public int? StatusId { get; set; }
        public int? WarehouseId { get; set; }
        public string? CodePwd { get; set; }

        public virtual Role? Role { get; set; }
        public virtual Status? Status { get; set; }
        public virtual Warehouse? Warehouse { get; set; }
        public virtual ICollection<Batch> Batches { get; set; }
        public virtual ICollection<Inventory> Inventories { get; set; }
        public virtual ICollection<Product> Products { get; set; }
        public virtual ICollection<StockExport> StockExports { get; set; }
        public virtual ICollection<StockImport> StockImports { get; set; }
    }
}
