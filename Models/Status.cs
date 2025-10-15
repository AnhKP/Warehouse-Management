using System;
using System.Collections.Generic;

namespace PRN221_Project_02.Models
{
    public partial class Status
    {
        public Status()
        {
            Categories = new HashSet<Category>();
            Products = new HashSet<Product>();
            StockExports = new HashSet<StockExport>();
            StockImports = new HashSet<StockImport>();
            Users = new HashSet<User>();
            Warehouses = new HashSet<Warehouse>();
        }

        public int StatusId { get; set; }
        public string StatusName { get; set; } = null!;

        public virtual ICollection<Category> Categories { get; set; }
        public virtual ICollection<Product> Products { get; set; }
        public virtual ICollection<StockExport> StockExports { get; set; }
        public virtual ICollection<StockImport> StockImports { get; set; }
        public virtual ICollection<User> Users { get; set; }
        public virtual ICollection<Warehouse> Warehouses { get; set; }
    }
}
