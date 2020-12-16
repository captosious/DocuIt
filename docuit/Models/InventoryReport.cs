using System;
using System.Collections.Generic;

namespace DocuItService.Models
{
    public partial class InventoryReport
    {
        public InventoryReport()
        {
            Pictures = new HashSet<Pictures>();
        }

        public int CompanyId { get; set; }
        public int ProjectId { get; set; }
        public int DossierId { get; set; }
        public string Id { get; set; }
        public string BuildingTypeId { get; set; }
        public string WorkingCenterId { get; set; }
        public string Comment { get; set; }

        public virtual Dossier Dossier { get; set; }
        public virtual WorkingCenterProject WorkingCenterProject { get; set; }
        public virtual ICollection<Pictures> Pictures { get; set; }
    }
}
