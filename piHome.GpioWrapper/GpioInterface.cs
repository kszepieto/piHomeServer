using System;
using System.Collections.Generic;
using System.Linq;
using piHome.GpioWrapper.Enums;
using piHome.Utils;
using Raspberry.IO.GeneralPurpose;

namespace piHome.GpioWrapper
{
    public class GpioInterface : IGpioOutputInterface, IGpioInputInterface
    {
        #region private

        private const int BlinkDurationMilis = 500;
        private readonly GpioConnection gpioConnection;

        #endregion

        public GpioInterface()
        {
            LogHelper.LogMessage("GpioInterface initialization started: {0}", DateTime.Now);

            gpioConnection = new GpioConnection(new List<PinConfiguration>
            {
                //configure output pins
                MapOutputPin(OutputPin.O1).Input(),
                MapOutputPin(OutputPin.O2).Input(),
                MapOutputPin(OutputPin.O3).Input(),
                MapOutputPin(OutputPin.O4).Input(),
                MapOutputPin(OutputPin.O5).Input(),
                MapOutputPin(OutputPin.O6).Input(),
                MapOutputPin(OutputPin.O7).Input(),
                MapOutputPin(OutputPin.O8).Input(),
                MapOutputPin(OutputPin.O9).Input(),
                MapOutputPin(OutputPin.O10).Input(),

                //configure input pins
                MapInputPin(InputPin.I1).Input(),
                MapInputPin(InputPin.I2).Input(),
                MapInputPin(InputPin.I3).Input(),
                MapInputPin(InputPin.I4).Input(),
                MapInputPin(InputPin.I5).Input(),
                MapInputPin(InputPin.I6).Input(),
                MapInputPin(InputPin.I7).Input(),
                MapInputPin(InputPin.I8).Input(),
                MapInputPin(InputPin.I9).Input(),
                MapInputPin(InputPin.I10).Input(),
            });

            gpioConnection.PinStatusChanged += PinStatusChanged;

            LogHelper.LogMessage("GpioInterface initialization finished");
        }

        #region IGpioOutputInterface

        private readonly List<Tuple<OutputPin, ConnectorPin>> _outputPins = new List<Tuple<OutputPin, ConnectorPin>>
            {
                new Tuple<OutputPin, ConnectorPin>(OutputPin.O1, ConnectorPin.P1Pin29),
                new Tuple<OutputPin, ConnectorPin>(OutputPin.O2, ConnectorPin.P1Pin31),
                new Tuple<OutputPin, ConnectorPin>(OutputPin.O3, ConnectorPin.P1Pin33),
                new Tuple<OutputPin, ConnectorPin>(OutputPin.O4, ConnectorPin.P1Pin35),
                new Tuple<OutputPin, ConnectorPin>(OutputPin.O5, ConnectorPin.P1Pin37),
                new Tuple<OutputPin, ConnectorPin>(OutputPin.O6, ConnectorPin.P1Pin40),
                new Tuple<OutputPin, ConnectorPin>(OutputPin.O7, ConnectorPin.P1Pin38),
                new Tuple<OutputPin, ConnectorPin>(OutputPin.O8, ConnectorPin.P1Pin36),
                new Tuple<OutputPin, ConnectorPin>(OutputPin.O9, ConnectorPin.P1Pin32),
                new Tuple<OutputPin, ConnectorPin>(OutputPin.O10, ConnectorPin.P1Pin22)
            };

        public void ChangeCircutState(OutputPin pin)
        {
            LogHelper.LogMessage("GpioInterface ChangeCircutState invoked: {0} - {1}", DateTime.Now, pin);

            var connectorPin = MapOutputPin(pin);
            gpioConnection.Blink(connectorPin, BlinkDurationMilis);
        }

        private ConnectorPin MapOutputPin(OutputPin outputPin)
        {

            var pin = _outputPins.SingleOrDefault(ip => ip.Item1 == outputPin);
            if (pin == null)
            {
                var errMsg = $"Invalid value: {outputPin}";
                throw new ArgumentOutOfRangeException(errMsg);
            }

            return pin.Item2;
        }

        #endregion

        #region IGpioInputInterface
        
        public Action<bool, InputPin> CircuitStateChanged { get; set; }

        private readonly List<Tuple<InputPin, ConnectorPin>> _inputPins = new List<Tuple<InputPin, ConnectorPin>>
            {
                new Tuple<InputPin, ConnectorPin>(InputPin.I1, ConnectorPin.P1Pin03),
                new Tuple<InputPin, ConnectorPin>(InputPin.I2, ConnectorPin.P1Pin05),
                new Tuple<InputPin, ConnectorPin>(InputPin.I3, ConnectorPin.P1Pin7),
                new Tuple<InputPin, ConnectorPin>(InputPin.I4, ConnectorPin.P1Pin11),
                new Tuple<InputPin, ConnectorPin>(InputPin.I5, ConnectorPin.P1Pin13),
                new Tuple<InputPin, ConnectorPin>(InputPin.I6, ConnectorPin.P1Pin15),
                new Tuple<InputPin, ConnectorPin>(InputPin.I7, ConnectorPin.P1Pin16),
                new Tuple<InputPin, ConnectorPin>(InputPin.I8, ConnectorPin.P1Pin12),
                new Tuple<InputPin, ConnectorPin>(InputPin.I9, ConnectorPin.P1Pin10),
                new Tuple<InputPin, ConnectorPin>(InputPin.I10, ConnectorPin.P1Pin8)
            };

        private void PinStatusChanged(object sender, PinStatusEventArgs e)
        {
            var connectorPin = e.Configuration.Pin.ToConnector();
            var inputPin = MapConnectorInputPin(connectorPin);

            if (inputPin != null && CircuitStateChanged != null)
            {
                CircuitStateChanged(e.Enabled, inputPin.Value);
            }
            else
            {
                LogHelper.LogMessage("Pin changed {0}", connectorPin);
            }
        }
        
        private ConnectorPin MapInputPin(InputPin inputPin)
        {
            var pin = _inputPins.SingleOrDefault(ip => ip.Item1 == inputPin);
            if (pin == null)
            {
                var errMsg = string.Format("Invalid value: {0}", inputPin);
                throw new ArgumentOutOfRangeException(errMsg);
            }

            return pin.Item2;
        }

        private InputPin? MapConnectorInputPin(ConnectorPin inputPin)
        {
            var pin = _inputPins.SingleOrDefault(ip => ip.Item2 == inputPin);
            if (pin != null)
            {
                return pin.Item1;
            }

            return null;
        }

        #endregion

        #region IDisposable

        public void Dispose()
        {
            if (gpioConnection != null && gpioConnection.IsOpened)
            {
                gpioConnection.Close();
            }
        }
        
        #endregion
    }
}
