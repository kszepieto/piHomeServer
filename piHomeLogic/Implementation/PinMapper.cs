using System;
using piHome.GpioWrapper.Enums;
using piHome.Logic.Interfaces;
using piHome.Models.Enums;

namespace piHome.Logic.Implementation
{
    public class PinMapper : IPinMapper
    {
        public OutputPin MapCircuitToOutputPin(Circuit circuit)
        {
            switch (circuit)
            {
                case Circuit.C1:
                    return OutputPin.O1;
                case Circuit.C2:
                    return OutputPin.O2;
                case Circuit.C3:
                    return OutputPin.O3;
                case Circuit.C4:
                    return OutputPin.O4;
                case Circuit.C5:
                    return OutputPin.O5;
                case Circuit.C6:
                    return OutputPin.O6;
                case Circuit.C7:
                    return OutputPin.O7;
                case Circuit.C8:
                    return OutputPin.O8;
                case Circuit.C9:
                    return OutputPin.O9;
                case Circuit.C10:
                    return OutputPin.O10;
                default:
                    throw new ArgumentOutOfRangeException("circuit");
            }
        }

        public Circuit MapInputPinToCircuit(InputPin inputPin)
        {
            switch (inputPin)
            {
                case InputPin.I1:
                    return Circuit.C1;
                case InputPin.I2:
                    return Circuit.C2;
                case InputPin.I3:
                    return Circuit.C3;
                case InputPin.I4:
                    return Circuit.C4;
                case InputPin.I5:
                    return Circuit.C5;
                case InputPin.I6:
                    return Circuit.C6;
                case InputPin.I7:
                    return Circuit.C7;
                case InputPin.I8:
                    return Circuit.C8;
                case InputPin.I9:
                    return Circuit.C9;
                case InputPin.I10:
                    return Circuit.C10;
                default:
                    throw new ArgumentOutOfRangeException("inputPin");
            }
        }
    }
}