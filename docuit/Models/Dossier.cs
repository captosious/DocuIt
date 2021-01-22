using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace DocuItService.Models
{
    public partial class Dossier
    {
        public Dossier()
        {
            DossierElement = new HashSet<DossierElement>();
            QuestionnaireReport = new HashSet<QuestionnaireReport>();
        }

        public int CompanyId { get; set; }
        public int ProjectId { get; set; }
        public int DossierId { get; set; }
        public string Name { get; set; }
        public string ReferenceId { get; set; }
        public double? LocationLatitude { get; set; }
        public double? LocationLongitude { get; set; }
        public int UserId { get; set; }
        public DateTimeOffset CreationTime { get; set; }

        public virtual ProjectSecurity ProjectSecurity { get; set; }
        public virtual ICollection<DossierElement> DossierElement { get; set; }
        public virtual ICollection<QuestionnaireReport> QuestionnaireReport { get; set; }
    }
}
