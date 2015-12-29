using System.Collections.Generic;
using System.Web.Http;
using piHome.Logic.Interfaces;
using piHome.Models;
using piHome.Utils;

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
            _outputCircuitsManager.SwitchCircuit(change);
        }
    }
}
