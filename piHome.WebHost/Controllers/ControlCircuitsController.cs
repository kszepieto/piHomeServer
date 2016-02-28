using System;
using System.Collections.Generic;
using System.Web.Http;
using Ninject;
using piHome.GpioWrapper;
using piHome.GpioWrapper.Enums;
using piHome.Logic.Interfaces;
using piHome.Models;
using piHome.Models.Enums;
using piHome.Utils;
using piHome.WebHost.Infrastructure;

namespace piHome.WebHost.Controllers
{
    public class ControlCircuitsController : ApiController
    {
        private readonly IOutputCircuitsManager _outputCircuitsManager;

        #region C'stor

        public ControlCircuitsController(IOutputCircuitsManager outputCircuitsManager)
        {
            _outputCircuitsManager = outputCircuitsManager;
        }

        #endregion

        [HttpGet]
        public List<CircuitState> GetCircuitStates()
        {
            LogHelper.LogMessage("GetCircuitStates");

            var circuits = _outputCircuitsManager.GetOutputPinsInfo();

            return circuits;
        }

        [HttpPost]
        public void Switch(CircuitStateChange change)
        {
#if DEBUG
            var inputGpio = NinjectConfiguration.GetInstance().Kernel.Get<IGpioInputInterface>();

            Dictionary<Circuit, InputPin> inputPins = new Dictionary<Circuit, InputPin>
            {
                {Circuit.C1, InputPin.I1},
                {Circuit.C2, InputPin.I2},
                {Circuit.C3, InputPin.I3},
                {Circuit.C4, InputPin.I4},
                {Circuit.C5, InputPin.I5},
                {Circuit.C6, InputPin.I6},
                {Circuit.C7, InputPin.I7},
                {Circuit.C8, InputPin.I8},
                {Circuit.C9, InputPin.I9},
                {Circuit.C10, InputPin.I10},
            };

            inputGpio.CircuitStateChanged(change.State, inputPins[change.Circuit]);
#endif

            _outputCircuitsManager.SwitchCircuit(change);
        }
    }
}
