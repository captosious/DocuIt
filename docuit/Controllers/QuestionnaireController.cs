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

    public class QuestionnaireController : ControllerBase
    {
        private readonly DocuItContext MyDBContext;
        private readonly MyAppSettings MySettings;

        public QuestionnaireController(DocuItContext db, MyAppSettings MySettings)
        {
            MyDBContext = db;
            this.MySettings = MySettings;
        }

        //[HttpGet("GetQuestionnaire")]
        public IEnumerable<Questionnaire> Get([FromBody] QuestionnaireParams param)
        {
            IEnumerable<Questionnaire> questionnaire;
            //questionnaire = (IEnumerable<QuestionnaireTable>)MyDBContext.QuestionnaireTable.Where(q => q.CompanyId == param.CompanyId && q.QuestionnaireTypeId == param.QuestionnaireTypeId);
            questionnaire = (IEnumerable<Questionnaire>)MyDBContext.Questionnaire.ToList(); ;
            if (questionnaire != null)
            {
                return questionnaire;
            }
            else
            {
                return null;
            }
        }
    }
}
