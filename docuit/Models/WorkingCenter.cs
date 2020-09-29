using System;
using System.Collections.Generic;

namespace DocuItService.Models
{
    public partial class WorkingCenter
    {
        public WorkingCenter()
        {
            WorkingCenterProject = new HashSet<WorkingCenterProject>();
        }

        public string Id { get; set; }
        public int CompanyId { get; set; }
        public string Name { get; set; }

        public virtual Company Company { get; set; }
        public virtual ICollection<WorkingCenterProject> WorkingCenterProject { get; set; }
    }
}
