using System;
using Microsoft.Extensions.Configuration;

namespace DocuItService.Models
{
    public class MyAppSettings
    {
        private readonly IConfiguration MyConfiguration;

        public MyAppSettings(IConfiguration configuration)
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
    }
}
