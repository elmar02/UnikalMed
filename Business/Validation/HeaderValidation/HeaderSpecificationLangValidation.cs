using Entities.DTOs.HeaderDTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Validation.HeaderValidation
{
    public class HeaderSpecificationLangValidation : AbstractValidator<GetHeaderSpecificationLangDTO>
    {
        public HeaderSpecificationLangValidation()
        {
            RuleFor(x => x.Key).NotEmpty().WithName("Açar");
            RuleFor(x => x.Value).NotEmpty().WithName("Dəyər");
        }
    }
}
