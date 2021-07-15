using System;
using System.Collections.Generic;

#nullable disable

namespace DocuItService.Models
{
    public partial class DossierElement
    {
        public DossierElement()
        {
            DossierElementQuestionnaires = new HashSet<DossierElementQuestionnaire>();
        }

        public int CompanyId { get; set; }
        public int ProjectId { get; set; }
        public int DossierId { get; set; }
        public int ElementId { get; set; }
        public int ElementTypeId { get; set; }
        public string Name { get; set; }
        public double? LocationLatitude { get; set; }
        public double? LocationLongitude { get; set; }
        public string Comment { get; set; }
        public string FileId { get; set; }
        public DateTime CreationTime { get; set; }

        public virtual Dossier Dossier { get; set; }
        public virtual ICollection<DossierElementQuestionnaire> DossierElementQuestionnaires { get; set; }
    }
}
