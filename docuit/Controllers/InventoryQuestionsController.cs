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

    public class InventoryQuestionsController : ControllerBase
    {
        private readonly DocuItContext MyDBContext;
        private readonly MyAppSettings MySettings;

        public InventoryQuestionsController(DocuItContext db, MyAppSettings MySettings)
        {
            MyDBContext = db;
            this.MySettings = MySettings;
        }

        [HttpGet("GetInventoryQuestionnaire")]
        public IEnumerable<Questionnairetable> Get([FromBody] InventoryParams param)
        {
            IEnumerable<GetInventory> questionnaire;

            questionnaire = (IEnumerable<GetInventory>)MyDBContext.GetInventory.Where(q => q.CompanyId == param.CompanyId && q.InventoryTypeId == param.TypeId);

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
