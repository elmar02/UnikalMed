using Entities.DTOs.PhoneNumberDTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Validation.SendEmailValidation
{
    public class PhoneNumberValidation : AbstractValidator<PhoneNumberDTO>
    {
        public PhoneNumberValidation()
        {
            RuleFor(x => x.PhoneNumber).NotEmpty().NotNull().Matches(@"^[+\d]+$").WithName("Əlaqə nömrəsi");
        }
    }
}
