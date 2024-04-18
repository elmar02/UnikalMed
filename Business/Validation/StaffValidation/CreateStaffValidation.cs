using Entities.DTOs.StaffDTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Validation.StaffValidation
{
    public class CreateStaffValidation : AbstractValidator<CreateStaffDTO>
    {
        public CreateStaffValidation()
        {
            RuleFor(x=>x.PhotoUrl).NotEmpty().NotNull();
            RuleForEach(x=>x.Languages).SetValidator(new StaffLanguageValidation());
        }
    }
}
