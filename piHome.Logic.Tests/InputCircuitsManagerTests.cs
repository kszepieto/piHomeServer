using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using piHome.DataAccess.Interfaces;
using piHome.Events;
using piHome.GpioWrapper.Enums;
using piHome.Logic.Implementation;
using piHome.Logic.Shared.Implementation;
using piHome.Logic.Shared.Interfaces;
using piHome.Models.Entities.Circuits;
using piHome.Models.Enums;
using piHome.Models.ValueObjects;

namespace piHome.Logic.Tests
{
    [TestClass]
    public class InputCircuitsManagerTests
    {
        [TestMethod]
        public void test_when_circuit_is_on_and_off_for_2_seconds_this_is_logged()
        {
            var circuitRepositoryMock = new Mock<ICircuitsDalHelper>();

            var insertDate = new DateTime(2014, 1, 1, 12, 1, 1);
            var stateEnteredWhenOn = new CircuitStateHistoryEntity
            {
                Circuit = Circuit.C1,
                TurnOnTime = insertDate
            };
            
            circuitRepositoryMock.Setup(r => r.GetLastRowHistoricalState(stateEnteredWhenOn.Circuit))
                                 .Returns(() => stateEnteredWhenOn);

            var dateProviderMock = new Mock<IDateProvider>();
            dateProviderMock.Setup(dp => dp.GetUtcDateTimeDate())
                            .Returns(() => insertDate)
                            .Callback(() => insertDate = insertDate.AddHours(1));
            var eventBroadcasterMock = new Mock<IEventBroadcaster>();

            var manager = new InputCircuitsManager(circuitRepositoryMock.Object, new PinMapper(),
                dateProviderMock.Object, eventBroadcasterMock.Object);

            manager.HandleCircuitChange(true, InputPin.I1);
            circuitRepositoryMock.Verify(r => r.InsertHistory(It.Is<CircuitStateHistoryEntity>(cs => cs.Circuit == Circuit.C1)), Times.Once);
            manager.HandleCircuitChange(false, InputPin.I1);

            circuitRepositoryMock.Verify(r => r.GetLastRowHistoricalState(stateEnteredWhenOn.Circuit), Times.Once);
            circuitRepositoryMock.Verify(r => r.UpdateHistory(It.Is<CircuitStateHistoryEntity>(cs => cs.Circuit == Circuit.C1 && cs.TurnedOnLength == 3600)), Times.Once);
            eventBroadcasterMock.Verify(x => x.BroadcastCircuitStateChange(It.IsAny<StateChange>()), Times.Exactly(2));
        }
    }
}
