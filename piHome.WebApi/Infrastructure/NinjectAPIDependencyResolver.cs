using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Dependencies;
using Ninject;
using piHome.DataAccess.Implementation;
using piHome.DataAccess.Interfaces;
using piHome.GpioWrapper;
using piHome.Logic.Implementation;
using piHome.Logic.Interfaces;

namespace piHome.WebApi.Infrastructure
{
    public class NinjectAPIDependencyResolver : IDependencyResolver
    {
        private readonly IKernel _kernel;

        public NinjectAPIDependencyResolver()
        {
            _kernel = NinjectConfiguration.GetInstance().Kernel;
        }
        
        #region IDependencyResolver

        public IDependencyScope BeginScope()
        {
            return this;
        }

        public object GetService(Type serviceType)
        {
            return _kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return _kernel.GetAll(serviceType);
        }

        public void Dispose()
        {

        }

        #endregion
    }
}
