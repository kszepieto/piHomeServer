using piHome.GpioWrapper.Enums;

namespace piHome.GpioWrapper
{
    public interface IGpioOutputInterface
    {
        void ChangeCircutState(OutputPin pin);
    }
}