using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Web.Http.Dependencies;

namespace Serilog_Client_Info_Enricher_Debug
{
    public class DefaultDependencyResolver : IDependencyResolver, System.Web.Mvc.IDependencyResolver
    {
        private readonly IServiceProvider provider;

        public DefaultDependencyResolver(IServiceProvider provider)
        {
            this.provider = provider;
        }

        public object GetService(Type serviceType)
        {
            return provider.GetService(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return provider.GetServices(serviceType);
        }

        public IDependencyScope BeginScope()
        {
            return this;
        }

        public void Dispose()
        {
        }
    }
}