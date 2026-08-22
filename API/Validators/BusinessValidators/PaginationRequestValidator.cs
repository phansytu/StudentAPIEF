using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;

using StudentAPIw6.Model.request;
namespace StudentAPIw6.API.Validators.BusinessValidators
{
    public class PaginationRequestValidator : AbstractValidator<PaginationRequest>
    {
        public PaginationRequestValidator()
        {
            RuleFor(x => x.PageNumber).GreaterThanOrEqualTo(0).WithMessage("PageNumber phải lớn hơn hoặc bằng 0");
            RuleFor(x => x.PageSize).InclusiveBetween(1, 50).WithMessage("PageSize phải nằm trong khoảng từ 1 đến 100");
        }
    }
}