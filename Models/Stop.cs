using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DominicoBus.Models
{
    public class Stop
    {
        public string? Id { get; set; }
        public string? Code { get; set; }
        public string? Name { get; set; }
        public string? Latitude { get; set; }
        public string? Longitude { get; set; }
        public List<Route> Routes { get; set; } = new List<Route>();
    }
}