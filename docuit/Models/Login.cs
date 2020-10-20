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
        private readonly DocuItContext MyDBContext;
        private readonly MyAppSettings MySettings;

        public  Login(DocuItContext db, MyAppSettings MySettings)
        {
            MyDBContext = db;
            this.MySettings = MySettings;
        }
        
        public int CompanyId;
        public int UserId;
        [Required] public string UserName { get; set; }
        [Required] public string Password { get; set; }

        public string Name { get; set; }
        public string FamilyName { get; set; }

        public int SecurityId { get;  set; }
        public bool Locked { get; set; }
        public bool LoggedIn { get; set; }
        public string Token { get; set; }

        private User user;

        public bool CheckUser()
        {
            user  = (User)MyDBContext.User.FirstOrDefault(u=>u.CompanyId==CompanyId && u.Username==UserName && u.Password == Password);

            if (user==null)
            {
                return false;
            }
            else
            {
                UserId = user.UserId;
                UserName = user.Username;
                Name = user.Name;
                FamilyName = user.FamilyName;
                SecurityId = user.SecurityId;
                Locked = user.Locked;
                LoggedIn = true;
                Token = GetToken();
            }
            return true;
        }

        private string GetToken()
        {
            // Leemos el secret_key desde nuestro appseting
            var key = Encoding.ASCII.GetBytes(MySettings.SecretKey);
            // Creamos los claims (pertenencias, características) del usuario
            var claims = new[]
            {
                    new Claim(ClaimTypes.NameIdentifier, user.Username),
                    new Claim(ClaimTypes.Email, user.Email)
                };
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                // Nuestro token va a durar un día
                Expires = System.DateTime.UtcNow.AddDays(1),
                // Credenciales para generar el token usando nuestro secretykey y el algoritmo hash 256
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var createdToken = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(createdToken);
        }
    }
}