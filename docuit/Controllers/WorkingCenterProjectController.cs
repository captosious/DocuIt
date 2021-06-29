using System.Collections.Generic;
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
    //[Microsoft.AspNetCore.Authorization.Authorize]

    public class WorkingCenterProjectController : ControllerBase
    {
        private readonly DocuItContext MyDBContext;
        private readonly MyAppSettings MySettings;

        public WorkingCenterProjectController(DocuItContext db, MyAppSettings MySettings)
        {
            MyDBContext = db;
            this.MySettings = MySettings;
        }

        // GET: api/values
        [HttpGet("{GetAll}")]
        public IEnumerable<WorkingCenterProject> GetAll([FromBody] WorkingCenterProject objParams)
        {
            IEnumerable<WorkingCenterProject> objReturn = MyDBContext.WorkingCenterProject.Where(x=> x.CompanyId==objParams.CompanyId && x.ProjectId==objParams.ProjectId);

            if (objReturn == null)
            {
                return null;
            }
            return (objReturn);
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromBody] WorkingCenterProject objParams)
        {
            WorkingCenterProject objReturn;

            if (objParams == null)
            { 
                return BadRequest();
            }
            objReturn = await MyDBContext.WorkingCenterProject.FindAsync(objParams.CompanyId,objParams.ProjectId,objParams.Id);
            if (objReturn == null)
            {
                return NotFound();
            }
            return Ok(objReturn);
        }

        // POST api/values (ADD)
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] WorkingCenterProject objParams)
        {
            if (objParams == null)
            {
                return NotFound();
            }
            MyDBContext.WorkingCenterProject.Add(objParams);
            if (ModelState.IsValid)
            {
                await MyDBContext.SaveChangesAsync();
            }
            else
            {
                return BadRequest("Client Object Not Valid.");
            }
            return Ok(objParams.Id);
        }

        // PUT api/values/5 (FULL UPDATE)
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] WorkingCenterProject objParams)
        {
            MyDBContext.Update(objParams);
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
        //[HttpPatch]
        //public async Task<IActionResult> Patch(int CompanyId, [FromBody] JsonPatchDocument<Company> companyToPatch)
        //{
        //    Company company;

        //    if (companyToPatch == null)
        //    {
        //        return BadRequest();
        //    }
        //    if (CompanyId < 1)
        //    {
        //        return BadRequest();
        //    }
        //    company = await MyDBContext.Company.FindAsync(CompanyId);
        //    companyToPatch.ApplyTo(company);
        //    if (ModelState.IsValid)
        //    {
        //        await MyDBContext.SaveChangesAsync();
        //    }
        //    else
        //    {
        //        return BadRequest();
        //    }
        //    return Ok();
        //}

        //[HttpPatch]
        //public async Task<IActionResult> Patch([FromBody] JToken jsonDocument)
        //{
        //    Company company;
        //    JsonPatchDocument<Company> jsonPatchDocument;

        //    IEnumerable MyToken = jsonDocument.First ;

        //    company = (Company)MyToken;

        //    if (jsonDocument == null)
        //    {
        //        return BadRequest();
        //    }
        //    //if (CompanyId < 1)
        //    //{
        //    //    return BadRequest();
        //    //}
        //    //company = await MyDBContext.Company.FindAsync(CompanyId);
        //    //companyToPatch.ApplyTo(company);
        //    //if (ModelState.IsValid)
        //    //{
        //    //    await MyDBContext.SaveChangesAsync();
        //    //}
        //    //else
        //    //{
        //    //    return BadRequest();
        //    //}
        //    return Ok(MyToken);
        //}

        [HttpGet("Delete")]
        public async Task<IActionResult> Delete([FromBody] WorkingCenterProject objParams)
        {
            WorkingCenterProject objReturn;

            if (objParams == null)
            {
                return BadRequest();
            }
            objReturn = await MyDBContext.WorkingCenterProject.FindAsync(objParams.CompanyId, objParams.Id);
            if (objReturn == null)
            {
                return NotFound();
            }
            else
            {
                MyDBContext.WorkingCenterProject.Remove(objReturn);
                await MyDBContext.SaveChangesAsync();
                return Ok();
            }
        }
    }
}