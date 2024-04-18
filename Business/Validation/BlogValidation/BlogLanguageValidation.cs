using Business.Validation.StaffValidation;
using Entities.DTOs.BlogDTOs;
using Entities.DTOs.StaffDTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Validation.BlogValidation
{
    public class BlogLanguageValidation : AbstractValidator<BlogLangDTO>
    {
        public BlogLanguageValidation()
        {
            RuleFor(x => x.Title).NotEmpty().NotNull().WithName("Başlıq");
            RuleFor(x => x.Content).NotEmpty().NotNull().WithName("Kontent");
            RuleFor(x => x.SubTitle).NotEmpty().NotNull().WithName("ALt Başlıq");
            RuleFor(x => x.LangCode).NotEmpty().NotNull().WithName("Dil Kodu");
        }
    }
}
