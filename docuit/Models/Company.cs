using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace DocuItService.Models
{
    public partial class Company
    {
        public Company()
        {
            BuildingType = new HashSet<BuildingType>();
            ElementType = new HashSet<ElementType>();
            Project = new HashSet<Project>();
            QuestionnaireType = new HashSet<QuestionnaireType>();
            User = new HashSet<User>();
            WorkingCenter = new HashSet<WorkingCenter>();
        }

        public int CompanyId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string FiscalId { get; set; }
        public string Address { get; set; }
        public string Town { get; set; }
        public string ZipCode { get; set; }

        public virtual ICollection<BuildingType> BuildingType { get; set; }
        public virtual ICollection<ElementType> ElementType { get; set; }
        public virtual ICollection<Project> Project { get; set; }
        public virtual ICollection<QuestionnaireType> QuestionnaireType { get; set; }
        public virtual ICollection<User> User { get; set; }
        public virtual ICollection<WorkingCenter> WorkingCenter { get; set; }
    }
}
