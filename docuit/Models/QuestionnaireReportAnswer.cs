using System;
using System.Collections.Generic;

#nullable disable

namespace DocuItService.Models
{
    public partial class QuestionnaireReportAnswer
    {
        public int CompanyId { get; set; }
        public int ProjectId { get; set; }
        public int DossierId { get; set; }
        public string QuestionnaireReportId { get; set; }
        public string QuestionId { get; set; }
        public string Answer { get; set; }

        public virtual QuestionnaireReport QuestionnaireReport { get; set; }
    }
}
