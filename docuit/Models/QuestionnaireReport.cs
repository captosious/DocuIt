using System;
using System.Collections.Generic;

#nullable disable

namespace DocuItService.Models
{
    public partial class QuestionnaireReport
    {
        public QuestionnaireReport()
        {
            Pictures = new HashSet<Picture>();
            QuestionnaireReportAnswers = new HashSet<QuestionnaireReportAnswer>();
        }

        public int CompanyId { get; set; }
        public int ProjectId { get; set; }
        public int DossierId { get; set; }
        public string Id { get; set; }
        public string BuildingTypeId { get; set; }
        public string WorkingCenterId { get; set; }
        public string QuestionnaireTypeId { get; set; }
        public string Comment { get; set; }

        public virtual Dossier Dossier { get; set; }
        public virtual WorkingCenterProject WorkingCenterProject { get; set; }
        public virtual ICollection<Picture> Pictures { get; set; }
        public virtual ICollection<QuestionnaireReportAnswer> QuestionnaireReportAnswers { get; set; }
    }
}
