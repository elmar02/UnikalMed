using Entities.DTOs.ReferenceDTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Validation.ReferenceValidation
{
    public class ReferenceLanguageValidation : AbstractValidator<ReferenceLangDTO>
    {
        public ReferenceLanguageValidation()
        {
            RuleFor(x => x.Address).NotEmpty().NotNull().WithName("Adres");
            RuleFor(x => x.Content).NotEmpty().NotNull().WithName("Kontent");
            RuleFor(x => x.Name).NotEmpty().NotNull().WithName("Ad");
            RuleFor(x => x.SurName).NotEmpty().NotNull().WithName("Soyad");
            RuleFor(x => x.LangCode).NotEmpty().NotNull().WithName("Dil Kodu");
        }
    }
}
