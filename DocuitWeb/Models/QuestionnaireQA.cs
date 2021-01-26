using System;

namespace DocuitWeb.Models

{
    public partial class QuestionnaireQA
    {
        public int CompanyId { get; set; }
        public int ProjectId { get; set; }
        public int DossierId { get; set; }
        public string QuestionnaireReportId { get; set; }
        public string QuestionnaireTypeId { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public string QuestionId { get; set; }
        public string QuestionText { get; set; }
        public string Answer { get; set; }
    }
}
