using System;
using System.Collections.Generic;

#nullable disable

namespace DocuItService.Models
{
    public partial class Security
    {
        public Security()
        {
            Users = new HashSet<User>();
        }

        public int SecurityId { get; set; }
        public string Description { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
