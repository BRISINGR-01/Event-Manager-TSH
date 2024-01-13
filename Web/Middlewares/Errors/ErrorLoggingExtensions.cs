using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging.Configuration;

namespace Web.Middlewares.Errors
{
    public static class ErrorLoggingExtensions
    {
        public static ILoggingBuilder AddErrorLogger(
        this ILoggingBuilder builder)
        {
            builder.AddConfiguration();

            builder.Services.TryAddEnumerable(
                ServiceDescriptor.Singleton<ILoggerProvider, ErrorLoggerProvider>());

            LoggerProviderOptions.RegisterProviderOptions
                <ErrorLoggerConfiguration, ErrorLoggerProvider>(builder.Services);

            return builder;
        }

        public static ILoggingBuilder AddErrorLogger(
            this ILoggingBuilder builder,
            Action<ErrorLoggerConfiguration> configure)
        {
            builder.AddErrorLogger();
            builder.Services.Configure(configure);

            return builder;
        }
    }
}
