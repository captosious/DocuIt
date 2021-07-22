using System;
using System.Collections.Generic;

#nullable disable

namespace DocuItService.Models
{
    public partial class ProjectSecurity
    {
        public int CompanyId { get; set; }
        public int ProjectId { get; set; }
        public int UserId { get; set; }
        public int Rights { get; set; }

        public virtual User User { get; set; }
    }
}
