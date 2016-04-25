using System.Collections.Generic;
using piHome.WebHost.WebModels.Circuits;

namespace piHome.WebHost.WebModels.UserSettings
{
    public class CircuitsHandlingSetVM
    {
        public string Name { get; set; }
        public bool IsPrivate { get; set; }
        public List<StateChangeVM> StatesOnActivation { get; set; }
        public List<StateChangeVM> StatesOnDeactivation { get; set; }
    }
}