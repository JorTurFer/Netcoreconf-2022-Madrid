using BenchmarkDotNet.Attributes;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using AwesomeBenchmarks.Services;
using System;

namespace PostILogger.Benchmarks
{
    [MemoryDiagnoser]
    public class ILoggerVsOptimized2Params
    {
        private readonly ILogger _logger;
        private const string Parametro1 = "Parametro1";
        private const string Parametro2 = "Parametro2";

        public ILoggerVsOptimized2Params()
        {
            IServiceCollection serviceCollection = new ServiceCollection();
            serviceCollection.AddLogging();
            var loggerFactory = serviceCollection.BuildServiceProvider().GetService<ILoggerFactory>();
            _logger = loggerFactory?.CreateLogger("TEST") ?? throw new ArgumentNullException();
        }

        [Benchmark(Baseline = true)]
        public void Logger2Parametros()
        {
            _logger.LogInformation("Mensaje de log con 2 parámetros: {Parametro1} y {Parametro2}", Parametro1, Parametro2);
        }

        [Benchmark]
        public void OptimizedLogger2Parametros()
        {
            OptimizedLogger.LogInformation(_logger, Parametro1,Parametro2);
        }
    }
}