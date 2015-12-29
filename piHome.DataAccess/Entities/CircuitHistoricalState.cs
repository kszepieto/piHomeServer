using System;
using piHome.Models.Enums;
using SQLite;

namespace piHome.DataAccess.Entities
{
    public class CircuitHistoricalState
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public Circuit Circuit { get; set; }
        
        /// <summary>
        /// Time when circuit was turned on
        /// </summary>
        public DateTime TurnOnTime { get; set; }

        /// <summary>
        /// Length in minutes when circuit was on in seconds
        /// </summary>
        public int TurnedOnLength { get; set; }
    }
}
