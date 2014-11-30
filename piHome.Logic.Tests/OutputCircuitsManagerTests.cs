using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using piHome.DataAccess.Interfaces;
using piHome.GpioWrapper;
using piHome.GpioWrapper.Enums;
using piHome.Logic.Implementation;
using piHome.Models.Enums;
using piHome.WebApi.Models;

namespace piHome.Logic.Tests
{
    [TestClass]
    public class OutputCircuitsManagerTests
    {
        [TestMethod]
        public void SwitchCircuit_do_nothing_when_circuit_status_is_same_as_requested()
        {
            var gpioOutputInterfaceMock = new Mock<IGpioOutputInterface>();
            gpioOutputInterfaceMock.Setup(gi => gi.ChangeCircutState(It.IsAny<OutputPin>())).Verifiable();

            var circuitRepositoryMock = new Mock<ICircuitsRepository>();
            circuitRepositoryMock.Setup(r => r.GetCircuitState(It.IsAny<Circuit>())).Returns(true);

            var circuitManager = new OutputCircuitsManager(gpioOutputInterfaceMock.Object, circuitRepositoryMock.Object, new PinMapper());
            circuitManager.SwitchCircuit(Circuit.C1, true);

            gpioOutputInterfaceMock.Verify(gpio => gpio.ChangeCircutState(OutputPin.O1), Times.Never);
        }

        [TestMethod]
        public void SwitchCircuit_invokes_ChangeCircutState_when_circuit_status_needs_to_change()
        {
            var gpioOutputInterfaceMock = new Mock<IGpioOutputInterface>();
            gpioOutputInterfaceMock.Setup(gi => gi.ChangeCircutState(It.IsAny<OutputPin>())).Verifiable();

            var circuitRepositoryMock = new Mock<ICircuitsRepository>();
            circuitRepositoryMock.Setup(r => r.GetCircuitState(It.IsAny<Circuit>())).Returns(true);

            var circuitManager = new OutputCircuitsManager(gpioOutputInterfaceMock.Object, circuitRepositoryMock.Object, new PinMapper());
            circuitManager.SwitchCircuit(Circuit.C1, false);

            gpioOutputInterfaceMock.Verify(gpio => gpio.ChangeCircutState(OutputPin.O1), Times.Once);
        }
    }
}
