using System;
using System.Collections.Generic;

namespace DocuItService.Models
{
    public partial class BuildingType
    {
        public BuildingType()
        {
            BuildingTypeProject = new HashSet<BuildingTypeProject>();
        }

        public int CompanyId { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }

        public virtual Company Company { get; set; }
        public virtual ICollection<BuildingTypeProject> BuildingTypeProject { get; set; }
    }
}
