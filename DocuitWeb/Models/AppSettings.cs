using System;
using Microsoft.Extensions.Configuration;

namespace DocuitWeb.Models
{
    public class AppSettings
    {
        private readonly IConfiguration MyConfiguration;

        public AppSettings(IConfiguration configuration)
        {
            MyConfiguration = configuration;
            DocuItServiceServer = MyConfiguration["AppSettings:DocuItServiceServer"];
            SMTPServer = MyConfiguration["AppSettings:SMTPServer"];
            SMTPUsername = MyConfiguration["AppSettings:SMTPUsername"];
            SMTPPassword = MyConfiguration["AppSettings:SMTPPassword"];
            CompanyId = 1;//int.Parse ( MyConfiguration["AppSettings:CompanyId"]);
            UserId = 1;
            Username = "pere";
        }

        public string DocuItServiceServer { get; set; }
        public string SMTPServer { get; set; }
        public string SMTPUsername { get; set; }
        public string SMTPPassword { get; set; }

        public int CompanyId { get; set; }
        public int UserId { get; set; }
        public string Username { get; set; }

    }
}