using System;
using piHome.GpioWrapper.Enums;

namespace piHome.GpioWrapper
{
    public interface IGpioInputInterface
    {
        Action<bool, InputPin> CircuitStateChanged { get; set; }
    }
}