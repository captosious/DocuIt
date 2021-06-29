using System;
using System.Collections.Generic;

namespace DocuitWeb.Models
{
    public partial class WorkingCenterProject
    {
        public int CompanyId { get; set; }
        public int ProjectId { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }

        public virtual WorkingCenter WorkingCenter { get; set; }
    }
}
