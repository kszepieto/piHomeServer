using piHome.Models.Enums;

namespace piHome.WebHost.WebModels.Circuits
{
    public class CircuitStateVM
    {
        public Circuit Circuit { get; set; }
        public bool State { get; set; }
        public string Name { get; set; }
    }
}
