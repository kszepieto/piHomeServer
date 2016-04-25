using System.Collections.Generic;
using piHome.Models.Entities.Circuits;
using piHome.Models.Enums;

namespace piHome.DataAccess.Interfaces
{
    public interface ICircuitsDalHelper
    {
        bool GetCircuitState(Circuit circuit);
        List<CircuitStateEntity> GetCircuitStates();
        void UpdateCircuitState(Circuit circuit, bool state);

        CircuitStateHistoryEntity GetLastRowHistoricalState(Circuit circuit);
        void InsertHistory(CircuitStateHistoryEntity circuitStateHistory);
        void UpdateHistory(CircuitStateHistoryEntity circuitStateHistory);
    }
}
