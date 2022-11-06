using BenchmarkDotNet.Attributes;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using AwesomeBenchmarks.Services;

namespace PostILogger.Benchmarks
{
    [MemoryDiagnoser]
    public class ILoggerVsOptimized1Params
    {
        private readonly ILogger _logger;
        private const string Parametro1 = "Parametro1";

        public ILoggerVsOptimized1Params()
        {
            IServiceCollection serviceCollection = new ServiceCollection();
            serviceCollection.AddLogging();
            var loggerFactory = serviceCollection.BuildServiceProvider().GetService<ILoggerFactory>();
            _logger = loggerFactory.CreateLogger("TEST");
        }

        [Benchmark(Baseline = true)]
        public void Logger1Parametro()
        {
            _logger.LogInformation("Mensaje de log con 1 parámetro: {Parametro1}", Parametro1);
        }

        [Benchmark]
        public void OptimizedLogger1Parametro()
        {
            OptimizedLogger.LogInformation(_logger, Parametro1);
        }
    }
}