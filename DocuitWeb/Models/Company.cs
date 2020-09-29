using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace DocuitWeb.Models
{
    public partial class Company
    {
        public Company()
        {
            Project = new HashSet<Project>();
            User = new HashSet<User>();
        }

        public int CompanyId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string FiscalId { get; set; }
        public string Address { get; set; }
        public string Town { get; set; }
        public string ZipCode { get; set; }

        public virtual ICollection<Project> Project { get; set; }
        public virtual ICollection<User> User { get; set; }

    }
}
