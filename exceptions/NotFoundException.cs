using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentAPIw5.exceptions
{

    public class NotFoundException : Exception
    {
        public NotFoundException(string mess) : base(mess) { }

    }

}