using Entities.DTOs.EmailDTOs;
using FluentValidation;
using Org.BouncyCastle.Math.EC.Rfc7748;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Validation.SendEmailValidation
{
    public class ContactValidation : AbstractValidator<ContactDTO>
    {
        public ContactValidation()
        {
            RuleFor(x=>x.EmailAddress).NotEmpty().NotNull().EmailAddress().WithName("Elektron Poçt");
            RuleFor(x=>x.Message).NotEmpty().NotNull().WithName("Mesaj");
            RuleFor(x=>x.PhoneNumber).NotEmpty().NotNull().Matches(@"^[+\d]+$").WithName("Əlaqə nömrəsi");
            RuleFor(x => x.Subject).NotEmpty().NotNull().WithName("Mövzu");
            RuleFor(x => x.FullName).NotEmpty().NotNull().WithName("Ad Soyad");
        }
    }
}
