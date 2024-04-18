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
    public class HeaderLanguageUpdateValidation : AbstractValidator<GetHeaderLanguageUpdateDTO>
    {
        public HeaderLanguageUpdateValidation()
        {
            RuleForEach(x => x.Specifications)
                .SetValidator(new HeaderSpecificationLangValidation());
        }
    }

}
