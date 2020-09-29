using System;
using System.Collections.Generic;

namespace DocuItService.Models
{
    public partial class Project
    {
        public int ProjectId { get; set; }
        public int CompanyId { get; set; }
        public string ReferenceId { get; set; }
        public int StatusId { get; set; }
        public string Name { get; set; }
        public DateTimeOffset CreationTime { get; set; }

        public virtual Company Company { get; set; }
        public virtual Status Status { get; set; }
    }
}
