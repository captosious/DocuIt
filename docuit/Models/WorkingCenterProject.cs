using System;
using System.Collections.Generic;

namespace DocuItService.Models
{
    public partial class WorkingCenterProject
    {
        public WorkingCenterProject()
        {
            InventoryReport = new HashSet<InventoryReport>();
        }

        public int CompanyId { get; set; }
        public int ProjectId { get; set; }
        public string Id { get; set; }

        public virtual WorkingCenter WorkingCenter { get; set; }
        public virtual ICollection<InventoryReport> InventoryReport { get; set; }
    }
}
