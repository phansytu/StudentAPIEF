using FluentValidation;
using StudentAPIw6.DTOs;
namespace StudentAPIw6.validator
{
    public class LopHocValidator
    {
        public class LopHocCreateValidator
            : AbstractValidator<LopHocDTO.LopHocCreateDTO>
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
            : AbstractValidator<LopHocDTO.LopHocUpdateDTO>
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
            : AbstractValidator<LopHocDTO.LopHocDeleteDTO>
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