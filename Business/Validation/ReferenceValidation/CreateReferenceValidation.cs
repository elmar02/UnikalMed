using Business.Validation.StaffValidation;
using Entities.DTOs.ReferenceDTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Validation.ReferenceValidation
{
    public class CreateReferenceValidation : AbstractValidator<CreateReferenceDTO>
    {
        public CreateReferenceValidation()
        {
            RuleFor(x=>x.PhotoUrl).NotEmpty().NotNull();
            RuleFor(x=>x.Languages).NotEmpty().NotNull();
            RuleForEach(x => x.Languages).SetValidator(new ReferenceLanguageValidation());
        }
    }
}
