using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace DocuItService.Models
{
    public partial class Security
    {
        public Security()
        {
            User = new HashSet<User>();
        }

        public int SecurityId { get; set; }
        public string Description { get; set; }

        public virtual ICollection<User> User { get; set; }
    }
}
