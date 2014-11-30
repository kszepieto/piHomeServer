using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using piHome.GpioWrapper;
using piHome.Logic.Interfaces;
using piHome.Models.Enums;
using piHome.Utils;
using piHome.WebApi.Models;

namespace piHome.WebApi.Controllers
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
            LogHelper.LogMessage("GetCircuitStates: {0}");

            return _outputCircuitsManager.GetOutputPinsInfo();
        }

        [HttpPost]
        public void Switch(Circuit circuit, bool state)
        {
            _outputCircuitsManager.SwitchCircuit(circuit, state);
        }
    }
}
