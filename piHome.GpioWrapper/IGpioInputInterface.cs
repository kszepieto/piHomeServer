using System;
using piHome.GpioWrapper.Enums;

namespace piHome.GpioWrapper
{
    public interface IGpioInputInterface
    {
        [Obsolete]
        void InvokeCircuitStateChangedManually(bool state, InputPin inputPin);

        Action<bool, InputPin> CircuitStateChanged { get; set; }
    }
}