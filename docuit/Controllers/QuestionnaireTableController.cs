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

    public class QuestionnaireTableController : ControllerBase
    {
        private readonly DocuItContext MyDBContext;
        private readonly MyAppSettings MySettings;

        public QuestionnaireTableController(DocuItContext db, MyAppSettings MySettings)
        {
            MyDBContext = db;
            this.MySettings = MySettings;
        }

        [HttpGet("GetQuestionnaire")]
        public IEnumerable<QuestionnaireTable> Get([FromBody] QuestionnaireParams param)
        {
            IEnumerable<QuestionnaireTable> questionnaire;
            //questionnaire = (IEnumerable<QuestionnaireTable>)MyDBContext.QuestionnaireTable.Where(q => q.CompanyId == param.CompanyId && q.QuestionnaireTypeId == param.QuestionnaireTypeId);
            questionnaire = (IEnumerable<QuestionnaireTable>)MyDBContext.QuestionnaireTable.ToList(); ;
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
