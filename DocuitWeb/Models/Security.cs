using System;
using System.Collections.Generic;

namespace DocuitWeb.Models
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
