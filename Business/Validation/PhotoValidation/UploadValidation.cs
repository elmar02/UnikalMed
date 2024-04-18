using Entities.DTOs.UploadDTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Validation.PhotoValidation
{
    public class UploadValidation : AbstractValidator<PhotoUrlWithLangDTO>
    {
        public UploadValidation()
        {
            RuleFor(x=>x.LangCode).NotEmpty().NotNull();
            RuleFor(x=>x.PhotoUrl).NotEmpty().NotNull();
        }
    }
}
