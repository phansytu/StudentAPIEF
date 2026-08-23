using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentAPIw6.API.DTOs.Response
{
    public class BoMonResponseDTO
    {
        public int id { get; set; }
        public required string tenMon { get; set; }
        public required string maBM { get; set; }
    }
}