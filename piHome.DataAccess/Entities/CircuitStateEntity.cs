using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using piHome.Models.Enums;

namespace piHome.WebApi.Models
{
    public class CircuitStateEntity
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        
        public Circuit Circuit { get; set; }
        
        public bool State { get; set; }
    }
}
