using System;
using piHome.GpioWrapper.Enums;

namespace piHome.GpioWrapper
{
    public interface IGpioOutputInterface : IDisposable
    {
        void ChangeCircutState(OutputPin pin);
    }
}