using System;
using System.Collections.Generic;

namespace PRN221_Project_02.Models
{
    public partial class Warehouse
    {
        public Warehouse()
        {
            Batches = new HashSet<Batch>();
            Categories = new HashSet<Category>();
            Inventories = new HashSet<Inventory>();
            StockExports = new HashSet<StockExport>();
            StockImports = new HashSet<StockImport>();
            TemperatureLogs = new HashSet<TemperatureLog>();
            Users = new HashSet<User>();
        }

        public int WarehouseId { get; set; }
        public string WarehouseName { get; set; } = null!;
        public string? Location { get; set; }
        public int? Capacity { get; set; }
        public string? TemperatureRange { get; set; }
        public int? StatusId { get; set; }

        public virtual Status? Status { get; set; }
        public virtual ICollection<Batch> Batches { get; set; }
        public virtual ICollection<Category> Categories { get; set; }
        public virtual ICollection<Inventory> Inventories { get; set; }
        public virtual ICollection<StockExport> StockExports { get; set; }
        public virtual ICollection<StockImport> StockImports { get; set; }
        public virtual ICollection<TemperatureLog> TemperatureLogs { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
