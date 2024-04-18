using Entities.DTOs.ServiceDTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Validation.ServiceValidation
{
    public class ServiceLanguageValidation : AbstractValidator<ServiceLangDTO>
    {
        public ServiceLanguageValidation()
        {
            RuleFor(x=>x.ServiceName).NotEmpty().NotNull().WithName("Servis adı");
            RuleFor(x => x.LangCode).NotEmpty().NotNull().WithName("Dil Kodu");
        }
    }
}
