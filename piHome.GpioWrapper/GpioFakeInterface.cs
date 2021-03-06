﻿using System;
using piHome.GpioWrapper.Enums;
using piHome.Utils;

namespace piHome.GpioWrapper
{
    public class GpioFakeInterface : IGpioOutputInterface, IGpioInputInterface
    {
        public GpioFakeInterface()
        {
            LogHelper.LogMessage("GpioInterface initialization started");
        }

        #region IGpioInputInterface
        
        public Action<bool, InputPin> CircuitStateChanged { get; set; } 
        
        #endregion

        #region IGpioOutputInterface

        public void ChangeCircutState(OutputPin pin)
        {
            LogHelper.LogMessage("GpioFakeInterface ChangeCircutState invoked: {0}", pin);
        }
 
        #endregion

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
