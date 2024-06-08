using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DominicoBus.DataTransfer
{
    public class BusDTO
    {
        [Range(0, int.MaxValue)]
        public int Capacity { get; set; }
        [Required]
        public string? Code { get; set; }
    }
}