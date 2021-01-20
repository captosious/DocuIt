using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace DocuItService.Models
{
    public partial class QuestionnaireTable
    {
        public int CompanyId { get; set; }
        public string QuestionnaireTypeId { get; set; }
        public string QuestionnaireTypeName { get; set; }
        public int ParagraphSortIndex { get; set; }
        public int Id { get; set; }
        public string ParagraphName { get; set; }
        public int QuestionnaireSortIndex { get; set; }
        public string QuestionId { get; set; }
        public string QuestionText { get; set; }
    }
}
