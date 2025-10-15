using System;
using System.Collections.Generic;

namespace PRN221_Project_02.Models
{
    public partial class Supplier
    {
        public Supplier()
        {
            Batches = new HashSet<Batch>();
            Products = new HashSet<Product>();
            StockImports = new HashSet<StockImport>();
        }

        public int SupplierId { get; set; }
        public string SupplierName { get; set; } = null!;
        public string? ContactName { get; set; }
        public string? ContactPhone { get; set; }
        public string? Address { get; set; }
        public string? Email { get; set; }
        public string? Website { get; set; }

        public virtual ICollection<Batch> Batches { get; set; }
        public virtual ICollection<Product> Products { get; set; }
        public virtual ICollection<StockImport> StockImports { get; set; }
    }
}
