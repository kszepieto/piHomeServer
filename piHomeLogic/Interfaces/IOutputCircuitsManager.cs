using System.Collections.Generic;
using piHome.Models;

namespace piHome.Logic.Interfaces
{
    public interface IOutputCircuitsManager
    {
        void SwitchCircuit(CircuitStateChange change);

        List<CircuitState> GetOutputPinsInfo();
    }
}
