using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DominicoBus.DataTransfer
{
    public record BusResult(string Code, int Capacity, string Id) { }
}