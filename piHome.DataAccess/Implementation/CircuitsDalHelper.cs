using System.Collections.Generic;
using System.Linq;
using MongoDB.Driver;
using piHome.DataAccess.Interfaces;
using piHome.Models.Entities.Circuits;
using piHome.Models.Enums;

namespace piHome.DataAccess.Implementation
{
    public class CircuitsDalHelper : BaseDalHelper, ICircuitsDalHelper
    {
        public bool GetCircuitState(Circuit circuit)
        {
            return _dbContext.Circuits.Find(c => c.Circuit == circuit).Single().State;
        }

        public List<CircuitStateEntity> GetCircuitStates()
        {
            return _dbContext.Circuits.Find(c => true).SortBy(c => c.Name).ToList();
        }

        public void UpdateCircuitState(Circuit circuit, bool state)
        {
            _dbContext.Circuits.UpdateOne(c => c.Circuit == circuit, Builders<CircuitStateEntity>.Update.Set(c => c.State, state));
        }

        public CircuitStateHistoryEntity GetLastRowHistoricalState(Circuit circuit)
        {
            var circuits = _dbContext.CircuitsStateHistory.Find(c => c.Circuit == circuit && c.TurnedOnLength == 0)
                .SortByDescending(c => c.TurnOnTime).Limit(1).ToList();

            return circuits.SingleOrDefault();
        }

        public void InsertHistory(CircuitStateHistoryEntity circuitStateHistory)
        {
            _dbContext.CircuitsStateHistory.InsertOne(circuitStateHistory);
        }

        public void UpdateHistory(CircuitStateHistoryEntity circuitStateHistory)
        {
            _dbContext.CircuitsStateHistory.FindOneAndReplace(c => c.Id == circuitStateHistory.Id, circuitStateHistory);
        }

        public CircuitsDalHelper(IDbContext dbContext)
            : base(dbContext)
        {
        }
    }
}
