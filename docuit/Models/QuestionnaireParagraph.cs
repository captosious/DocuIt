using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace DocuItService.Models
{
    public partial class QuestionnaireParagraph
    {
        public int CompanyId { get; set; }
        public string QuestionnaireTypeId { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public int SortIndex { get; set; }

        public virtual QuestionnaireType QuestionnaireType { get; set; }
    }
}
