using System;
namespace DocuItService.Models
{
    public class InventoryQuestionsTable
    {
        public InventoryQuestionsTable()
        {

        }

        public int CompanyId { get; set; }
        public string InventoryTypeId { get; set; }
        public string InventoryName { get; set;}
        public int Id { get; set; }
        public int SortIndex { get; set; }
        public string QuestionId { get; set; }
        public string QuestionText { get; set; }
        public int ParagraphId { get; set; }
        public string ParagraphName { get; set; }
    }
}
