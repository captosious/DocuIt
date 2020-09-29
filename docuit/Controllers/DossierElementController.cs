using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using DocuItService.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace DocuItService.Controllers
{
    [Route("[controller]")]
    [Produces("application/json")]
    [ApiController]

    public class DossierElementController : ControllerBase
    {
        private readonly DocuItContext MyDBContext;
        private readonly MyAppSettings MySettings;

        public DossierElementController(DocuItContext db, MyAppSettings MySettings)
        {
            MyDBContext = db;
            this.MySettings = MySettings;
        }

        // GET: api/values
        [HttpGet("{GetAll}")]
        public IEnumerable<DossierElement> Get([FromBody] Dossier dossier)
        {
            IEnumerable<DossierElement> dossiers = MyDBContext.DossierElement.Where (x=> x.CompanyId == dossier.CompanyId && x.ProjectId == dossier.ProjectId && x.DossierId == dossier.DossierId);

            if (dossiers == null)
            {
                return null;
            }
            return dossiers;
        }

        // GET api/values/5
        [HttpGet]
        public DossierElement Get([FromBody] DossierElement dossierElementParameters)
        {
            DossierElement dossierElement = (DossierElement)MyDBContext.DossierElement.FirstOrDefault(d => d.CompanyId == dossierElementParameters.CompanyId && d.ProjectId == dossierElementParameters.ProjectId && d.DossierId == dossierElementParameters.DossierId && d.ElementId == dossierElementParameters.ElementId);

            if (dossierElement == null)
            {
                return null;
            }
            return dossierElement;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] DossierElement dossierElementParameters)
        {
            if (dossierElementParameters == null)
            {
                return NotFound();
            }
            MyDBContext.DossierElement.Add(dossierElementParameters);
            if (ModelState.IsValid)
            {
                await MyDBContext.SaveChangesAsync();
            }
            else
            {
                return BadRequest("Client Object Not Valid.");
            }
            return Ok(dossierElementParameters.DossierId);
        }

        // PUT api/values/5 (FULL UPDATE)
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] Dossier dossierParameters)
        {
            MyDBContext.Update(dossierParameters);
            if (ModelState.IsValid)
            {
                await MyDBContext.SaveChangesAsync();
            }
            else
            {
                return BadRequest();
            }
            return Ok();
        }

        // PATCH api/values (PARTIAL UPDATE)
        [HttpPatch]
        public async Task<IActionResult> Patch(int CompanyId, [FromBody] JsonPatchDocument<DossierElement> dossierElementToPatch)
        {
            DossierElement dossierElement;

            if (dossierElementToPatch == null)
            {
                return BadRequest();
            }
            if (CompanyId < 1)
            {
                return BadRequest();
            }
            dossierElement = await MyDBContext.DossierElement.FindAsync(CompanyId);
            dossierElementToPatch.ApplyTo(dossierElement);
            if (ModelState.IsValid)
            {
                await MyDBContext.SaveChangesAsync();
            }
            else
            {
                return BadRequest();
            }
            return Ok();
        }

        // DELETE api/values/5
        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] DossierElement dossierElementToDelete)
        {
            DossierElement dossierElement;

            if (dossierElementToDelete == null)
            {
                return BadRequest("Parameters Object not valid.");
            }
            dossierElement = MyDBContext.DossierElement.FirstOrDefault(d => d.CompanyId == dossierElementToDelete.CompanyId && d.ProjectId == dossierElementToDelete.ProjectId && d.DossierId == dossierElementToDelete.DossierId && d.ElementId== dossierElementToDelete.ElementId);
            try
            {
                MyDBContext.DossierElement.Remove(dossierElement);
                await MyDBContext.SaveChangesAsync();
            }
            catch (Microsoft.EntityFrameworkCore.DbUpdateConcurrencyException)
            {
                return BadRequest("UserId not valid.");
            }
            return Ok();
        }
    }
}