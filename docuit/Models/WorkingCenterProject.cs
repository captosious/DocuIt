using System;
using System.Collections.Generic;

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

        public virtual WorkingCenter WorkingCenter { get; set; }
        public virtual ICollection<QuestionnaireReport> QuestionnaireReport { get; set; }
    }
}
