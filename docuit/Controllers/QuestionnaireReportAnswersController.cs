using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DocuItService.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage;

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

        [HttpGet]
        public IEnumerable<QuestionnaireReportAnswers> Get([FromBody] QuestionnaireReportAnswers param)
        {
            IEnumerable<QuestionnaireReportAnswers> questionnaire;

            questionnaire = (IEnumerable<QuestionnaireReportAnswers>)MyDBContext.QuestionnaireReportAnswers.Where(e=> e.CompanyId==param.CompanyId && e.ProjectId ==param.ProjectId && e.DossierId ==param.DossierId);
            return questionnaire;
        }

        [HttpPut]
        public async Task<IActionResult> Put ([FromBody] IEnumerable<QuestionnaireReportAnswers> questionnaire)
        {
            IDbContextTransaction transaction = MyDBContext.Database.BeginTransaction();

            try
            {
                MyDBContext.QuestionnaireReportAnswers.RemoveRange(questionnaire);
                await MyDBContext.SaveChangesAsync();
                MyDBContext.QuestionnaireReportAnswers.AddRange(questionnaire);
                await MyDBContext.SaveChangesAsync();
            }
            catch
            {
                transaction.Rollback();
            }
            transaction.Commit();
            return Ok();
        }

        //[HttpPut]
        //public async Task<IActionResult> Put([FromBody] ICollection<Questionnaire> questionnaire)
        //{
        //    IDbContextTransaction transaction = MyDBContext.Database.BeginTransaction();
        //    ICollection<QuestionnaireReportAnswers> questionnaireReport = new List<QuestionnaireReportAnswers>();
        //    QuestionnaireReportAnswers questionnaireReportAnswersquestion;

        //    foreach (Questionnaire question in questionnaire)
        //    {
        //        questionnaireReportAnswersquestion = new QuestionnaireReportAnswers();
        //        questionnaireReportAnswersquestion.CompanyId = question.CompanyId;
        //        questionnaireReportAnswersquestion.DossierId = question.CompanyId;
        //        questionnaireReportAnswersquestion.ProjectId = question.CompanyId;
        //        questionnaireReportAnswersquestion.QuestionnaireReportId = question.QuestionnaireReportId;
        //        questionnaireReportAnswersquestion.QuestionId = question.QuestionId;
        //        questionnaireReportAnswersquestion.Answer = question.Answer;

        //        questionnaireReport.Add(questionnaireReportAnswersquestion);
        //    }

        //    try
        //    {
        //        MyDBContext.QuestionnaireReportAnswers.RemoveRange(questionnaireReport);
        //        await MyDBContext.SaveChangesAsync();
        //        MyDBContext.QuestionnaireReportAnswers.AddRange(questionnaireReport);
        //        await MyDBContext.SaveChangesAsync();
        //    }
        //    catch
        //    {
        //        transaction.Rollback();
        //    }
        //    transaction.Commit();
        //    return Ok();
        //}
    }
}

