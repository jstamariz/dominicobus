using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DominicoBus.Models.Base;

namespace DominicoBus.Models
{
    public class Stop : Entity
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public List<Route> Routes { get; set; }
    }
}