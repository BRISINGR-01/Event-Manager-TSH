namespace Web.Middlewares.Errors
{
    public class ErrorLogger : ILogger
    {
        private readonly string _name;
        private readonly Func<ErrorLoggerConfiguration> _getCurrentConfig;

        public ErrorLogger(string name, Func<ErrorLoggerConfiguration> getCurrentConfig)
        {
            _name = name;
            _getCurrentConfig = getCurrentConfig;
        }

        public IDisposable BeginScope<TState>(TState state) => default!;

        public bool IsEnabled(LogLevel logLevel) =>
            _getCurrentConfig().LogLevelToColorMap.ContainsKey(logLevel);

        public void Log<TState>(
            LogLevel logLevel,
            EventId eventId,
            TState state,
            Exception? exception,
            Func<TState, Exception?, string> formatter)
        {


            if (exception != null)
            {
                using StreamWriter writer = new("./Log.txt", true);
                writer.WriteLine(exception.Message);
            }
        }
    }
}
