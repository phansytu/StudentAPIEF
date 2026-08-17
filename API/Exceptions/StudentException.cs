using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentAPIw6.Exceptions
{
    public class StudentException
    {
        public class SinhVienBadRequestException : NotFoundException
        {
            public SinhVienBadRequestException(string message) : base(message) { }
        }
        public class SinhVienNotFoundException : BadRequestException
        {
            public SinhVienNotFoundException(string mess) : base(mess) { }

        }
    }
}