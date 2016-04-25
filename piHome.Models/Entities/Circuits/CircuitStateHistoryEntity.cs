using System;
using piHome.Models.Enums;

namespace piHome.Models.Entities.Circuits
{
    public class CircuitStateHistoryEntity : BaseEntity
    {
        public Circuit Circuit { get; set; }
        
        public DateTime TurnOnTime { get; set; }
        
        public int TurnedOnLength { get; set; }
    }
}
