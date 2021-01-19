using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace DocuItService.Models
{
    public partial class Project
    {
        public int ProjectId { get; set; }
        public int CompanyId { get; set; }
        public string ReferenceId { get; set; }
        public int StatusId { get; set; }
        public string Name { get; set; }

        public virtual Company Company { get; set; }
        public virtual Status Status { get; set; }
    }
}
