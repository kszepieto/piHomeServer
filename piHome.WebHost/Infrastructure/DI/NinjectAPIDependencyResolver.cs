using System;
using System.Collections.Generic;
using System.Web.Http.Dependencies;
using Ninject;

namespace piHome.WebHost.Infrastructure.DI
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
