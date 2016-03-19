using System.Collections.Generic;
using System.Linq;
using piHome.DataAccess.Interfaces;
using piHome.GpioWrapper;
using piHome.Logic.Interfaces;
using piHome.Models.Circuit;

namespace piHome.Logic.Implementation
{
    public class OutputCircuitsManager : IOutputCircuitsManager
    {
        private readonly IGpioOutputInterface _gpioOutputInterface;
        private readonly ICircuitsRepository _circuitsRepository;
        private readonly IPinMapper _pinMapper;

        #region C'stor

        public OutputCircuitsManager(IGpioOutputInterface gpioOutputInterface, 
            ICircuitsRepository circuitsRepository,
            IPinMapper pinMapper)
        {
            _gpioOutputInterface = gpioOutputInterface;
            _circuitsRepository = circuitsRepository;
            _pinMapper = pinMapper;
        }

        #endregion
        
        public void SwitchCircuit(StateChange change)
        {
            var currentState = _circuitsRepository.GetCircuitState(change.Circuit);
            if (change.State != currentState)
            {
                var outputPin = _pinMapper.MapCircuitToOutputPin(change.Circuit);
                _gpioOutputInterface.ChangeCircutState(outputPin);
            }
        }

        public List<CircuitState> GetOutputPinsInfo()
        {
            var circuits = _circuitsRepository.GetCircuitStates();
            return circuits.Select(x => new CircuitState {Circuit = x.Circuit, State = x.State, Name = x.Name}).ToList();
        }
    }
}
