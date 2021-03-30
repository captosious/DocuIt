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
    //[Microsoft.AspNetCore.Authorization.Authorize]

    public class ProjectSecurityController : ControllerBase
    {
        private readonly DocuItContext MyDBContext;
        private readonly MyAppSettings MySettings;

        public ProjectSecurityController(DocuItContext db, MyAppSettings MySettings)
        {
            MyDBContext = db;
            this.MySettings = MySettings;
        }

        // GET: api/values
        [HttpGet("GetProjectUserSecurity")]
        public IEnumerable<ProjectUserSecurity> GetProjectUserSecurity([FromBody] ProjectUserSecurity parProjectUserSecurity)
        {
            IEnumerable<ProjectUserSecurity> projectUserSecurity = MyDBContext.ProjectUserSecurity.Where(x=> x.CompanyId == parProjectUserSecurity.CompanyId && x.ProjectId == parProjectUserSecurity.ProjectId);

            if (projectUserSecurity == null)
            {
                return null;
            }
            return projectUserSecurity;
        }

        //// GET: api/values
        //[HttpGet]
        //public IEnumerable<ProjectSecurity> Get()
        //{
        //    IEnumerable<ProjectSecurity> project = MyDBContext.ProjectSecurity ;

        //    if (project == null)
        //    {
        //        return null;
        //    }
        //    return project;
        //}

        // GET api/values/5
        [HttpGet]
        public ProjectSecurity Get([FromBody] ProjectSecurity ProjectSecurityParameters)
        {
            ProjectSecurity ProjectSecurity = (ProjectSecurity)MyDBContext.ProjectSecurity.FirstOrDefault(d => d.CompanyId == ProjectSecurityParameters.CompanyId && d.ProjectId == ProjectSecurityParameters.ProjectId && d.ProjectId == ProjectSecurityParameters.ProjectId  && d.UserId == ProjectSecurityParameters.UserId );

            if (ProjectSecurity == null)
            {
                return null;
            }
            return ProjectSecurity;
        }

        [HttpGet("GetAll")]
        public IEnumerable<ProjectSecurity> GetAll([FromBody] ProjectSecurity ProjectSecurityParameters)
        {
            IEnumerable <ProjectSecurity> projectSecurityList = MyDBContext.ProjectSecurity.Where(d => d.CompanyId == ProjectSecurityParameters.CompanyId && d.ProjectId == ProjectSecurityParameters.ProjectId && d.ProjectId == ProjectSecurityParameters.ProjectId);

            if (projectSecurityList == null)
            {
                return null;
            }
            return projectSecurityList;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ProjectSecurity ProjectSecurityParameters)
        {
            if (ProjectSecurityParameters == null)
            {
                return NotFound();
            }
            MyDBContext.ProjectSecurity.Add(ProjectSecurityParameters);
            if (ModelState.IsValid)
            {
                await MyDBContext.SaveChangesAsync();
            }
            else
            {
                return BadRequest("Client Object Not Valid.");
            }
            return Ok(ProjectSecurityParameters.UserId );
        }

        // PUT api/values/5 (FULL UPDATE)
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] ProjectSecurity ProjectSecurityParameters)
        {
            MyDBContext.Update(ProjectSecurityParameters);
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
        public async Task<IActionResult> Patch(int CompanyId, [FromBody] JsonPatchDocument<ProjectSecurity> ProjectSecurityParametersToPatch)
        {
            ProjectSecurity ProjectSecurity;

            if (ProjectSecurityParametersToPatch == null)
            {
                return BadRequest();
            }
            if (CompanyId < 1)
            {
                return BadRequest();
            }
            ProjectSecurity = await MyDBContext.ProjectSecurity.FindAsync(CompanyId);
            ProjectSecurityParametersToPatch.ApplyTo(ProjectSecurity);
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
        public async Task<IActionResult> Delete([FromBody] ProjectSecurity ProjectSecurityToDelete)
        {
            //ProjectSecurity ProjectSecurity;

            if (ProjectSecurityToDelete == null)
            {
                return BadRequest("Parameters Object not valid.");
            }
            //ProjectSecurity = MyDBContext.ProjectSecurity.FirstOrDefault(d => d.CompanyId == ProjectSecurityToDelete.CompanyId && d.ProjectId == dossierElementToDelete.ProjectId && d.DossierId == dossierElementToDelete.DossierId && d.ElementId == dossierElementToDelete.ElementId);
            try
            {
                MyDBContext.ProjectSecurity.Remove(ProjectSecurityToDelete);
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