using System;
using System.Collections.Generic;

namespace DocuItService.Models
{
    public partial class BuildingTypeProject
    {
        public int CompanyId { get; set; }
        public int ProjectId { get; set; }
        public string Id { get; set; }

        public virtual BuildingType BuildingType { get; set; }
    }
}
