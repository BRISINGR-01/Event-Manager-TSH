using Microsoft.Extensions.Options;
using System.Collections.Concurrent;
using System.Runtime.Versioning;

namespace Web.Middlewares.Errors
{
    [UnsupportedOSPlatform("browser")]
    [ProviderAlias("ErrorConsole")]
    public sealed class ErrorLoggerProvider : ILoggerProvider
    {
        private readonly IDisposable? _onChangeToken;
        private ErrorLoggerConfiguration _currentConfig;
        private readonly ConcurrentDictionary<string, ErrorLogger> _loggers =
            new(StringComparer.OrdinalIgnoreCase);

        public ErrorLoggerProvider(
            IOptionsMonitor<ErrorLoggerConfiguration> config)
        {
            _currentConfig = config.CurrentValue;
            _onChangeToken = config.OnChange(updatedConfig => _currentConfig = updatedConfig);
        }

        public ILogger CreateLogger(string categoryName) =>
            _loggers.GetOrAdd(categoryName, name => new ErrorLogger(name, GetCurrentConfig));

        private ErrorLoggerConfiguration GetCurrentConfig() => _currentConfig;

        public void Dispose()
        {
            _loggers.Clear();
            _onChangeToken?.Dispose();
        }
    }
}
