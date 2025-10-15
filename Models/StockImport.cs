using System;
using System.Collections.Generic;

namespace PRN221_Project_02.Models
{
    public partial class StockImport
    {
        public int MovementId { get; set; }
        public int ProductId { get; set; }
        public int? UserId { get; set; }
        public int? WarehouseId { get; set; }
        public int? Quantity { get; set; }
        public int? StatusId { get; set; }
        public int? SupplierId { get; set; }
        public DateTime? MovementDate { get; set; }

        public virtual Product Product { get; set; } = null!;
        public virtual Status? Status { get; set; }
        public virtual Supplier? Supplier { get; set; }
        public virtual User? User { get; set; }
        public virtual Warehouse? Warehouse { get; set; }
    }
}
