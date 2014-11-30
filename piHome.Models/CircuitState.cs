using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using piHome.Models.Enums;

namespace piHome.WebApi.Models
{
    public class CircuitState
    {
        public Circuit Circuit { get; set; }
        public bool State { get; set; }
    }
}
