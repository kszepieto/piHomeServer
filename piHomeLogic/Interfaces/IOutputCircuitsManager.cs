using System.Collections.Generic;
using piHome.Models.Entities.Circuits;
using piHome.Models.ValueObjects;

namespace piHome.Logic.Interfaces
{
    public interface IOutputCircuitsManager
    {
        void SwitchCircuit(StateChange change);

        List<CircuitStateEntity> GetOutputPinsInfo();
    }
}
