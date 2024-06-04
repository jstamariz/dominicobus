using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DominicoBus.Models
{
    public class Bus
    {
        public string? Id { get; set; }
        public int Capacity { get; set; } = default;
        public string? Code { get; set; }
    }
}