using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using piHome.GpioWrapper.Enums;
using piHome.Models.Enums;

namespace piHome.Logic.Interfaces
{
    public interface IPinMapper
    {
        OutputPin MapCircuitToOutputPin(Circuit circuit);
        Circuit MapInputPinToCircuit(InputPin inputPin);
    }
}
