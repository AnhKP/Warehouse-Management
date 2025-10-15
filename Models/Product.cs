using System;
using System.Collections.Generic;

namespace PRN221_Project_02.Models
{
    public partial class Product
    {
        public Product()
        {
            Batches = new HashSet<Batch>();
            Inventories = new HashSet<Inventory>();
            StockExports = new HashSet<StockExport>();
            StockImports = new HashSet<StockImport>();
            TemperatureLogs = new HashSet<TemperatureLog>();
        }

        public int ProductId { get; set; }
        public int UserId { get; set; }
        public int CategoryId { get; set; }
        public string ProductName { get; set; } = null!;
        public int Quantity { get; set; }
        public decimal? Price { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? StatusId { get; set; }
        public int? SupplierId { get; set; }

        public virtual Category Category { get; set; } = null!;
        public virtual Status? Status { get; set; }
        public virtual Supplier? Supplier { get; set; }
        public virtual User User { get; set; } = null!;
        public virtual ICollection<Batch> Batches { get; set; }
        public virtual ICollection<Inventory> Inventories { get; set; }
        public virtual ICollection<StockExport> StockExports { get; set; }
        public virtual ICollection<StockImport> StockImports { get; set; }
        public virtual ICollection<TemperatureLog> TemperatureLogs { get; set; }
    }
}
