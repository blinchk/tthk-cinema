using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Net;
using System.Net.Mail;

namespace tthk_kinoteater.Models
{
    public class Message
    {
        public const string Sender = "contacts@laus19.thkit.ee";
        public const string Password = "siinSaabTeavitadaKeegi";
        public List<string> Recipients { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }

        public Message(string body)
        {
            Random random = new Random();
            Subject = $"Teie kinopiletid. Ostukorv #{random.Next(1123415, 5000012)}.";
            Body = body;
        }

        public bool Send()
        {
            try
            {
                var message = new MailMessage();
                var smtpClient = new SmtpClient("smtp.zone.eu", 587)
                {
                    Credentials = new NetworkCredential(Sender, Password),
                    EnableSsl = true
                };
                foreach (var recipent in Recipients) message.To.Add(recipent);
                message.From = new MailAddress(Sender, "Kino");
                message.Body = Body;
                message.IsBodyHtml = true;
                message.Subject = Subject;
                smtpClient.Send(message);
            }
            catch (Exception)
            {
                return false;
            }
            
            return true;
        }
    }
}