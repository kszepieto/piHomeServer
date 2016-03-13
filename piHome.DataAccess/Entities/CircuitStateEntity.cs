using piHome.Models.Circuit.Enums;

namespace piHome.DataAccess.Entities
{
    public class CircuitStateEntity : BaseEntity
    {
        public Circuit Circuit { get; set; }

        public string Name { get; set; }

        public bool State { get; set; }
    }
}
