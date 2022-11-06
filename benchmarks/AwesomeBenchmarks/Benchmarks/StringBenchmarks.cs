using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AwesomeBenchmarks.benchmarks
{
    [MemoryDiagnoser]    
    public class StringBenchmarks
    {
        private readonly string _stringA = "Hello";
        private readonly string _stringB = "World";

        [Benchmark(Baseline = true)]
        public string StringConcat()
        {
            return string.Concat(_stringA, _stringB);
        }

        [Benchmark]
        public string StringFormat()
        {
            return string.Format("{0}{1}", _stringA, _stringB);
        }

        [Benchmark]
        public string StringInterpolation()
        {
            return $"{_stringA}{_stringB}";
        }

        [Benchmark]
        public string StringAdd()
        {
            return _stringA + _stringB;
        }

        [Benchmark]
        public string StringBuilder()
        {
            var builder = new StringBuilder();
            builder.Append(_stringA);
            builder.Append(_stringB);
            return builder.ToString();
        }
    }
}
