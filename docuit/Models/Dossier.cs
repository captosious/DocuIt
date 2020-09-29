using System;
using System.Collections.Generic;

namespace DocuItService.Models
{
    public partial class Dossier
    {
        public Dossier()
        {
            DossierElement = new HashSet<DossierElement>();
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
    }
}
