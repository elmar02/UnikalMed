using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.DTOs.HeaderDTOs;
using Entities.DTOs.ProductDTOs;
using FluentValidation;
using System.Linq;
using Business.Validation.HeaderValidation;
namespace Business.Validation.ProductValidation
{
    public class UpdateProductValidation : AbstractValidator<UpdateProductDTO>
    {
        public UpdateProductValidation()
        {
            RuleForEach(x => x.Languages)
                .SetValidator(new ProductLanguageValidation());

            RuleForEach(x => x.Headers)
                .SetValidator(new HeaderLangValidation());
        }
    }
}
