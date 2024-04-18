using Entities.DTOs.StaffDTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Validation.StaffValidation
{
    public class StaffLanguageValidation : AbstractValidator<StaffLangDTO>
    {
        public StaffLanguageValidation()
        {
            RuleFor(x=>x.Profession).NotEmpty().NotNull().WithName("Peşə");
            RuleFor(x => x.Content).NotEmpty().NotNull().WithName("Kontent");
            RuleFor(x => x.Name).NotEmpty().NotNull().WithName("Ad");
            RuleFor(x => x.SurName).NotEmpty().NotNull().WithName("Soyad");
            RuleFor(x => x.LangCode).NotEmpty().NotNull().WithName("Dil Kodu");
        }
    }
}
