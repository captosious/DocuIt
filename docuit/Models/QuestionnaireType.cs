using System;
using System.Collections.Generic;

#nullable disable

namespace DocuItService.Models
{
    public partial class QuestionnaireType
    {
        public QuestionnaireType()
        {
            QuestionnaireParagraphs = new HashSet<QuestionnaireParagraph>();
            QuestionnaireQuestions = new HashSet<QuestionnaireQuestion>();
        }

        public int CompanyId { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }

        public virtual Company Company { get; set; }
        public virtual ICollection<QuestionnaireParagraph> QuestionnaireParagraphs { get; set; }
        public virtual ICollection<QuestionnaireQuestion> QuestionnaireQuestions { get; set; }
    }
}
