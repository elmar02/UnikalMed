using Entities.DTOs.EmailDTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Validation.SendEmailValidation
{
    public class CreditValidation : AbstractValidator<CreditDTO>
    {
        public CreditValidation()
        {
            RuleFor(x => x.PhoneNumber).NotEmpty().NotNull().Matches(@"^[+\d]+$").WithName("Əlaqə nömrəsi");
        }
    }
}
