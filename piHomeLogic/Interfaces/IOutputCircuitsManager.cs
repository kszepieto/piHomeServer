using System.Collections.Generic;
using piHome.GpioWrapper.Enums;
using piHome.Models.Enums;
using piHome.WebApi.Models;

namespace piHome.Logic.Interfaces
{
    public interface IOutputCircuitsManager
    {
        void SwitchCircuit(Circuit circuit, bool newState);

        List<CircuitState> GetOutputPinsInfo();
    }
}
