using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentAPIw6.API.Exceptions
{

    public class BadRequestException : Exception
    {
        public BadRequestException(string message) : base(message) { }
    }


}