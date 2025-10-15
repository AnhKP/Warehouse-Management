using System;
using System.Collections.Generic;

namespace PRN221_Project_02.Models
{
    public partial class Agent
    {
        public Agent()
        {
            StockExports = new HashSet<StockExport>();
        }

        public int AgentId { get; set; }
        public string AgentName { get; set; } = null!;
        public string? ContactNumber { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }

        public virtual ICollection<StockExport> StockExports { get; set; }
    }
}
