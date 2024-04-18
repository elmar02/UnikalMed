using Core.Utilities.Results.Abstract;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Helpers.Email
{
    public interface IEmailService
    {
        bool IsValidEmail(string email);

        Task<IResult> SendEmailAsync(string userEmail, string userName, string subject, string body, bool isHtml = false);
    }
}
