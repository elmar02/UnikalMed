using Entities.DTOs.CategoryDTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Validation.CategoryValidation
{
    public class CategoryLanguageValidation : AbstractValidator<CategoryLanguageDTO>
    {
        public CategoryLanguageValidation()
        {
            RuleFor(dto => dto.CategoryName)
                .NotEmpty().NotNull().WithName("Kateqoriya");

            RuleFor(dto => dto.LangCode)
                .NotEmpty().NotNull();
        }
    }

}
