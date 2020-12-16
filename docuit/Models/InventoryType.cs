using System;
using System.Collections.Generic;

namespace DocuItService.Models
{
    public partial class InventoryType
    {
        public InventoryType()
        {
            InventoryParagraph = new HashSet<InventoryParagraph>();
            InventoryQuestions = new HashSet<InventoryQuestions>();
        }

        public int CompanyId { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }

        public virtual Company Company { get; set; }
        public virtual ICollection<InventoryParagraph> InventoryParagraph { get; set; }
        public virtual ICollection<InventoryQuestions> InventoryQuestions { get; set; }
    }
}
