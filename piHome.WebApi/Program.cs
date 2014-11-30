using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Owin.Hosting;
using Ninject;
using log4net.Config;
using piHome.DataAccess.Implementation;
using piHome.GpioWrapper;
using piHome.GpioWrapper.Enums;
using piHome.Logic.Interfaces;
using piHome.Utils;
using piHome.WebApi;
using piHome.WebApi.Infrastructure;

namespace piHome.Host
{
    class Program
    {
        const string baseAddress = "http://+:8081/";
        private const string piHomeDb = "piHome.db";

        //TODO BBB catch exceptions from pins monitor and logging

        static void Main(string[] args)
        {
            XmlConfigurator.Configure();

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
