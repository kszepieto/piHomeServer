using Microsoft.VisualStudio.TestTools.UnitTesting;
using piHome.GpioWrapper.Enums;
using piHome.Logic.Shared.Implementation;
using piHome.Models.Enums;

namespace piHome.Logic.Tests
{
    [TestClass]
    public class PinMapperTest
    {
        [TestMethod]
        public void Check_mappings_MapCircuitToOutputPin()
        {
            var mapper = new PinMapper();
            Assert.AreEqual(mapper.MapCircuitToOutputPin(Circuit.C1), OutputPin.O1);
            Assert.AreEqual(mapper.MapCircuitToOutputPin(Circuit.C2), OutputPin.O2);
            Assert.AreEqual(mapper.MapCircuitToOutputPin(Circuit.C3), OutputPin.O3);
            Assert.AreEqual(mapper.MapCircuitToOutputPin(Circuit.C4), OutputPin.O4);
            Assert.AreEqual(mapper.MapCircuitToOutputPin(Circuit.C5), OutputPin.O5);
            Assert.AreEqual(mapper.MapCircuitToOutputPin(Circuit.C6), OutputPin.O6);
            Assert.AreEqual(mapper.MapCircuitToOutputPin(Circuit.C7), OutputPin.O7);
            Assert.AreEqual(mapper.MapCircuitToOutputPin(Circuit.C8), OutputPin.O8);
            Assert.AreEqual(mapper.MapCircuitToOutputPin(Circuit.C9), OutputPin.O9);
            Assert.AreEqual(mapper.MapCircuitToOutputPin(Circuit.C10), OutputPin.O10); 
        }

        public void Check_mappings_MapInputPinToCircuit()
        {
            var mapper = new PinMapper();
            Assert.AreEqual(mapper.MapInputPinToCircuit(InputPin.I1),Circuit.C1);
            Assert.AreEqual(mapper.MapInputPinToCircuit(InputPin.I2),Circuit.C2);
            Assert.AreEqual(mapper.MapInputPinToCircuit(InputPin.I3),Circuit.C3);
            Assert.AreEqual(mapper.MapInputPinToCircuit(InputPin.I4),Circuit.C4);
            Assert.AreEqual(mapper.MapInputPinToCircuit(InputPin.I5),Circuit.C5);
            Assert.AreEqual(mapper.MapInputPinToCircuit(InputPin.I6),Circuit.C6);
            Assert.AreEqual(mapper.MapInputPinToCircuit(InputPin.I7),Circuit.C7);
            Assert.AreEqual(mapper.MapInputPinToCircuit(InputPin.I8),Circuit.C8);
            Assert.AreEqual(mapper.MapInputPinToCircuit(InputPin.I9),Circuit.C9);
            Assert.AreEqual(mapper.MapInputPinToCircuit(InputPin.I10), Circuit.C10);
        }
    }
}
