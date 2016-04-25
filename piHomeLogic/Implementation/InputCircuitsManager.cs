using System;
using piHome.DataAccess.Interfaces;
using piHome.Events;
using piHome.GpioWrapper.Enums;
using piHome.Logic.Interfaces;
using piHome.Logic.Shared.Interfaces;
using piHome.Models.Entities.Circuits;
using piHome.Models.ValueObjects;
using piHome.Utils;

namespace piHome.Logic.Implementation
{
    public class InputCircuitsManager : IInputCircuitsManager
    {
        private readonly ICircuitsDalHelper _circuitsDalHelper;
        private readonly IPinMapper _mapper;
        private readonly IDateProvider _dateProvider;
        private readonly IEventBroadcaster _eventBroadcaster;

        public InputCircuitsManager(ICircuitsDalHelper circuitsDalHelper,
            IPinMapper mapper,
            IDateProvider dateProvider,
            IEventBroadcaster eventBroadcaster)
        {
            _circuitsDalHelper = circuitsDalHelper;
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
                var newRow = new CircuitStateHistoryEntity
                {
                    Circuit = circuit,
                    TurnOnTime = _dateProvider.GetUtcDateTimeDate(),
                    TurnedOnLength = 0
                };
                _circuitsDalHelper.InsertHistory(newRow);
            }
            else
            {
                var turnedOnEntry = _circuitsDalHelper.GetLastRowHistoricalState(circuit);
                if (turnedOnEntry != null)
                {
                    var now = _dateProvider.GetUtcDateTimeDate();
                    var turnedOnTime = (int)Math.Round((now - turnedOnEntry.TurnOnTime).TotalSeconds);
                    turnedOnEntry.TurnedOnLength = turnedOnTime;

                    _circuitsDalHelper.UpdateHistory(turnedOnEntry);
                }
            }

            _circuitsDalHelper.UpdateCircuitState(circuit, state);
            _eventBroadcaster.BroadcastCircuitStateChange(new StateChange { Circuit = circuit, State = state });
        }
    }
}
