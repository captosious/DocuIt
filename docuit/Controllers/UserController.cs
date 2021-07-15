 using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DocuItService.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace DocuItService.Controllers
{
    [Route("[controller]")]
    [Produces("application/json")]
    [Consumes("application/json")]
    [ApiController]
    //[Microsoft.AspNetCore.Authorization.Authorize]

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
        [HttpGet("GetAll")]
        public IEnumerable<User> GetAll([FromBody] User UserParameters)
        {
            IEnumerable<User> users = MyDBContext.Users.Where<User>(x=> x.CompanyId == UserParameters.CompanyId);

            if (users == null)
            {
                return null;
            }
            return users;
        }

        [HttpGet("GetById")]
        public User Get([FromBody] User UserParameters)
        {
            User user =  (User)MyDBContext.Users.FirstOrDefault(u=>u.CompanyId ==UserParameters.CompanyId && u.UserId ==UserParameters.UserId);

            if (user == null)
            {
                return null;
            }
            return user;
        }

        [HttpGet("GetByUserName")]
        public User GetByUserName([FromBody] User UserParameters)
        {
            User user = (User)MyDBContext.Users.FirstOrDefault(u => u.CompanyId == UserParameters.CompanyId && u.Username == UserParameters.Username);

            if (user == null)
            {
                return null;
            }
            return user;
        }

        [HttpGet("GetAvatarByUserId")]
        public string GetAvatar([FromBody] User UserParameters)
        {
            User user = (User)MyDBContext.Users.FirstOrDefault(u => u.CompanyId == UserParameters.CompanyId && u.UserId == UserParameters.UserId);

            if (user == null)
            {
                return null;
            }
            return "";
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] User UserParameters)
        {
            if (UserParameters == null)
            {
                return NotFound();
            }
            MyDBContext.Users.Add(UserParameters);
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

        // PUT api/values/5 (FULL UPDATE)
        [HttpGet("{StorePhoto}")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> SetPhoto([FromForm] IFormFile image, [FromForm] int UserId, [FromForm] int CompanyId)
        {
            MemoryStream memoryStream = new MemoryStream();
            User user = new User();
            //int CompanyId, UserId;

            image.CopyTo(memoryStream);
            //CompanyId = int.Parse(Request.Form["CompanyId"]);
            //UserId = int.Parse(Request.Form["UserId"]);
            
            user = MyDBContext.Users.Find(CompanyId, UserId);

            if (user == null)
            {
                return BadRequest();
            }

            user.Image = memoryStream.ToArray();

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

        // PUT api/values/5 (FULL UPDATE)
        [HttpPost("{SetAvatar}")]
        public async Task<IActionResult> SetAvatar([FromForm] IFormFile image)
        {
            MemoryStream memoryStream = new MemoryStream();
            User user = new User();
            int CompanyId, UserId;

            image.CopyTo(memoryStream);
            CompanyId = int.Parse(Request.Form["CompanyId"]);
            UserId = int.Parse(Request.Form["UserId"]);
            user = MyDBContext.Users.Find(CompanyId, UserId);

            if (user == null)
            {
                return BadRequest();
            }

            user.Image = memoryStream.ToArray();

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
            user = await MyDBContext.Users.FindAsync(CompanyId);
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
            user = MyDBContext.Users.FirstOrDefault(u=>u.CompanyId == userParams.CompanyId && u.Username ==userParams.Username);
            try
            {
                MyDBContext.Users.Remove(user);
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