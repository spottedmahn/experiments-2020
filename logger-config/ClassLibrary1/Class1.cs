using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace ClassLibrary1
{
    public static class Logging
    {
        public static void AddApiLogging(IServiceCollection services, IConfigurationRoot config)
        {
            services.AddLogging(configure =>
            {
                //wire up logging to configuration
                //pulled from C:\Users\mdepouw\source\repos\GitHub\spottedmahn\AspNetCore\src\DefaultBuilder\src\WebHost.cs
                //I can't find the current location on GitHub
                //todo should verify this the current way to do this
                configure.AddConfiguration(config.GetSection("Logging"));

                //add Console window logging
                configure.AddConsole();

                //add VS output window logging
                configure.AddDebug();
            });
        }
    }
}