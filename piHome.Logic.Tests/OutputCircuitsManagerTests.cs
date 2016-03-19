using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using piHome.DataAccess.Interfaces;
using piHome.GpioWrapper;
using piHome.GpioWrapper.Enums;
using piHome.Logic.Implementation;
using piHome.Models.Circuit;
using piHome.Models.Circuit.Enums;

namespace piHome.Logic.Tests
{
    [TestClass]
    public class OutputCircuitsManagerTests
    {
        [TestMethod]
        public void SwitchCircuit_do_nothing_when_circuit_status_is_same_as_requested()
        {
            var gpioOutputInterfaceMock = new Mock<IGpioOutputInterface>();

            var circuitRepositoryMock = new Mock<ICircuitsRepository>();
            circuitRepositoryMock.Setup(r => r.GetCircuitState(It.IsAny<Circuit>())).Returns(true);

            var circuitManager = new OutputCircuitsManager(gpioOutputInterfaceMock.Object, circuitRepositoryMock.Object, new PinMapper());
            circuitManager.SwitchCircuit(new StateChange {Circuit = Circuit.C1, State = true});

            gpioOutputInterfaceMock.Verify(gpio => gpio.ChangeCircutState(OutputPin.O1), Times.Never);
        }

        [TestMethod]
        public void SwitchCircuit_invokes_ChangeCircutState_when_circuit_status_needs_to_change()
        {
            var gpioOutputInterfaceMock = new Mock<IGpioOutputInterface>();

            var circuitRepositoryMock = new Mock<ICircuitsRepository>();
            circuitRepositoryMock.Setup(r => r.GetCircuitState(It.IsAny<Circuit>())).Returns(true);

            var circuitManager = new OutputCircuitsManager(gpioOutputInterfaceMock.Object, circuitRepositoryMock.Object, new PinMapper());
            circuitManager.SwitchCircuit(new StateChange { Circuit = Circuit.C1, State = false });

            gpioOutputInterfaceMock.Verify(gpio => gpio.ChangeCircutState(OutputPin.O1), Times.Once);
        }
    }
}
