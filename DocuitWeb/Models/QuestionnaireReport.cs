using System;
using System.Collections.Generic;

namespace DocuitWeb.Models
{
    public partial class QuestionnaireReport
    {
        public QuestionnaireReport()
        {
            Pictures = new HashSet<Pictures>();
            QuestionnaireReportAnswers = new HashSet<QuestionnaireReportAnswers>();
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
        public virtual ICollection<Pictures> Pictures { get; set; }
        public virtual ICollection<QuestionnaireReportAnswers> QuestionnaireReportAnswers { get; set; }
    }
}
