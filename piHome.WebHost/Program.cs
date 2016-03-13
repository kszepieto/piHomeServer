using System;
using System.Linq;
using Microsoft.Owin.Hosting;
using MongoDB.Driver;
using Ninject;
using piHome.DataAccess;
using piHome.GpioWrapper;
using piHome.GpioWrapper.Enums;
using piHome.Logic.Interfaces;
using piHome.Utils;
using piHome.WebHost.Infrastructure;
using piHome.WebHost.Infrastructure.DI;

namespace piHome.WebHost
{
    class Program
    {
        //resoources
        //vnc - https://www.raspberrypi.org/documentation/remote-access/vnc/
        //installing mongodb - http://www.widriksson.com/install-mongodb-raspberrypi/

        //TODO 2- implement authentication on client
        //TODO 3 - add proper loging -> log4net doesn't work on PI

        const string baseAddress = "http://+:8081/";
        
        static void Main(string[] args)
        {
            NinjectConfiguration.Configure();

            var kernel = NinjectConfiguration.GetInstance().Kernel;
            DbInitializer.Initialize(kernel.Get<IDbContext>()).Wait();

            var inputGpio = kernel.Get<IGpioInputInterface>();
            var inputManager = kernel.Get<IInputCircuitsManager>();
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
                                inputGpio.CircuitStateChanged(state, inputPin);
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
