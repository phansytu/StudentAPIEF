using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentAPIw5.exceptions
{
    public class StudentException
    {
        public class SinhVienBadRequestException : Exception
        {
            public SinhVienBadRequestException(string message) : base(message) { }
        }
        public class SinhVienNotFoundException : Exception
        {
            public SinhVienNotFoundException(string mess) : base(mess) { }

        }
    }
}