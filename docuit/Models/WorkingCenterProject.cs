using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace DocuItService.Models
{
    public partial class WorkingCenterProject
    {
        public WorkingCenterProject()
        {
            QuestionnaireReport = new HashSet<QuestionnaireReport>();
        }

        public int CompanyId { get; set; }
        public int ProjectId { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }

        public virtual WorkingCenter WorkingCenter { get; set; }
        public virtual ICollection<QuestionnaireReport> QuestionnaireReport { get; set; }
    }
}
