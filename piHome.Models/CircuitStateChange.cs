using piHome.Models.Enums;

namespace piHome.Models
{
    public class CircuitStateChange
    {
        public Circuit Circuit { get; set; }
        public bool State { get; set; }
    }
}