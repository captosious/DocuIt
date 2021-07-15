using System;
using System.Collections.Generic;

#nullable disable

namespace DocuItService.Models
{
    public partial class ProjectUserSecurity
    {
        public int CompanyId { get; set; }
        public long? ProjectId { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public string FamilyName { get; set; }
        public int? Rights { get; set; }
    }
}
