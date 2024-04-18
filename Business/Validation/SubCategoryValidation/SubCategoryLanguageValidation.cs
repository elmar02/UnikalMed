using Entities.DTOs.SubCategoryDTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Validation.SubCategoryValidation
{
    public class SubCategoryLanguageValidation : AbstractValidator<SubCategoryLanguageDTO>
    {
        public SubCategoryLanguageValidation()
        {
            RuleFor(dto => dto.SubCategoryName)
                .NotEmpty().NotNull().WithName("Kateqoriya");

            RuleFor(dto => dto.LangCode)
                .NotEmpty().NotNull();
        }
    }
}
