using System;
using System.Collections.Generic;
using System.Linq;
using piHome.DataAccess.Interfaces;
using piHome.GpioWrapper;
using piHome.GpioWrapper.Enums;
using piHome.Logic.Interfaces;
using piHome.Models.Enums;
using piHome.WebApi.Models;

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
        
        public void SwitchCircuit(Circuit circuit, bool newState)
        {
            var currentState = _circuitsRepository.GetCircuitState(circuit);
            if (newState != currentState)
            {
                var outputPin = _pinMapper.MapCircuitToOutputPin(circuit);
                _gpioOutputInterface.ChangeCircutState(outputPin);
            }
        }

        public List<CircuitState> GetOutputPinsInfo()
        {
            var circuits = _circuitsRepository.GetCircuitStates();
            return circuits.Select(x => new CircuitState {Circuit = x.Circuit, State = x.State}).ToList();
        }
    }
}
