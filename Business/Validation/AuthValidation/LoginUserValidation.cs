using Entities.DTOs.AuthDTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Validation.AuthValidation
{
    public class LoginUserValidation : AbstractValidator<LoginDTO>
    {
        public LoginUserValidation()
        {
            RuleFor(x => x.EmailOrUsername).NotNull().NotEmpty().WithName("İstifadəçi adı və ya E-poçt");
            RuleFor(x => x.Password).NotEmpty().NotNull().WithName("Şifrə");
        }
    }
}
