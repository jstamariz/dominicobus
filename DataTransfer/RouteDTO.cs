using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace DominicoBus.DataTransfer
{
    public class RouteDTO
    {
        public string? Id { get; set; }
        [Required]
        public string? Name { get; set; }
        public List<RouteStop> Stops { get; set; } = new();
    }
}