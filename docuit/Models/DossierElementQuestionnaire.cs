using System;
using System.Collections.Generic;

#nullable disable

namespace DocuItService.Models
{
    public partial class DossierElementQuestionnaire
    {
        public int CompanyId { get; set; }
        public int ProjectId { get; set; }
        public int DossierId { get; set; }
        public int DossierElementId { get; set; }
        public int Id { get; set; }

        public virtual DossierElement DossierElement { get; set; }
    }
}
