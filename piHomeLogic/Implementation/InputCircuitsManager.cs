using System;
using piHome.DataAccess.Entities;
using piHome.DataAccess.Interfaces;
using piHome.Events;
using piHome.GpioWrapper.Enums;
using piHome.Logic.Interfaces;
using piHome.Models.Circuit;
using piHome.Utils;

namespace piHome.Logic.Implementation
{
    public class InputCircuitsManager : IInputCircuitsManager
    {
        private readonly ICircuitsRepository _circuitsRepository;
        private readonly IPinMapper _mapper;
        private readonly IDateProvider _dateProvider;
        private readonly IEventBroadcaster _eventBroadcaster;

        public InputCircuitsManager(ICircuitsRepository circuitsRepository,
            IPinMapper mapper,
            IDateProvider dateProvider,
            IEventBroadcaster eventBroadcaster)
        {
            _circuitsRepository = circuitsRepository;
            _mapper = mapper;
            _dateProvider = dateProvider;
            _eventBroadcaster = eventBroadcaster;
        }

        public void HandleCircuitChange(bool state, InputPin inputPin)
        {
            LogHelper.LogMessage("state: {0}, inputPin: {1}", state, inputPin);

            var circuit = _mapper.MapInputPinToCircuit(inputPin);
            if (state)
            {
                //when circuit is turned on
                var newRow = new CircuitStateHistory { Circuit = circuit, TurnOnTime = _dateProvider.GetUtcDateTimeDate(), TurnedOnLength = 0 };
                _circuitsRepository.InsertHistory(newRow);
            }
            else
            {
                var turnedOnEntry = _circuitsRepository.GetLastRowHistoricalState(circuit);

                if (turnedOnEntry != null)
                {
                    var now = _dateProvider.GetUtcDateTimeDate();
                    var turnedOnTime = (int)Math.Round((now - turnedOnEntry.TurnOnTime).TotalSeconds);

                    turnedOnEntry.TurnedOnLength = turnedOnTime;
                    _circuitsRepository.UpdateHistory(turnedOnEntry);
                }
            }

            _circuitsRepository.UpdateCircuitState(circuit, state);
            _eventBroadcaster.BroadcastCircuitStateChange(new StateChange { Circuit = circuit, State = state });
        }
    }
}
