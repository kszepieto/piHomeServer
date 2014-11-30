using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using piHome.Models.Enums;

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
