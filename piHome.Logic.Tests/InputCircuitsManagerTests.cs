using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using piHome.DataAccess.Entities;
using piHome.DataAccess.Interfaces;
using piHome.Events;
using piHome.GpioWrapper.Enums;
using piHome.Logic.Implementation;
using piHome.Logic.Interfaces;
using piHome.Models;
using piHome.Models.Enums;

namespace piHome.Logic.Tests
{
    [TestClass]
    public class InputCircuitsManagerTests
    {
        [TestMethod]
        public void test_when_circuit_is_on_and_off_for_2_seconds_this_is_logged()
        {
            var circuitRepositoryMock = new Mock<ICircuitsRepository>();

            var insertDate = new DateTime(2014, 1, 1, 12, 1, 1);
            var stateEnteredWhenOn = new CircuitStateHistory
            {
                Circuit = Circuit.C1,
                TurnOnTime = insertDate
            };

            circuitRepositoryMock.Setup(r => r.Insert(stateEnteredWhenOn));
            circuitRepositoryMock.Setup(r => r.Update(stateEnteredWhenOn));
            circuitRepositoryMock.Setup(r => r.GetLastRowHistoricalState(stateEnteredWhenOn.Circuit))
                                 .Returns(() => stateEnteredWhenOn);

            var dateProviderMock = new Mock<IDateProvider>();
            dateProviderMock.Setup(dp => dp.GetDate())
                            .Returns(() => insertDate)
                            .Callback(() => insertDate = insertDate.AddHours(1));
            var eventBroadcasterMock = new Mock<IEventBroadcaster>();

            var manager = new InputCircuitsManager(circuitRepositoryMock.Object, new PinMapper(),
                dateProviderMock.Object, eventBroadcasterMock.Object);

            manager.HandleCircuitChange(true, InputPin.I1);
            circuitRepositoryMock.Verify(r => r.Insert(It.Is<CircuitStateHistory>(cs => cs.Circuit == Circuit.C1)), Times.Once);

            manager.HandleCircuitChange(false, InputPin.I1);
            circuitRepositoryMock.Verify(r => r.GetLastRowHistoricalState(stateEnteredWhenOn.Circuit), Times.Once);
            circuitRepositoryMock.Verify(r => r.Update(It.Is<CircuitStateHistory>(cs => cs.Circuit == Circuit.C1 && cs.TurnedOnLength == 3600)), Times.Once);
            eventBroadcasterMock.Verify(x => x.BroadcastCircuitStateChange(It.IsAny<CircuitStateChange>()), Times.Once);
        }
    }
}
