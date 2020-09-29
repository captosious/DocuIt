using System;
using System.Collections.Generic;

namespace DocuitWeb.Models
{
    public partial class Project
    {
        public Project()
        {
            Dossier = new HashSet<Dossier>();
            ProjectSecurity = new HashSet<ProjectSecurity>();
        }

        public int CompanyId { get; set; }
        public int ProjectId { get; set; }
        public string ReferenceId { get; set; }
        public int StatusId { get; set; }
        public string Name { get; set; }
        public DateTimeOffset CreationTime { get; set; }

        public virtual Company Company { get; set; }
        public virtual ICollection<Dossier> Dossier { get; set; }
        public virtual ICollection<ProjectSecurity> ProjectSecurity { get; set; }
    }
}
