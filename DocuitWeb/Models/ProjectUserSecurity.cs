using System;
using System.Collections.Generic;

namespace DocuitWeb.Models

{
    public partial class ProjectUserSecurity
    {
        public int CompanyId { get; set; }
        public int ProjectId { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public string FamilyName { get; set; }
        public int Rights { get; set; }
    }
}
