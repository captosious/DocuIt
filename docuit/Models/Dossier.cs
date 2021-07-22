using System;
using System.Collections.Generic;

#nullable disable

namespace DocuItService.Models
{
    public partial class Dossier
    {
        public Dossier()
        {
            DossierElements = new HashSet<DossierElement>();
            QuestionnaireReports = new HashSet<QuestionnaireReport>();
        }

        public int CompanyId { get; set; }
        public int ProjectId { get; set; }
        public int DossierId { get; set; }
        public string Name { get; set; }
        public string ReferenceId { get; set; }
        public double? LocationLatitude { get; set; }
        public double? LocationLongitude { get; set; }
        public int? ParentId { get; set; }
        public int? ChildId { get; set; }

        public virtual ICollection<DossierElement> DossierElements { get; set; }
        public virtual ICollection<QuestionnaireReport> QuestionnaireReports { get; set; }
    }
}
