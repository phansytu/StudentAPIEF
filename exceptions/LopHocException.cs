using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StudentAPIw5.data;

namespace StudentAPIw5.exceptions
{
    public class LopHocException : Exception
    {
        public class LopHocBadRequestException : NotFoundException
        {
            public LopHocBadRequestException(string message) : base(message) { }
        }
        public class LopHocNotFoundException : BadRequestException
        {
            public LopHocNotFoundException(string mess) : base(mess) { }

        }
    }
}