using System;
using System.Collections.Generic;

namespace PRN221_Project_02.Models
{
    public partial class Category
    {
        public Category()
        {
            Products = new HashSet<Product>();
        }

        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = null!;
        public int? StatusId { get; set; }
        public int? WarehouseId { get; set; }

        public virtual Status? Status { get; set; }
        public virtual Warehouse? Warehouse { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
