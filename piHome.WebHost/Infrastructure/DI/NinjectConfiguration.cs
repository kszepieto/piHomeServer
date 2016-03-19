using System;
using System.Configuration;
using AutoMapper;
using MongoDB.Driver;
using Ninject;
using piHome.DataAccess;
using piHome.DataAccess.Implementation;
using piHome.DataAccess.Interfaces;
using piHome.Events;
using piHome.GpioWrapper;
using piHome.Logic.Implementation;
using piHome.Logic.Interfaces;
using piHome.Models.Auth;
using piHome.Utils;
using piHome.WebHost.Infrastructure.Mapping;
using piHome.WebHost.WebModels.Auth;

namespace piHome.WebHost.Infrastructure.DI
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

            _kernel.Bind<IDateProvider>().To<DateProvider>().InSingletonScope();
            _kernel.Bind<IEventBroadcaster>().To<EventBroadcaster>().InSingletonScope();

            RegisterGPIOComponents(_kernel);
            RegisterDataAccessComponents(_kernel);
            RegisterAutoMapper(_kernel);


            LogHelper.LogMessage("Kernel initialized");
        }

        private void RegisterGPIOComponents(IKernel kernel)
        {
#if DEBUG
            var gpioInterface = new GpioFakeInterface();
#else
                        var gpioInterface = new GpioInterface();
#endif

            kernel.Bind<IGpioOutputInterface>().ToConstant(gpioInterface);
            kernel.Bind<IGpioInputInterface>().ToConstant(gpioInterface);

            _kernel.Bind<IPinMapper>().To<PinMapper>().InSingletonScope();
            _kernel.Bind<IInputCircuitsManager>().To<InputCircuitsManager>();
            _kernel.Bind<IOutputCircuitsManager>().To<OutputCircuitsManager>();

        }

        private void RegisterDataAccessComponents(IKernel kernel)
        {
            kernel.Bind<IMongoDatabase>().ToConstant(GetDatabase());
            kernel.Bind<IDbContext>().To<DbContext>();
            kernel.Bind<ICircuitsRepository>().To<CircuitsRepository>();
            kernel.Bind<IAuthRepository>().To<AuthRepository>();
        }

        private void RegisterAutoMapper(IKernel kernel)
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile<MappingsProfile>());
            config.AssertConfigurationIsValid();

            kernel.Bind<IMapper>().ToMethod((ctx) => config.CreateMapper());
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
