using System;
using System.Collections.Generic;

namespace DocuitWeb.Models
{
    public partial class BuildingType :ICloneable
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

        public object Clone()
        {
            BuildingType buildingType = new BuildingType();

            buildingType.CompanyId = this.CompanyId;
            buildingType.Id = this.Id;
            buildingType.Name = this.Name;

            buildingType.Company = this.Company;
            buildingType.BuildingTypeProject = this.BuildingTypeProject;

            return buildingType;
        }
    }
}
