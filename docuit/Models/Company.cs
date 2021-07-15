using System;
using System.Collections.Generic;

#nullable disable

namespace DocuItService.Models
{
    public partial class Company
    {
        public Company()
        {
            BuildingTypes = new HashSet<BuildingType>();
            ElementTypes = new HashSet<ElementType>();
            Projects = new HashSet<Project>();
            QuestionnaireTypes = new HashSet<QuestionnaireType>();
            Users = new HashSet<User>();
            WorkingCenters = new HashSet<WorkingCenter>();
        }

        public int CompanyId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string FiscalId { get; set; }
        public string Address { get; set; }
        public string Town { get; set; }
        public string ZipCode { get; set; }

        public virtual ICollection<BuildingType> BuildingTypes { get; set; }
        public virtual ICollection<ElementType> ElementTypes { get; set; }
        public virtual ICollection<Project> Projects { get; set; }
        public virtual ICollection<QuestionnaireType> QuestionnaireTypes { get; set; }
        public virtual ICollection<User> Users { get; set; }
        public virtual ICollection<WorkingCenter> WorkingCenters { get; set; }
    }
}
