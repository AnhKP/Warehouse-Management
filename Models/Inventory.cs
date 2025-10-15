using System;
using System.Collections.Generic;

namespace PRN221_Project_02.Models
{
    public partial class Inventory
    {
        public int InventoryId { get; set; }
        public int WarehouseId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public decimal? Temperature { get; set; }
        public DateTime? CheckedAt { get; set; }
        public int? CheckedBy { get; set; }

        public virtual User? CheckedByNavigation { get; set; }
        public virtual Product Product { get; set; } = null!;
        public virtual Warehouse Warehouse { get; set; } = null!;
    }
}
