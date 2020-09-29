using System;
using System.Collections.Generic;

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
