using System;
using System.Collections.Generic;

#nullable disable

namespace DocuItService.Models
{
    public partial class User
    {
        public User()
        {
            ProjectSecurities = new HashSet<ProjectSecurity>();
            Projects = new HashSet<Project>();
        }

        public int CompanyId { get; set; }
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string FamilyName { get; set; }
        public bool Locked { get; set; }
        public int SecurityId { get; set; }
        public byte[] Image { get; set; }
        public DateTime CreationTime { get; set; }

        public virtual Company Company { get; set; }
        public virtual Security Security { get; set; }
        public virtual ICollection<ProjectSecurity> ProjectSecurities { get; set; }
        public virtual ICollection<Project> Projects { get; set; }
    }
}
