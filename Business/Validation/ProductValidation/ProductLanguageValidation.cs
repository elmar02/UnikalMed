using Entities.DTOs.ProductDTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Validation.ProductValidation
{
    public class ProductLanguageValidation : AbstractValidator<ProductLanguageDTO>
    {
        public ProductLanguageValidation()
        {
            RuleFor(dto => dto.LangCode).NotEmpty().NotNull();

            RuleForEach(dto => dto.Specifications).SetValidator(new ProductSpecificationValidation());
        }
    }

}
