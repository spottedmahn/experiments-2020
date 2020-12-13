using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog_Client_Info_Enricher_Debug.Controllers;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Serilog_Client_Info_Enricher_Debug
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            GlobalConfiguration.Configure(Configure);
        }

        private void Configure(HttpConfiguration httpConfiguration)
        {
            InitLogging();
            InitDependencyInjection(httpConfiguration);
        }

        private void InitLogging()
        {
            var loggerConfig = new LoggerConfiguration()
                               .Enrich.FromLogContext()
                               .Enrich.WithClientIp()
                               .Enrich.WithClientAgent()
                               .WriteTo.Debug(outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj} " +
                                     "{Properties:j}{NewLine}{Exception}");
            Log.Logger = loggerConfig.CreateLogger();
        }

        private void InitDependencyInjection(HttpConfiguration httpConfiguration)
        {
            var resolver = new DefaultDependencyResolver(CreateServiceProvider());
            httpConfiguration.DependencyResolver = resolver;
            var debug = httpConfiguration.DependencyResolver.GetService(typeof(HomeController));
            //interesting I had to do this
            //but in our real project we're using
            //httpConfiguration.DependencyResolver 🤔
            DependencyResolver.SetResolver(resolver);
        }

        private static ServiceProvider CreateServiceProvider()
        {
            return new ServiceCollection()
                .AddLogging(loggingBuilder =>
                {
                    loggingBuilder.AddSerilog(dispose: true);
                })
                .AddTransient<HomeController>()
                .BuildServiceProvider(); ;
        }
    }
}
