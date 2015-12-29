using System.Collections.Generic;
using piHome.DataAccess.Entities;
using piHome.Models.Enums;

namespace piHome.DataAccess.Interfaces
{
    public interface ICircuitsRepository : IBaseRepository
    {
        bool GetCircuitState(Circuit circuit);
        List<CircuitStateEntity> GetCircuitStates();
        CircuitHistoricalState GetLastRowHistoricalState(Circuit circuit);
    }
}
