using BenchmarkDotNet.Attributes;
using System.Text;

namespace AwesomeBenchmarks.benchmarks
{
    [MemoryDiagnoser]
    public class StringWithIterationsBenchmarks
    {
        private readonly string _stringA = "Hello";
        private readonly string _stringB = "World";
        private readonly int _ciclos = 100000;

        [Benchmark(Baseline = true)]
        public string StringConcat()
        {
            var result = _stringA;
            for (var i = 0; i < _ciclos; i++)
            {
                result = string.Concat(result, _stringA);
            }
            return result;
        }

        [Benchmark]
        public string StringFormat()
        {
            var result = _stringA;
            for (var i = 0; i < _ciclos; i++)
            {
                result = string.Format("{0}{1}", result, _stringB);
            }
            return result;
        }

        [Benchmark]
        public string StringInterpolation()
        {
            var result = _stringA;
            for (var i = 0; i < _ciclos; i++)
            {
                result = $"{result}{_stringB}";
            }
            return result;
        }

        [Benchmark]
        public string StringAdd()
        {
            var result = _stringA;
            for (var i = 0; i < _ciclos; i++)
            {
                result += _stringB;
            }
            return result;
        }

        [Benchmark]
        public string StringBuilder()
        {
            var builder = new StringBuilder();
            builder.Append(_stringA);
            for (var i = 0; i < _ciclos; i++)
            {
                builder.Append(_stringB);
            }
            return builder.ToString();
        }
    }
}
