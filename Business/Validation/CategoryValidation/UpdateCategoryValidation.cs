using Entities.DTOs.CategoryDTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Validation.CategoryValidation
{
    public class UpdateCategoryValidation : AbstractValidator<UpdateCategoryDTO>
    {
        public UpdateCategoryValidation()
        {
            RuleFor(x=>x.Languages).NotEmpty().NotNull();
            RuleForEach(dto => dto.Languages).SetValidator(new CategoryLanguageValidation());
        }
    }
}
