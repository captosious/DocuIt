using System;
using System.Collections.Generic;


namespace DocuitWeb.Models
{
    public partial class WorkingCenter : ICloneable
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

        public object Clone()
        {
            WorkingCenter workingCenter = new WorkingCenter();

            workingCenter.CompanyId = this.CompanyId;
            workingCenter.Id = this.Id;
            workingCenter.Name = this.Name;

            workingCenter.Company = this.Company;
            workingCenter.WorkingCenterProject = this.WorkingCenterProject;

            return workingCenter;
        }
    }
}
