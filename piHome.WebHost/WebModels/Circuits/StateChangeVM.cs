using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using piHome.Models.Circuit.Enums;

namespace piHome.WebHost.WebModels.Circuits
{
    public class StateChangeVM
    {
        public Circuit Circuit { get; set; }
        public bool NewState { get; set; }
    }
}
