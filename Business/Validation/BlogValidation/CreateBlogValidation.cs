using Business.Validation.StaffValidation;
using Entities.DTOs.BlogDTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Validation.BlogValidation
{
    public class CreateBlogValidation : AbstractValidator<CreateBlogDTO>
    {
        public CreateBlogValidation()
        {
            RuleFor(x=>x.PhotoUrl).NotEmpty().NotNull();
            RuleFor(x=>x.Languages).NotEmpty().NotNull();
            RuleForEach(x => x.Languages).SetValidator(new BlogLanguageValidation());
        }
    }
}
