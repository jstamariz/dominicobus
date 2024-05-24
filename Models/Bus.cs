using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DominicoBus.Models.Base;

namespace DominicoBus.Models
{
    public class Bus : Entity
    {
        public int Capacity { get; set; } = default;
        public string? Code { get; set; }
    }
}