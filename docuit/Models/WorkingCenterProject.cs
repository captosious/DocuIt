using System;
using System.Collections.Generic;

#nullable disable

namespace DocuItService.Models
{
    public partial class WorkingCenterProject
    {
        public WorkingCenterProject()
        {
            QuestionnaireReports = new HashSet<QuestionnaireReport>();
        }

        public int CompanyId { get; set; }
        public int ProjectId { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }

        public virtual WorkingCenter WorkingCenter { get; set; }
        public virtual ICollection<QuestionnaireReport> QuestionnaireReports { get; set; }
    }
}
