using Entities.DTOs.CategoryDTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Validation.CategoryValidation
{
    public class CreateCategoryValidation : AbstractValidator<CreateCategoryDTO>
    {
        public CreateCategoryValidation()
        {
            RuleFor(x => x.CategoryNames).NotEmpty().NotNull();
            RuleForEach(dto => dto.CategoryNames).SetValidator(new CategoryLanguageValidation());
        }
    }
}
