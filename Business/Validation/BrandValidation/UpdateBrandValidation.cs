﻿using Entities.DTOs.BrandDTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Validation.BrandValidation
{
    public class UpdateBrandValidation : AbstractValidator<UpdateBrandDTO>
    {
        public UpdateBrandValidation()
        {
            RuleFor(x => x.BrandName).NotEmpty().NotNull().WithName("Brend adı");
        }
    }
}
