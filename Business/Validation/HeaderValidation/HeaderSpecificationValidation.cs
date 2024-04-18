using Entities.DTOs.HeaderDTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Validation.HeaderValidation
{
    public class HeaderSpecificationValidation : AbstractValidator<HeaderSpecificationDTO>
    {
        public HeaderSpecificationValidation()
        {
            RuleFor(dto => dto.Key).NotEmpty().NotNull().WithName("Açar");

            RuleFor(dto => dto.Value).NotEmpty().NotNull().WithName("Açarın dəyəri");
        }
    }

}
