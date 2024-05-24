using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DominicoBus.Models.Base;

namespace DominicoBus.Models
{
    public class User : Entity
    {
        public string? Name { get; set; }
        public string? Email { get; set; }
    }
}