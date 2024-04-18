using Entities.DTOs.SubCategoryDTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Validation.SubCategoryValidation
{
    public class CreateSubCategoryValidation : AbstractValidator<CreateSubCategoryDTO>
    {
        public CreateSubCategoryValidation()
        {
            RuleFor(x => x.Languages).NotEmpty().NotNull();
            RuleForEach(dto => dto.Languages).SetValidator(new SubCategoryLanguageValidation());
        }
    }
}
