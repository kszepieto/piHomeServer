using System;
using System.Configuration;
using MongoDB.Driver;
using Ninject;
using piHome.DataAccess;
using piHome.DataAccess.Implementation;
using piHome.DataAccess.Interfaces;
using piHome.Events;
using piHome.GpioWrapper;
using piHome.Logic.Implementation;
using piHome.Logic.Interfaces;
using piHome.Utils;

namespace piHome.WebHost.Infrastructure
{
    public class NinjectConfiguration
    {
        public const string CONNECTION_STRING_NAME = "piHome";
        public const string DATABASE_NAME = "piHome";

        private readonly IKernel _kernel;
        public IKernel Kernel => _kernel;

        private NinjectConfiguration()
        {
            _kernel = new StandardKernel();

#if DEBUG
            var gpioInterface = new GpioFakeInterface();
#else
            var gpioInterface = new GpioInterface();
#endif
            
            _kernel.Bind<IMongoDatabase>().ToConstant(GetDatabase());
            _kernel.Bind<IDbContext>().To<DbContext>();
            _kernel.Bind<ICircuitsRepository>().To<CircuitsRepository>();

            _kernel.Bind<IGpioOutputInterface>().ToConstant(gpioInterface);
            _kernel.Bind<IGpioInputInterface>().ToConstant(gpioInterface);

            _kernel.Bind<IPinMapper>().To<PinMapper>().InSingletonScope();
            _kernel.Bind<IDateProvider>().To<DateProvider>().InSingletonScope();
            _kernel.Bind<IInputCircuitsManager>().To<InputCircuitsManager>();
            _kernel.Bind<IOutputCircuitsManager>().To<OutputCircuitsManager>();
            _kernel.Bind<IEventBroadcaster>().To<EventBroadcaster>().InSingletonScope();

            LogHelper.LogMessage("Kernel initialized");
        }

        private IMongoDatabase GetDatabase()
        {
            var connectionString = ConfigurationManager.ConnectionStrings[CONNECTION_STRING_NAME].ConnectionString;
            var client = new MongoClient(connectionString);
            return client.GetDatabase(DATABASE_NAME);
        }

        #region singleton

        private static NinjectConfiguration _instance;

        public static void Configure()
        {
            if (_instance == null)
            {
                _instance = new NinjectConfiguration();    
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
