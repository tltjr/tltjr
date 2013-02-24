using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using tltjr.Models;
using System.Net;
using System.Net.Mail;

namespace tltjr.Data
{
    public class EmailProvider
    {
        private string _messageFormat = "You have received an email from {0} from the email address {1}.\n\nMessage:\n{2}";
        private string _myEmail = "thomas.thornton.jr@gmail.com";
        public bool TrySendEmail(ContactModel model)
        {            const string fromPassword = "";
            string body = string.Format(_messageFormat, model.Name, model.Email, model.Message);

            var smtp = new SmtpClient("smtp.gmail.com", 587)
                       {
                           Credentials = new NetworkCredential(_myEmail, fromPassword),
                           EnableSsl = true
                       };
            try
            {
                smtp.Send(_myEmail, _myEmail, model.Subject, body);
                return true;
            }
            catch (SmtpException e)
            {
                return false;
            }
        }
    }
}