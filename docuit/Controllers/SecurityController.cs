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
    [Microsoft.AspNetCore.Authorization.Authorize]

    public class SecurityController : ControllerBase
    {
        private readonly DocuItContext MyDBContext;
        private readonly MyAppSettings MySettings;

        public SecurityController(DocuItContext db, MyAppSettings MySettings)
        {
            MyDBContext = db;
            this.MySettings = MySettings;
        }

        // GET: api/values
        public IEnumerable<Security> Get()
        {
            IEnumerable<Security> securities = MyDBContext.Security;

            if (securities == null)
            {
                return null;
            }
            return securities;
        }
    }
}
