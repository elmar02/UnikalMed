using Business.Validation.HeaderValidation;
using Entities.DTOs.HeaderDTOs;
using Entities.DTOs.ProductDTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Validation.ProductValidation
{
    public class CreateProductValidation : AbstractValidator<CreateProductDTO>
    {
        public CreateProductValidation()
        {
            RuleFor(dto => dto.Languages).NotEmpty().NotNull();

            RuleFor(x=>x.PhotoUrls).NotEmpty().NotNull();

            RuleForEach(dto => dto.Languages).SetValidator(new ProductLanguageValidation());

            RuleForEach(dto => dto.Headers).SetValidator(new HeaderDTOValidation());
        }
    }
}
