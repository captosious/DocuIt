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
            IEnumerable<ProjectUserSecurity> projectUserSecurity = MyDBContext.ProjectUserSecurities.Where(x=> x.CompanyId == parProjectUserSecurity.CompanyId && x.ProjectId == parProjectUserSecurity.ProjectId);

            if (projectUserSecurity == null)
            {
                return null;
            }
            return projectUserSecurity;
        }

        [HttpGet("GetProjectUsers")]
        public IEnumerable<ProjectUserSecurity> GetProjectUsers([FromBody] ProjectUserSecurity parProjectUserSecurity)
        {
            IEnumerable<User> users = MyDBContext.Users.Where(x => x.CompanyId == parProjectUserSecurity.CompanyId);
            IEnumerable<ProjectUserSecurity> projectUserSecurityGrantedList = MyDBContext.ProjectUserSecurities.Where(x => x.CompanyId == parProjectUserSecurity.CompanyId && x.ProjectId == parProjectUserSecurity.ProjectId).ToList<ProjectUserSecurity>();

            List<ProjectUserSecurity> projectUserSecurities = new List<ProjectUserSecurity>();
            ProjectUserSecurity projectUserSecurity;

            if (users == null)
            {
                return null;
            }
            foreach (User user in users)
            {
                bool found = false;

                foreach (ProjectUserSecurity projectUser in projectUserSecurityGrantedList)
                {
                    if (projectUser.UserId == user.UserId)
                    {
                        found = true;   
                        break;
                    }
                }
                if (!found)
                {
                    projectUserSecurity = new ProjectUserSecurity();

                    projectUserSecurity.CompanyId = user.CompanyId;
                    projectUserSecurity.UserId = user.UserId;
                    projectUserSecurity.FamilyName = user.FamilyName;
                    projectUserSecurity.Name = user.Name;
                    projectUserSecurity.ProjectId = parProjectUserSecurity.ProjectId;
                    projectUserSecurity.Rights = 0;
                    projectUserSecurities.Add(projectUserSecurity);
                }
            }
            return projectUserSecurities;
        }
        
        [HttpGet]
        public ProjectSecurity Get([FromBody] ProjectSecurity ProjectSecurityParameters)
        {
            ProjectSecurity ProjectSecurity = (ProjectSecurity)MyDBContext.ProjectSecurities.FirstOrDefault(d => d.CompanyId == ProjectSecurityParameters.CompanyId && d.ProjectId == ProjectSecurityParameters.ProjectId && d.ProjectId == ProjectSecurityParameters.ProjectId  && d.UserId == ProjectSecurityParameters.UserId );

            if (ProjectSecurity == null)
            {
                return null;
            }
            return ProjectSecurity;
        }

        [HttpGet("GetAll")]
        public IEnumerable<ProjectSecurity> GetAll([FromBody] ProjectSecurity ProjectSecurityParameters)
        {
            IEnumerable <ProjectSecurity> projectSecurityList = MyDBContext.ProjectSecurities.Where(d => d.CompanyId == ProjectSecurityParameters.CompanyId && d.ProjectId == ProjectSecurityParameters.ProjectId && d.ProjectId == ProjectSecurityParameters.ProjectId);

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
            MyDBContext.ProjectSecurities.Add(ProjectSecurityParameters);
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

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] ICollection<ProjectSecurity> projectSecurities)
        {
            Microsoft.EntityFrameworkCore.Storage.IDbContextTransaction transaction;
            ProjectSecurity search = new ProjectSecurity();
            ICollection<ProjectSecurity> users_to_delete;

            if (projectSecurities.Count > 0)
            {
                
                //try {
                    transaction = MyDBContext.Database.BeginTransaction();
                    search = projectSecurities.First();
                    users_to_delete = (ICollection<ProjectSecurity>)MyDBContext.ProjectSecurities.Where(Q => Q.CompanyId == search.CompanyId && Q.ProjectId == search.ProjectId).ToList();
                    MyDBContext.ProjectSecurities.RemoveRange(users_to_delete);
                    await MyDBContext.SaveChangesAsync();
                    MyDBContext.ProjectSecurities.AddRange(projectSecurities);
                    await MyDBContext.SaveChangesAsync();

                    transaction.Commit();
                //}
                //catch {
                //    return null;
                //}
                return Ok();
            }
            return null;
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
            ProjectSecurity = await MyDBContext.ProjectSecurities.FindAsync(CompanyId);
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
            ProjectSecurity ProjectSecurity;

            if (ProjectSecurityToDelete == null)
            {
                return BadRequest("Parameters Object not valid.");
            }
            ProjectSecurity = MyDBContext.ProjectSecurities.FirstOrDefault(d => d.CompanyId == ProjectSecurityToDelete.CompanyId && d.ProjectId == ProjectSecurityToDelete.ProjectId );
            try
            {
                MyDBContext.ProjectSecurities.Remove(ProjectSecurityToDelete);
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