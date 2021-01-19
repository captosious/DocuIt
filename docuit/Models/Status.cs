using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace DocuItService.Models
{
    public partial class Status
    {
        public Status()
        {
            Project = new HashSet<Project>();
        }

        public int StatusId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Project> Project { get; set; }
    }
}
