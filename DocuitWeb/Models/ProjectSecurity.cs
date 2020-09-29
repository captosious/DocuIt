using System;
using System.Collections.Generic;

namespace DocuitWeb.Models
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
        public DateTimeOffset CreationTime { get; set; }

        public virtual Project Project { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<Dossier> Dossier { get; set; }
    }
}

