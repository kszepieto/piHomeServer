using System.Collections.Generic;
using piHome.DataAccess.Entities;
using piHome.Models.Enums;

namespace piHome.DataAccess.Interfaces
{
    public interface ICircuitsRepository
    {
        bool GetCircuitState(Circuit circuit);
        List<CircuitStateEntity> GetCircuitStates();
        CircuitStateHistory GetLastRowHistoricalState(Circuit circuit);
        void Insert(CircuitStateHistory circuitStateHistory);
        void Update(CircuitStateHistory circuitStateHistory);
    }
}
