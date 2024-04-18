using Entities.DTOs.SubCategoryDTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Validation.SubCategoryValidation
{
    public class UpdateSubCategoryValidation : AbstractValidator<UpdateSubCategoryDTO>
    {
        public UpdateSubCategoryValidation()
        {
            RuleFor(x => x.Languages).NotNull().NotEmpty();
            RuleForEach(dto => dto.Languages).SetValidator(new SubCategoryLanguageValidation());
        }
    }
}
