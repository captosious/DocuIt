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
    [Microsoft.AspNetCore.Authorization.Authorize]

    public class ProjectController : ControllerBase 
    {
        private readonly DocuItContext MyDBContext;
        private readonly MyAppSettings MySettings;

        public ProjectController(DocuItContext db, MyAppSettings MySettings)
        {
            MyDBContext = db;
            this.MySettings = MySettings;
        }

        // GET: api/values
        [HttpGet("{GetAll}")]
        public IEnumerable<Project> GetAll([FromBody] Project projectParameters)
        {
            IEnumerable<Project> projects = MyDBContext.Project.Where(x => x.CompanyId == projectParameters.CompanyId);

            if (projects == null)
            {
                return null;
            }
            return projects;
        }

        // GET api/values/5
        [HttpGet]
        public Project Get([FromBody] Project projectParameters)
        {
            Project project = (Project)MyDBContext.Project.FirstOrDefault(p => p.CompanyId == projectParameters.CompanyId && p.ProjectId  == projectParameters.ProjectId);

            if (project == null)
            {
                return null;
            }
            return project;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Project projectParameters)
        {
            if (projectParameters == null)
            {
                return NotFound();
            }
            MyDBContext.Project.Add(projectParameters);
            if (ModelState.IsValid)
            {
                await MyDBContext.SaveChangesAsync();
            }
            else
            {
                return BadRequest("Client Object Not Valid.");
            }
            return Ok(projectParameters.ProjectId );
        }

        // PUT api/values/5 (FULL UPDATE)
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] Project projectParameters)
        {
            MyDBContext.Update(projectParameters);
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
        public async Task<IActionResult> Patch(int CompanyId, [FromBody] JsonPatchDocument<Project> projectToPatch)
        {
            Project project;

            if (projectToPatch == null)
            {
                return BadRequest();
            }
            if (CompanyId < 1)
            {
                return BadRequest();
            }
            project = await MyDBContext.Project.FindAsync(CompanyId);
            projectToPatch.ApplyTo(project);
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
        public async Task<IActionResult> Delete([FromBody] Project projectToPatch)
        {
            Project project;

            if (projectToPatch == null)
            {
                return BadRequest("Parameters Object not valid.");
            }
            project = MyDBContext.Project.FirstOrDefault(p => p.CompanyId == projectToPatch.CompanyId && p.ProjectId == projectToPatch.ProjectId);
            try
            {
                MyDBContext.Project.Remove(project);
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
