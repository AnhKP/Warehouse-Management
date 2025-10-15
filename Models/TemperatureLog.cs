using System;
using System.Collections.Generic;

namespace PRN221_Project_02.Models
{
    public partial class TemperatureLog
    {
        public int LogId { get; set; }
        public int WarehouseId { get; set; }
        public int? ProductId { get; set; }
        public decimal? RecordedTemperature { get; set; }
        public DateTime? RecordedAt { get; set; }

        public virtual Product? Product { get; set; }
        public virtual Warehouse Warehouse { get; set; } = null!;
    }
}
