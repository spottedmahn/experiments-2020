﻿using ClassLibrary1;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ModernConsoleAppTemplate.SampleService;
using System;
using System.IO;

namespace ModernConsoleAppTemplate
{
    public class CoreServices
    {
        public static ServiceProvider AddDotCoreCoreServices()
        {
            var configurationRoot = InitConfig();

            var serviceProvider = InitDependencyInjection(configurationRoot);

            return serviceProvider;
        }

        private static IConfigurationRoot InitConfig()
        {
            var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            var builder = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                    .AddJsonFile($"appsettings.{env}.json", optional: false, reloadOnChange: true)
                    .AddEnvironmentVariables();

            return builder.Build();
        }

        private static ServiceProvider InitDependencyInjection(IConfigurationRoot configurationRoot)
        {
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection, configurationRoot);

            var serviceProvider = serviceCollection.BuildServiceProvider();
            return serviceProvider;
        }

        private static void ConfigureServices(IServiceCollection services, IConfigurationRoot config)
        {
            Logging.AddMyLogging(services, config);

            //add custom services here
            services.AddTransient<IMyService, MyService>();

            //add custom configuration / options
            services.Configure<MyOptions>(config.GetSection(nameof(MyOptions)));
        }
    }

}
