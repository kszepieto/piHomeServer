using System.Collections.Generic;
using piHome.DataAccess.Entities;
using piHome.Models;
using piHome.Models.Enums;

namespace piHome.DataAccess.Interfaces
{
    public interface ICircuitsRepository
    {
        bool GetCircuitState(Circuit circuit);
        List<CircuitStateEntity> GetCircuitStates();
        CircuitStateHistory GetLastRowHistoricalState(Circuit circuit);

        void InsertHistory(CircuitStateHistory circuitStateHistory);
        void UpdateHistory(CircuitStateHistory circuitStateHistory);
        void UpdateCircuitState(Circuit circuit, bool state);
    }
}
