using BenchmarkDotNet.Attributes;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using AwesomeBenchmarks.Services;

namespace PostILogger.Benchmarks
{
    [MemoryDiagnoser]
    public class ILoggerVsOptimized0Params
    {
        private readonly ILogger _logger;

        public ILoggerVsOptimized0Params()
        {
            IServiceCollection serviceCollection = new ServiceCollection();
            serviceCollection.AddLogging();
            var loggerFactory = serviceCollection.BuildServiceProvider().GetService<ILoggerFactory>();
            _logger = loggerFactory.CreateLogger("TEST");
        }

        [Benchmark(Baseline = true)]
        public void Logger0Parametros()
        {
            _logger.LogInformation("Mensaje de log sin parámetros");
        }

        [Benchmark]
        public void OptimizedLogger0Parametros()
        {
            OptimizedLogger.LogInformation(_logger);
        }
    }
}