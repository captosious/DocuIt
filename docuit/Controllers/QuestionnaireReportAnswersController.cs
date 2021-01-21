using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DocuItService.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace DocuItService.Controllers
{
    [Route("[controller]")]
    [Produces("application/json")]
    [ApiController]
    //[Microsoft.AspNetCore.Authorization.Authorize]

    public class QuestionnaireReportAnswersController : ControllerBase
    {
        private readonly DocuItContext MyDBContext;
        private readonly MyAppSettings MySettings;

        public QuestionnaireReportAnswersController(DocuItContext db, MyAppSettings MySettings)
        {
            MyDBContext = db;
            this.MySettings = MySettings;
        }

        //[HttpGet("GetQuestionnaire")]
        public IEnumerable<QuestionnaireReportAnswers> Get([FromBody] QuestionnaireParams param)
        {
            IEnumerable<QuestionnaireReportAnswers> questionnaire;

            questionnaire = (IEnumerable<QuestionnaireReportAnswers>)MyDBContext.Questionnaire.ToList(); ;
            return questionnaire;
        }

        [HttpPut]
        public async Task<IActionResult> Put ([FromBody] IEnumerable<QuestionnaireReportAnswers> questionnaire)
        {
            QuestionnaireReportAnswers question_db;

            foreach (QuestionnaireReportAnswers question in questionnaire)
            {
                question_db = MyDBContext.QuestionnaireReportAnswers.Find(question.CompanyId , question.ProjectId, question.DossierId, question.QuestionnaireReportId, question.QuestionId  );
                //MyDBContext.QuestionnaireReportAnswers.Attach(question);

                MyDBContext.QuestionnaireReportAnswers.Add(question);
                await MyDBContext.SaveChangesAsync();

                MyDBContext.QuestionnaireReportAnswers.Remove(question);
                //MyDBContext.QuestionnaireReportAnswers.AddOrUpdaet
            }

            //questionnaire

            return Ok();
        }
    }
}

