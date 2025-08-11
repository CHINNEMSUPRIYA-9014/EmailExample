using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace EmailSMTP
{
    public class EmailService : IEmailService
    {
        private readonly EmailSettings _emailSettings;

        public EmailService()
        {
            // Load config here (works in a non-DI console app)
            var config = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appSettings.json", optional: false, reloadOnChange: true)
                .Build();

            _emailSettings = config.GetSection("EmailSettings").Get<EmailSettings>();
        }

        public void Greet()
        {
            Console.WriteLine("Enter To Email: ");
            string toMail = Console.ReadLine() ?? string.Empty;

            Console.WriteLine("Enter Email Subject: ");
            string subject = Console.ReadLine() ?? string.Empty;

            Console.WriteLine("Enter Email Body: ");
            string body = Console.ReadLine() ?? string.Empty;

            try
            {
                SendEmail(toMail, subject, body);
                Console.WriteLine("Email sent successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error sending email: " + ex.Message);
            }
        }

        private void SendEmail(string to, string subject, string body)
        {
            using (var mail = new MailMessage())
            {
                mail.From = new MailAddress(_emailSettings.FromEmail);
                mail.To.Add(to);
                mail.Subject = subject;
                mail.Body = body;
                mail.IsBodyHtml = true;

                using (var smtp = new SmtpClient(_emailSettings.SMTPHost, _emailSettings.SMTPPort))
                {
                    smtp.Credentials = new NetworkCredential(_emailSettings.Username, _emailSettings.Password);
                    smtp.EnableSsl = _emailSettings.EnableSSL;
                    smtp.Send(mail);
                }
            }
        }
    }
}
