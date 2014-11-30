using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject;
using piHome.DataAccess.Implementation;
using piHome.DataAccess.Interfaces;
using piHome.GpioWrapper;
using piHome.Logic.Implementation;
using piHome.Logic.Interfaces;
using piHome.Utils;

namespace piHome.WebApi.Infrastructure
{
    public class NinjectConfiguration
    {
        private readonly IKernel _kernel;
        public IKernel Kernel { get { return _kernel; } }

        private NinjectConfiguration(SqlLiteDb db)
        {
            _kernel = new StandardKernel();
#if DEBUG
            var gpioInterface = new GpioFakeInterface();
#else
            var gpioInterface = new GpioInterface();
#endif
            _kernel.Bind<IGpioOutputInterface>().ToConstant(gpioInterface);
            _kernel.Bind<IGpioInputInterface>().ToConstant(gpioInterface);

            _kernel.Bind<SqlLiteDb>().ToConstant(db);
            _kernel.Bind<ICircuitsRepository>().To<CircuitsRepository>();

            _kernel.Bind<IPinMapper>().To<PinMapper>();
            _kernel.Bind<IDateProvider>().To<DateProvider>();
            _kernel.Bind<IInputCircuitsManager>().To<InputCircuitsManager>();
            _kernel.Bind<IOutputCircuitsManager>().To<OutputCircuitsManager>();

            LogHelper.LogMessage("Kernel initialized");
        }

        #region singleton

        private static NinjectConfiguration _instance;

        public static void Configure(SqlLiteDb db)
        {
            if (_instance == null)
            {
                _instance = new NinjectConfiguration(db);    
            }
        }

        public static NinjectConfiguration GetInstance()
        {
            if (_instance == null)
            {
                throw new ApplicationException("NinjectConfiguration not initialized");
            }

            return _instance;
        }
 
        #endregion
    }
}
