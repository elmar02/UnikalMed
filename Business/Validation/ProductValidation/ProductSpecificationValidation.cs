using Entities.DTOs.ProductDTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Validation.ProductValidation
{
    public class ProductSpecificationValidation : AbstractValidator<ProductSpecificationDTO>
    {
        public ProductSpecificationValidation()
        {
            RuleFor(dto => dto.Key).NotEmpty().NotNull().WithName("Açar");

            RuleFor(dto => dto.Value).NotEmpty().NotNull().WithName("Açarın dəyəri");
        }
    }

}
