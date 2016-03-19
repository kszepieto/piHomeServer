using System.Collections.Generic;
using piHome.Models.Circuit;

namespace piHome.Logic.Interfaces
{
    public interface IOutputCircuitsManager
    {
        void SwitchCircuit(StateChange change);

        List<CircuitState> GetOutputPinsInfo();
    }
}
