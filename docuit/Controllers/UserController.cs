using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DocuItService.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;


namespace DocuItService.Controllers
{
    [Route("[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class UserController : ControllerBase 
    {
        private readonly DocuItContext MyDBContext;
        private readonly MyAppSettings MySettings;

        public UserController(DocuItContext db, MyAppSettings MySettings)
        {
            MyDBContext = db;
            this.MySettings = MySettings;
        }

        // GET: api/values
        [HttpGet("{GetAll}")]
        [Authorize]
        public IEnumerable<User> Get()
        {
            IEnumerable<User> users = MyDBContext.User;

            if (users == null)
            {
                return null;
            }
            return users;
        }

        // GET api/values/5
        [HttpGet]
        public User Get([FromBody] User UserParameters)
        {
            User user =  (User)MyDBContext.User.FirstOrDefault(u=>u.CompanyId ==UserParameters.CompanyId && u.Username ==UserParameters.Username);

            if (user == null)
            {
                return null;
            }
            return user;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] User UserParameters)
        {
            if (UserParameters == null)
            {
                return NotFound();
            }
            MyDBContext.User.Add(UserParameters);
            if (ModelState.IsValid)
            {
                await MyDBContext.SaveChangesAsync();
            }
            else
            {
                return BadRequest("Client Object Not Valid.");
            }
            return Ok(UserParameters.UserId);
        }


        // PUT api/values/5 (FULL UPDATE)
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] User UserParameters)
        {
            MyDBContext.Update(UserParameters);
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
        public async Task<IActionResult> Patch(int CompanyId, [FromBody] JsonPatchDocument<User> userToPatch)
        {
            User user;

            if (userToPatch == null)
            {
                return BadRequest();
            }
            if (CompanyId < 1)
            {
                return BadRequest();
            }
            user = await MyDBContext.User.FindAsync(CompanyId);
            userToPatch.ApplyTo(user);
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
        public async Task<IActionResult> Delete([FromBody] User userParams)
        {
            User user;

            if (userParams == null)
            {
                return BadRequest("Parameters Object not valid.");
            }
            user = MyDBContext.User.FirstOrDefault(u=>u.CompanyId == userParams.CompanyId && u.Username ==userParams.Username);
            try
            {
                MyDBContext.User.Remove(user);
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