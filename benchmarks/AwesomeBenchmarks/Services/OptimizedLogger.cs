using System;
using Microsoft.Extensions.Logging;

namespace AwesomeBenchmarks.Services
{
    public static class OptimizedLogger
    {
        static Exception Exception = new Exception();
        public static void LogInformation(ILogger logger) => _informationSinParametros(logger, Exception);
        public static void LogInformation(ILogger logger,string parametro1) => _information1Parametro(logger,parametro1, Exception);
        public static void LogInformation(ILogger logger, string parametro1, string parametro2) => _information2Parametros(logger,parametro1,parametro2, Exception);


        private static readonly Action<ILogger, Exception> _informationSinParametros = LoggerMessage.Define(
            LogLevel.Information,
            new EventId(100),
            "Mensaje de log sin parámetros");

        private static readonly Action<ILogger,string, Exception> _information1Parametro = LoggerMessage.Define<string>(
            LogLevel.Information,
            new EventId(100),
            "Mensaje de log con 1 parámetro: {Parametro1}");

        private static readonly Action<ILogger,string,string, Exception> _information2Parametros = LoggerMessage.Define<string,string>(
            LogLevel.Information,
            new EventId(100),
            "Mensaje de log con 2 parámetros: {Parametro1} y {Parametro2}");
    }
}