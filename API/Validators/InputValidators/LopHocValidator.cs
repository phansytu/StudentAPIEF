using FluentValidation;
using StudentAPIw6.API.DTOs.Request;

namespace StudentAPIw6.API.Validators.InputValidators
{
    public class LopHocValidator
    {
        public class LopHocCreateValidator
            : AbstractValidator<LopHocRequestDTO.LopHocCreateDTO>
        {
            public LopHocCreateValidator()
            {
                RuleFor(x => x.TenLop)
                    .NotEmpty()
                    .WithMessage("Tên lớp không được để trống")
                    .MaximumLength(100)
                    .WithMessage("Tên lớp không được vượt quá 100 ký tự");

                RuleFor(x => x.ChuyenNganh)
                    .NotEmpty()
                    .WithMessage("Chuyên ngành không được để trống")
                    .MaximumLength(100)
                    .WithMessage("Chuyên ngành không được vượt quá 100 ký tự");
            }
        }
        public class LopHocUpdateValidator
            : AbstractValidator<LopHocRequestDTO.LopHocUpdateDTO>
        {
            public LopHocUpdateValidator()
            {
                RuleFor(x => x.TenLop)
                    .NotEmpty()
                    .WithMessage("Tên lớp không được để trống")
                    .MaximumLength(100)
                    .WithMessage("Tên lớp không được vượt quá 100 ký tự");

                RuleFor(x => x.ChuyenNganh)
                    .NotEmpty()
                    .WithMessage("Chuyên ngành không được để trống")
                    .MaximumLength(100)
                    .WithMessage("Chuyên ngành không được vượt quá 100 ký tự");
            }
        }
        public class LopHocDeleteValidator
            : AbstractValidator<LopHocRequestDTO.LopHocDeleteDTO>
        {
            public LopHocDeleteValidator()
            {
                RuleFor(x => x.MaLop)
                    .NotEmpty()
                    .WithMessage("Mã lớp không được để trống")
                    .MaximumLength(20)
                    .WithMessage("Mã lớp không được vượt quá 20 ký tự");
            }
        }
    }
}