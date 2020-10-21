using System;
using Framework.Logging.Model;
using Serilog;
using Serilog.Exceptions;

namespace Framework.Logging
{
    public static class ILoggerBuilderExtensions
    {
        public static Serilog.ILogger CreateLoggerConsole(Action<LoggerOptions> optionBuilder)
        {
            var loggerOptions = new LoggerOptions();
            optionBuilder.Invoke(loggerOptions);

            var logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .Enrich.WithExceptionDetails()
                .Enrich.WithProperty("ApplicationName", loggerOptions.AppName)
                .WriteTo.Console();

            return logger.CreateLogger();
        }
    }
}