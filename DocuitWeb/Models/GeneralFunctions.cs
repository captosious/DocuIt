using System;
namespace DocuitWeb.Models
{
    public class GeneralFunctions
    {
        public GeneralFunctions()
        {
        }

        public bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }

    
}
