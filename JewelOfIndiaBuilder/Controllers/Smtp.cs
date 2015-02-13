using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;


namespace JewelOfIndiaBuilder.Controllers
{
    public static class Smtp
    {
        private static readonly SmtpClient SmtpClient;
        public static readonly MailMessage Message;
        static Smtp()
        {
            Message = new MailMessage();
            SmtpClient = new SmtpClient();
        }


        //TODO If we need to get the host and port from database we need make changes in delivery client to 

        public static void SendMail(string apartmentDesc)
        {

            Message.Attachments.Clear();
            Message.To.Clear();

            SmtpClient.Host = ConfigurationManager.AppSettings["Host"];
            SmtpClient.Port = Convert.ToInt16(ConfigurationManager.AppSettings["Port"]);
            Message.From = new MailAddress(ConfigurationManager.AppSettings["FromAddress"]);
            Message.To.Add(new MailAddress(ConfigurationManager.AppSettings["ToAddress"]));
            Message.Subject = apartmentDesc;
            Message.IsBodyHtml = false;
            Message.Body = "The Aparment is waiting for Approval";
            SmtpClient.Send(Message);
            
        }

        

       
    }
}