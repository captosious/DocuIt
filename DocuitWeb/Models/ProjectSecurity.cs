using System;
using System.Collections.Generic;

namespace DocuitWeb.Models
{

    public partial class ProjectSecurity
    {
        public ProjectSecurity()
        {
            Dossiers = new HashSet<Dossier>();
        }

        public int CompanyId { get; set; }
        public int ProjectId { get; set; }
        public int UserId { get; set; }
        public DateTime CreationTime { get; set; }
        public int Rights { get; set; }

        public virtual User User { get; set; }
        public virtual ICollection<Dossier> Dossiers { get; set; }
    }
}

