using System.Collections.Generic;
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

    public class CompanyController : ControllerBase
    {
        private readonly DocuItContext MyDBContext;
        private readonly MyAppSettings MySettings;

        public CompanyController(DocuItContext db, MyAppSettings MySettings)
        {
            MyDBContext = db;
            this.MySettings = MySettings;
        }

        // GET: api/values
        [HttpGet("{GetAll}")]
        public IEnumerable<Company> Get()
        {
            IEnumerable<Company> companies = MyDBContext.Company;

            if (companies == null)
            {
                return null;
            }
            return (companies);
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromBody] Company CompanyParams)
        {
            Company company;

            if (CompanyParams == null)
            {
                return BadRequest();
            }
            company = await MyDBContext.Company.FindAsync(CompanyParams.CompanyId);
            if (company == null)
            {
                return NotFound();
            }
            return Ok(company);
        }

        // POST api/values (ADD)
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Company company)
        {
            if (company == null)
            {
                return NotFound();
            }
            MyDBContext.Company.Add(company);
            if (ModelState.IsValid)
            {
                await MyDBContext.SaveChangesAsync();
            }
            else
            {
                return BadRequest("Client Object Not Valid.");
            }
            return Ok(company.CompanyId);
        }

        // PUT api/values/5 (FULL UPDATE)
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] Company company)
        {
            MyDBContext.Update(company);
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
        public async Task<IActionResult> Patch(int CompanyId, [FromBody] JsonPatchDocument<Company> companyToPatch)
        {
            Company company;

            if (companyToPatch == null)
            {
                return BadRequest();
            }
            if (CompanyId < 1)
            {
                return BadRequest();
            }
            company = await MyDBContext.Company.FindAsync(CompanyId);
            companyToPatch.ApplyTo(company);
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

        // DELETE api/values/5
        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] Company CompanyParams)
        {
            Company company;

            if (CompanyParams == null)
            {
                return BadRequest("Parameters Object not valid.");
            }
            company = await MyDBContext.Company.FindAsync(CompanyParams.CompanyId);
            try
            {
                MyDBContext.Company.Remove(company);
                await MyDBContext.SaveChangesAsync();
            }
            catch (Microsoft.EntityFrameworkCore.DbUpdateConcurrencyException)
            {
                return BadRequest("CompanyID not valid.");
            }
            return Ok();
        }
    }
}