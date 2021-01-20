using System;
namespace DocuitWeb.Models
{
    public partial class Questionnaire
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
