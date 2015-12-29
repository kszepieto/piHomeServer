using System;
using System.Linq;
using Microsoft.Owin.Hosting;
using Ninject;
using piHome.DataAccess.Implementation;
using piHome.GpioWrapper;
using piHome.GpioWrapper.Enums;
using piHome.Logic.Interfaces;
using piHome.Utils;
using piHome.WebHost.Infrastructure;

namespace piHome.WebHost
{
    class Program
    {
        const string baseAddress = "http://+:8081/";
        private const string piHomeDb = "piHome.db";

        //TODO BBB catch exceptions from pins monitor and logging

        static void Main(string[] args)
        {
            var db = new SqlLiteDb(piHomeDb);
            db.InitializeDB();
            NinjectConfiguration.Configure(db);

            var inputGpio = NinjectConfiguration.GetInstance().Kernel.Get<IGpioInputInterface>();
            var inputManager = NinjectConfiguration.GetInstance().Kernel.Get<IInputCircuitsManager>();
            inputGpio.CircuitStateChanged = inputManager.HandleCircuitChange;
            
            LogHelper.LogMessage("Input GPIO: " + baseAddress);

            using (WebApp.Start<WebApiStartup>(url: baseAddress))
            {
                LogHelper.LogMessage("Host started on: " + baseAddress);
#if DEBUG
                while (true)
                {
                    var message = Console.ReadLine();
                    if (!string.IsNullOrEmpty(message))
                    {
                        var arguments = message.Split(':');
                        if (arguments.Count() == 2)
                        {
                            InputPin inputPin;
                            bool state;

                            var pinParsedSucessfull = Enum.TryParse(arguments[0], out inputPin);
                            var stateParsedSucessfull = Boolean.TryParse(arguments[1], out state);

                            if (pinParsedSucessfull && stateParsedSucessfull)
                            {
                                inputGpio.InvokeCircuitStateChangedManually(state, inputPin);
                            }
                        }
                    }
                }
#else
                Console.ReadLine();
#endif
            }
        }
    }
}
