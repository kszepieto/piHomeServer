using piHome.Models.Enums;
using SQLite;

namespace piHome.DataAccess.Entities
{
    public class CircuitStateEntity
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        
        public Circuit Circuit { get; set; }

        public string Name { get; set; }

        public bool State { get; set; }
    }
}
