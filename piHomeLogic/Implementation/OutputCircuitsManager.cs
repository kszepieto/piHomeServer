using System.Collections.Generic;
using piHome.DataAccess.Interfaces;
using piHome.GpioWrapper;
using piHome.Logic.Interfaces;
using piHome.Logic.Shared.Interfaces;
using piHome.Models.Entities.Circuits;
using piHome.Models.ValueObjects;

namespace piHome.Logic.Implementation
{
    public class OutputCircuitsManager : IOutputCircuitsManager
    {
        private readonly IGpioOutputInterface _gpioOutputInterface;
        private readonly ICircuitsDalHelper _circuitsDalHelper;
        private readonly IPinMapper _pinMapper;

        #region C'stor

        public OutputCircuitsManager(IGpioOutputInterface gpioOutputInterface, 
            ICircuitsDalHelper circuitsDalHelper,
            IPinMapper pinMapper)
        {
            _gpioOutputInterface = gpioOutputInterface;
            _circuitsDalHelper = circuitsDalHelper;
            _pinMapper = pinMapper;
        }

        #endregion
        
        public void SwitchCircuit(StateChange change)
        {
            var currentState = _circuitsDalHelper.GetCircuitState(change.Circuit);
            if (change.State != currentState)
            {
                var outputPin = _pinMapper.MapCircuitToOutputPin(change.Circuit);
                _gpioOutputInterface.ChangeCircutState(outputPin);
            }
        }

        public List<CircuitStateEntity> GetOutputPinsInfo()
        {
            return _circuitsDalHelper.GetCircuitStates();
        }
    }
}
