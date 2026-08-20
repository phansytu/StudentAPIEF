using System;
using FluentValidation;
using StudentAPIw6.DTOs;

namespace StudentAPIw6.validator
{
    public class SinhVienValidator
    {
        public class SinhVienCreateValidator : AbstractValidator<SinhVienDTO.SinhVienCreateDTO>
        {
            public SinhVienCreateValidator()
            {
                RuleFor(x => x.HoTen)
                    .NotEmpty()
                    .WithMessage("Họ tên không được để trống")
                    .MaximumLength(100)
                    .WithMessage("Họ tên không được vượt quá 100 ký tự");

                RuleFor(x => x.Email)
                    .NotEmpty()
                    .WithMessage("Email không được để trống")
                    .EmailAddress()
                    .WithMessage("Email không hợp lệ")
                    .MaximumLength(150)
                    .WithMessage("Email không được vượt quá 150 ký tự");

                RuleFor(x => x.NgaySinh)
                    .LessThan(DateTime.Today)
                    .WithMessage("Ngày sinh phải nhỏ hơn ngày hiện tại");

                RuleFor(x => x.DiemTB)
                    .InclusiveBetween(0, 10)
                    .WithMessage("Điểm trung bình phải từ 0 đến 10");

                RuleFor(x => x.lopHocId)
                    .NotNull()
                    .WithMessage("Id lớp không được để trống");
            }
        }

        // 2. Validator cho Update DTO
        public class SinhVienUpdateValidator : AbstractValidator<SinhVienDTO.SinhVienUpdateDTO>
        {
            public SinhVienUpdateValidator()
            {
                RuleFor(x => x.HoTen)
                    .NotEmpty()
                    .WithMessage("Họ tên không được để trống")
                    .MaximumLength(100)
                    .WithMessage("Họ tên không được vượt quá 100 ký tự");

                RuleFor(x => x.Email)
                    .NotEmpty()
                    .WithMessage("Email không được để trống")
                    .EmailAddress()
                    .WithMessage("Email không hợp lệ")
                    .MaximumLength(150)
                    .WithMessage("Email không được vượt quá 150 ký tự");

                RuleFor(x => x.NgaySinh)
                    .LessThan(DateTime.Today)
                    .WithMessage("Ngày sinh phải nhỏ hơn ngày hiện tại");

                RuleFor(x => x.DiemTB)
                    .InclusiveBetween(0, 10)
                    .WithMessage("Điểm trung bình phải từ 0 đến 10");

                RuleFor(x => x.lopHocId)
                    .NotNull()
                    .WithMessage("Id lớp phải tồn tại trong lớp học");
            }
        }
    }
}