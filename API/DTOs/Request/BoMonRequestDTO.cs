using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentAPIw6.API.DTOs.Request
{
    public class BoMonRequestDTO
    {
        public class CreateBoMonDTO
        {
            public required string tenMon { get; set; }

        }
        public class UpdateBoMonDTO
        {
            public required string tenMon { get; set; }

        }
        public class DeleteBoMonDTO
        {
            public required string maBM { get; set; }
        }
    }
}