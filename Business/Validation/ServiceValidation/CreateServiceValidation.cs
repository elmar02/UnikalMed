using Business.Validation.ProductValidation;
using Entities.DTOs.ServiceDTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Validation.ServiceValidation
{
    public class CreateServiceValidation : AbstractValidator<CreateServiceDTO>
    {
        public CreateServiceValidation()
        {
            RuleFor(dto => dto.Languages).NotEmpty().NotNull();
            RuleFor(dto => dto.PhotoUrl).NotNull().NotEmpty();
            RuleForEach(dto => dto.Languages).SetValidator(new ServiceLanguageValidation());
        }
    }
}
