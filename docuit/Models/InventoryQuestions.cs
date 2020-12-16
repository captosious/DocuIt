using System;
using System.Collections.Generic;

namespace DocuItService.Models
{
    public partial class InventoryQuestions
    {
        public int CompanyId { get; set; }
        public string InventoryTypeId { get; set; }
        public int Id { get; set; }
        public int SortIndex { get; set; }
        public string QuestionId { get; set; }
        public string QuestionText { get; set; }
        public int? ParagraphId { get; set; }

        public virtual InventoryType InventoryType { get; set; }
    }
}
