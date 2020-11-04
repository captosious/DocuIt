using System;
using Microsoft.Extensions.Configuration;

namespace DocuitWeb.Data
{
    public class TestingService
    {
        private readonly IConfiguration MyConfiguration;

        public TestingService(IConfiguration configuration)
        {
            MyConfiguration = configuration;
            SmtpServer = configuration["AppSettings:SmtpServer"];
            SmtpUser = configuration["AppSettings:SmtpUser"];
            SmtpPassword = configuration["AppSettings:SmtpPassword"];
            SecretKey = configuration["AppSettings:SecretKey"];
        }

        public string SmtpServer { get; set; }
        public string SmtpUser { get; set; }
        public string SmtpPassword { get; set; }
        public string SecretKey { get; set; }
        public string CommonText { get; set; }
    }
}