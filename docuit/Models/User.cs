using System;
using System.Collections.Generic;

namespace DocuItService.Models
{
    public partial class User
    {
        public User()
        {
            ProjectSecurity = new HashSet<ProjectSecurity>();
        }

        public int CompanyId { get; set; }
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string FamilyName { get; set; }
        public bool Locked { get; set; }
        public DateTimeOffset CreationTime { get; set; }
        public int SecurityId { get; set; }

        public virtual Company Company { get; set; }
        public virtual Security Security { get; set; }
        public virtual ICollection<ProjectSecurity> ProjectSecurity { get; set; }
    }
}
