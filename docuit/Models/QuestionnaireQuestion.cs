using System;
using System.Collections.Generic;

#nullable disable

namespace DocuItService.Models
{
    public partial class QuestionnaireQuestion
    {
        public int CompanyId { get; set; }
        public string QuestionnaireTypeId { get; set; }
        public int Id { get; set; }
        public string QuestionId { get; set; }
        public int SortIndex { get; set; }
        public string QuestionText { get; set; }
        public int? ParagraphId { get; set; }

        public virtual QuestionnaireType QuestionnaireType { get; set; }
    }
}
