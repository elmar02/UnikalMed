using Business.Validation.PhotoValidation;
using Entities.DTOs.AdvertDTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Validation.AdvertValidation
{
    public class CreateAdvertValidation : AbstractValidator<CreateAdvertDTO>
    {
        public CreateAdvertValidation()
        {
            RuleFor(x=>x.Link).NotEmpty().NotNull();
            RuleFor(x => x.Languages).NotEmpty().NotNull();
            RuleForEach(x => x.Languages).SetValidator(new UploadValidation());
        }
    }
}
