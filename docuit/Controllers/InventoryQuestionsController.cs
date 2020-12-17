using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using DocuItService.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace DocuItService.Controllers
{
    [Route("[controller]")]
    [Produces("application/json")]
    [ApiController]

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
        public IEnumerable<InventoryQuestionsTable> GetInventoryQuestionnaire([FromBody] InventoryReport params)
        {
            var questionnaire;

            questionnaire = MyDBContext.Select;

        }
    }
}
