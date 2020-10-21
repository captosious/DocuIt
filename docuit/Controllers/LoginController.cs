using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using DocuItService.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace DocuItService.Controllers
{
    [Route("[controller]")]
    [Produces("application/json")]
    [ApiController]

    public class LoginController : ControllerBase
    {
        private readonly DocuItContext MyDBContext;
        private readonly MyAppSettings MySettings;

        public LoginController(DocuItContext db, MyAppSettings MySettings)
        {
            MyDBContext = db;
            this.MySettings = MySettings;
        }

        //[HttpGet("{LogIn}")]
        [HttpGet]
        public Login LogIn([FromBody] Login LoginParameters)
        {
            Login login = new Login(MyDBContext, MySettings);

            if (LoginParameters == null)
            {
                return null;
            }
            login.CompanyId = LoginParameters.CompanyId;
            login.UserName = LoginParameters.UserName;
            login.Password = LoginParameters.Password;
            if (login.CheckUser())
            {
                return login;
            }
            return null;
        }

        [HttpGet("{LogOut}")]
        public bool LogOut([FromBody] Login LoginParameters)
        {
            //Login login = new Login(MyDBContext, MySettings);

            if (LoginParameters == null)
            {
                return false;
            }
            return true;
        }
    }
}