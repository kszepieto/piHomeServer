using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using piHome.Models.Circuit.Enums;

namespace piHome.WebHost.WebModels.Circuits
{
    public class CircuitStateVM
    {
        public Circuit Circuit { get; set; }
        public bool State { get; set; }
        public string Name { get; set; }
    }
}
