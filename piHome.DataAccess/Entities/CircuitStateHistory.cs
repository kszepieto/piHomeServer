using System;
using piHome.Models.Circuit.Enums;

namespace piHome.DataAccess.Entities
{
    public class CircuitStateHistory : BaseEntity
    {
        public Circuit Circuit { get; set; }
        
        public DateTime TurnOnTime { get; set; }

        public int TurnedOnLength { get; set; }
    }
}
