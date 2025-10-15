using System;
using System.Collections.Generic;

namespace PRN221_Project_02.Models
{
    public partial class Batch
    {
        public int BatchId { get; set; }
        public int ProductId { get; set; }
        public string BatchNumber { get; set; } = null!;
        public DateTime? ManufactureDate { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public int? Quantity { get; set; }
        public int? WarehouseId { get; set; }
        public int? SupplierId { get; set; }
        public int? UserId { get; set; }
        public DateTime? UpdateDate { get; set; }
        public int? StatusId { get; set; }
        public string? RejectReason { get; set; }

        public virtual Product Product { get; set; } = null!;
        public virtual Supplier? Supplier { get; set; }
        public virtual User? User { get; set; }
        public virtual Warehouse? Warehouse { get; set; }
    }
}
