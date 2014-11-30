using piHome.GpioWrapper.Enums;

namespace piHome.Logic.Interfaces
{
    public interface IInputCircuitsManager
    {
        void HandleCircuitChange(bool state, InputPin inputPin);
    }
}
