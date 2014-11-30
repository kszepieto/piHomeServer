using System;
using piHome.DataAccess.Entities;
using piHome.DataAccess.Interfaces;
using piHome.GpioWrapper;
using piHome.GpioWrapper.Enums;
using piHome.Logic.Interfaces;
using piHome.Utils;

namespace piHome.Logic.Implementation
{
    public class InputCircuitsManager : IInputCircuitsManager
    {
        private readonly ICircuitsRepository _circuitsRepository;
        private readonly IPinMapper _mapper;
        private readonly IDateProvider _dateProvider;

        public InputCircuitsManager(ICircuitsRepository circuitsRepository,
            IPinMapper mapper,
            IDateProvider dateProvider)
        {
            _circuitsRepository = circuitsRepository;
            _mapper = mapper;
            _dateProvider = dateProvider;
        }

        public void HandleCircuitChange(bool state, InputPin inputPin)
        {
            LogHelper.LogMessage("state: {0}, inputPin: {1}", state, inputPin);

            var circuit = _mapper.MapInputPinToCircuit(inputPin);
            if (state)
            {
                //when circuit is turned on
                var newRow = new CircuitHistoricalState { Circuit = circuit, TurnOnTime = _dateProvider.GetDate(), TurnedOnLength = 0 };
                _circuitsRepository.Insert(newRow);
            }
            else
            {
                var turnedOnEntry = _circuitsRepository.GetLastRowHistoricalState(circuit);

                if (turnedOnEntry != null)
                {
                    var now = _dateProvider.GetDate();
                    var turnedOnTime = (int)Math.Round((now - turnedOnEntry.TurnOnTime).TotalSeconds);

                    turnedOnEntry.TurnedOnLength = turnedOnTime;
                    _circuitsRepository.Update(turnedOnEntry);
                }
            }
        }
    }
}
