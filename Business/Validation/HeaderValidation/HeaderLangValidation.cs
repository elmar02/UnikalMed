using Entities.DTOs.HeaderDTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Validation.HeaderValidation
{
    public class HeaderLangValidation : AbstractValidator<GetHeaderLangDTO>
    {
        public HeaderLangValidation()
        {
            RuleFor(x => x.WoltValue).GreaterThanOrEqualTo(0).WithName("Voltun qiyməti");
            RuleForEach(x => x.Languages).SetValidator(new HeaderLanguageValidation());
        }
    }
}
