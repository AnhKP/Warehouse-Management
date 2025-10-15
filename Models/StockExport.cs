using System;
using System.Collections.Generic;

namespace PRN221_Project_02.Models
{
    public partial class StockExport
    {
        public int ExportId { get; set; }
        public int ProductId { get; set; }
        public int? UserId { get; set; }
        public int? WarehouseId { get; set; }
        public int? AgentId { get; set; }
        public int? Quantity { get; set; }
        public int? StatusId { get; set; }
        public DateTime? MovementDate { get; set; }

        public virtual Agent? Agent { get; set; }
        public virtual Product Product { get; set; } = null!;
        public virtual Status? Status { get; set; }
        public virtual User? User { get; set; }
        public virtual Warehouse? Warehouse { get; set; }
    }
}
