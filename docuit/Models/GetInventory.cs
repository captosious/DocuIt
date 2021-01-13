using System;
using System.Collections.Generic;

namespace DocuItService.Models
{
    public partial class GetInventory
    {
        public int CompanyId { get; set; }
        public string InventoryTypeId { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public string QuestionId { get; set; }
        public string QuestionText { get; set; }
    }
}
