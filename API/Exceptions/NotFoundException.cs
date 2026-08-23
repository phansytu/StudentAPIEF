using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentAPIw6.API.Exceptions
{

    public class NotFoundException : Exception
    {
        public NotFoundException(string mess) : base(mess) { }

    }

}