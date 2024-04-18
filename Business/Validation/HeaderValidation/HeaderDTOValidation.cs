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
    public class HeaderDTOValidation : AbstractValidator<HeaderDTO>
    {
        public HeaderDTOValidation()
        {
            RuleFor(dto => dto.Languages).NotEmpty().NotNull();
            RuleFor(dto => dto.PhotoUrl).NotNull().NotEmpty();
            RuleForEach(dto => dto.Languages).SetValidator(new HeaderLanguageValidation());
        }
    }

}
