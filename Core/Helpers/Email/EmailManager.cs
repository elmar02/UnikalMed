using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete.ErrorResult;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using MailKit.Net.Smtp;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Net;
using Core.Utilities.Results.Concrete.SuccessResult;

namespace Core.Helpers.Email
{
    public class EmailManager : IEmailService
    {
        private readonly IConfiguration _config;

        public EmailManager(IConfiguration config)
        {
            _config = config;
        }

        public bool IsValidEmail(string email)
        {
            if (string.IsNullOrEmpty(email)) return false;
            var pattern = @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$";
            Regex regex = new(pattern);
            return regex.IsMatch(email);
        }

        public async Task<IResult> SendEmailAsync(string userEmail, string userName, string subject, string body, bool isHtml = false)
        {
            try
            {
                string senderEmail = _config["EmailSettings:Email"];
                string senderName = _config["EmailSettings:Name"];
                string senderPassword = _config["EmailSettings:Password"];
                string smtpServer = _config["EmailSettings:SmtpServer"];
                int port = Convert.ToInt32(_config["EmailSettings:Port"]);

                var message = new MimeMessage();
                message.From.Add(new MailboxAddress(senderName, senderEmail));
                message.To.Add(new MailboxAddress(userName, userEmail));
                message.Subject = subject;
                message.Importance = MessageImportance.High;
                message.Body = new TextPart(isHtml ? MimeKit.Text.TextFormat.Html : MimeKit.Text.TextFormat.Text)
                {
                    Text = body
                };

                using (var client = new SmtpClient())
                {
                    await client.ConnectAsync(smtpServer, port, SecureSocketOptions.StartTls);
                    await client.AuthenticateAsync(senderEmail, senderPassword);
                    await client.SendAsync(message);
                    await client.DisconnectAsync(true);
                }

                return new SuccessResult(message: "Email uğurla göndərildi", statusCode: HttpStatusCode.OK);
            }
            catch (Exception)
            {
                return new ErrorResult(message: "Gözlənilməz xəta baş verdi", statusCode: HttpStatusCode.BadRequest);
            }
        }
    }
}
