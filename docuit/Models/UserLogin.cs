using System;
using System.Collections.Generic;

namespace DocuItService.Models
{
    public partial class UserLogin
    {
        public UserLogin()
        {
            
        }

        public int CompanyId { get; set; }
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string FamilyName { get; set; }
        public byte Locked { get; set; }
        public DateTimeOffset CreationTime { get; set; }
        public int SecurityId { get; set; }
    }
}
