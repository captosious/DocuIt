using System;
using Microsoft.Extensions.Configuration;

namespace DocuitWeb.Data
{
    public class TestingService
    {
        public TestingService()
        {
            
        }

        public int CompanyId;
        public int UserId;
        public string UserName { get; set; }
        public string Password { get; set; }

        public string Name { get; set; }
        public string FamilyName { get; set; }

        public int SecurityId { get; set; }
        public bool Locked { get; set; }
        public bool LoggedIn { get; set; }
        public string Token { get; set; }
    }
}