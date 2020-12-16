using System;
using System.Collections.Generic;

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

        public virtual InventoryReport InventoryReport { get; set; }
    }
}
