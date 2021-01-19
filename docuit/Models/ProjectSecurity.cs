using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace DocuItService.Models
{
    public partial class ProjectSecurity
    {
        public ProjectSecurity()
        {
            Dossier = new HashSet<Dossier>();
        }

        public int CompanyId { get; set; }
        public int ProjectId { get; set; }
        public int UserId { get; set; }
        public byte IsOwner { get; set; }

        public virtual User User { get; set; }
        public virtual ICollection<Dossier> Dossier { get; set; }
    }
}
