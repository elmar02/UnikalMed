using Business.Validation.ProductValidation;
using Entities.DTOs.HeaderDTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Validation.HeaderValidation
{
    public class HeaderLanguageValidation : AbstractValidator<HeaderLangaugeDTO>
    {
        public HeaderLanguageValidation()
        {
            RuleFor(dto => dto.LangCode).NotEmpty().NotNull();
            RuleFor(dto => dto.Specifications).NotEmpty().NotNull();
            RuleForEach(dto => dto.Specifications).SetValidator(new HeaderSpecificationValidation());
        }
    }

}
