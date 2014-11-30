using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using piHome.DataAccess.Entities;
using piHome.DataAccess.Interfaces;
using piHome.Models.Enums;
using piHome.WebApi.Models;

namespace piHome.DataAccess.Implementation
{
    public class CircuitsRepository : BaseRepository, ICircuitsRepository
    {
        public CircuitsRepository(SqlLiteDb db) 
            : base(db)
        {
        }

        public bool GetCircuitState(Circuit circuit)
        {
            var circuits = _db.Connection.Query<CircuitStateEntity>("select * from CircuitStateEntity where Circuit = ?", circuit);
            return circuits.Single().State;
        }

        public List<CircuitStateEntity> GetCircuitStates()
        {
            return _db.Connection.Query<CircuitStateEntity>("select * from CircuitStateEntity");
        }

        public CircuitHistoricalState GetLastRowHistoricalState(Circuit circuit)
        {
            var query = "select * from CircuitHistoricalState where Circuit = ? AND TurnedOnLength = 0 order by TurnOnTime desc limit 1";
            var circuits = _db.Connection.Query<CircuitHistoricalState>(query, circuit);

            return circuits.SingleOrDefault();
        }
    }
}
