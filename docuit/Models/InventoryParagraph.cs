using System;
using System.Collections.Generic;

namespace DocuItService.Models
{
    public partial class InventoryParagraph
    {
        public int CompanyId { get; set; }
        public string InventoryTypeId { get; set; }
        public string Name { get; set; }
        public int SortId { get; set; }
        public int Id { get; set; }

        public virtual InventoryType InventoryType { get; set; }
    }
}
