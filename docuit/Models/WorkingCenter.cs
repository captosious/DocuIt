using System;
using System.Collections.Generic;

#nullable disable

namespace DocuItService.Models
{
    public partial class WorkingCenter
    {
        public WorkingCenter()
        {
            WorkingCenterProjects = new HashSet<WorkingCenterProject>();
        }

        public string Id { get; set; }
        public int CompanyId { get; set; }
        public string Name { get; set; }

        public virtual Company Company { get; set; }
        public virtual ICollection<WorkingCenterProject> WorkingCenterProjects { get; set; }
    }
}
