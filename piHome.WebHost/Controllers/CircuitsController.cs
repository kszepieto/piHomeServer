using System.Collections.Generic;
using System.Web.Http;
using AutoMapper;
using piHome.Logic.Interfaces;
using piHome.Models.Entities.Circuits;
using piHome.Models.ValueObjects;
using piHome.Utils;
using piHome.WebHost.Infrastructure.Mapping;
using piHome.WebHost.WebModels.Circuits;

namespace piHome.WebHost.Controllers
{
    [RoutePrefix("api/Circuit")]
    public class CircuitsController : ApiController
    {
        private readonly IOutputCircuitsManager _outputCircuitsManager;
        private readonly IMapper _mapper;

        [HttpGet]
        [Route("Circuits")]
        public List<CircuitStateVM> GetCircuitStates()
        {
            LogHelper.LogMessage("GetCircuitStates");

            var circuits = _outputCircuitsManager.GetOutputPinsInfo();

            return _mapper.MapList<CircuitStateEntity, CircuitStateVM>(circuits);
        }

        [HttpPost]
        [Route("Switch")]
        public void Switch(StateChangeVM changeVM)
        {
            var change = _mapper.Map<StateChange>(changeVM);
            _outputCircuitsManager.SwitchCircuit(change);
        }

        #region C'stor

        public CircuitsController(IOutputCircuitsManager outputCircuitsManager, IMapper mapper)
        {
            _outputCircuitsManager = outputCircuitsManager;
            _mapper = mapper;
        }

        #endregion
    }
}
