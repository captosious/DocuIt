using System;
using System.Collections.Generic;

namespace DocuItService.Models
{
    public partial class WorkingCenterProject
    {
        public int CompanyId { get; set; }
        public int ProjectId { get; set; }
        public string Id { get; set; }

        public virtual WorkingCenter WorkingCenter { get; set; }
    }
}
