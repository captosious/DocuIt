using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace DocuItService.Models
{
    public partial class Pictures
    {
        public int CompanyId { get; set; }
        public int ProjectId { get; set; }
        public int DossierId { get; set; }
        public string ReportId { get; set; }
        public string PictureId { get; set; }
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }
        public decimal? Orientation { get; set; }
        public decimal? Angle { get; set; }
        public int? Height { get; set; }
        public string Description { get; set; }
        public string Comment { get; set; }

        public virtual QuestionnaireReport QuestionnaireReport { get; set; }
    }
}
