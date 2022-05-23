﻿using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ModernConsoleAppTemplate.SampleService;

namespace ModernConsoleAppTemplate
{
    class Program
    {
        static ServiceProvider serviceProvider;

        static void Main(string[] args)
        {
            serviceProvider = CoreServices.AddDotCoreCoreServices();

            //testing logging and DI
            var logger = serviceProvider.GetService<ILogger<Program>>();

            logger.LogTrace("Hello World Trace");

            logger.LogInformation("Hello World Info");

            logger.LogDebug("Hello World Debug");

            logger.LogWarning("Hello World Warning");

            logger.LogError("Hello World Error");

            logger.LogCritical("Hello World Critical");

            //get the thing(s) I want to do work
            var myService = serviceProvider.GetService<IMyService>();
            myService.DoWork();

            //have to have this for logging to show up in the console
            //I would assume this would go away once I see some real work
            //Console.ReadLine();

            //https://github.com/serilog/serilog-extensions-logging-file/issues/16#issuecomment-395975751
            serviceProvider.Dispose();
        }
    }
}
