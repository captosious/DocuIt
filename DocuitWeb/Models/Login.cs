using System;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using DocuItService.Models;

namespace DocuItService.Models
{
    public partial class Login
    {
        public  Login()
        {
            
        }
        
        public int CompanyId;
        //public int UserId;
        [Required] public string UserName { get; set; }
        [Required] public string Password { get; set; }

        //public string Name { get; set; }
        //public string FamilyName { get; set; }

        //public int SecurityId { get;  set; }
        //public bool Locked { get; set; }
        //public bool LoggedIn { get; set; }
        //public string Token { get; set; }

        
    }
}