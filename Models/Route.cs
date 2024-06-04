using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DominicoBus.Models
{
    public class Route
    {
        public string? Id { get; set; }
        public string? Name { get; set; }
        public List<Stop> Stops { get; set; } = new List<Stop>();
    }
}