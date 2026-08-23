using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using StudentAPIw6.API.DTOs.Request;

namespace StudentAPIw6.API.Validators.InputValidators
{
    public class BoMonValidator : AbstractValidator<BoMonRequestDTO.CreateBoMonDTO>
    {
        public BoMonValidator()
        {
            RuleFor(x => x.tenMon)
                .NotEmpty()
                    .WithMessage("Tên môn không được để trống")
                    .MaximumLength(100)
                    .WithMessage("Tên môn không được vượt quá 100 ký tự");
        }
    }
}