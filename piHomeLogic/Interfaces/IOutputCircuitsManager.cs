using System.Collections.Generic;
using piHome.Models.Circuit;

namespace piHome.Logic.Interfaces
{
    public interface IOutputCircuitsManager
    {
        void SwitchCircuit(CircuitStateChange change);

        List<CircuitState> GetOutputPinsInfo();
    }
}
