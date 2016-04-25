using piHome.Models.Enums;

namespace piHome.Models.Entities.Circuits
{
    public class CircuitStateEntity : BaseEntity
    {
        public Circuit Circuit { get; set; }

        public string Name { get; set; }

        public bool State { get; set; }
    }
}
