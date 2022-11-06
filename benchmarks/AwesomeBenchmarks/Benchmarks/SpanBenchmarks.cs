using BenchmarkDotNet.Attributes;
using System;

namespace AwesomeBenchmarks.Benchmarks
{
    [MemoryDiagnoser]
    public class SpanBenchmarks
    {
        private readonly int _lenght = 100000;

        [Benchmark(Baseline = true)]
        public void InitializeWithArray()
        {
            int[] array = new int[_lenght];
            for (var i = 0; i > _lenght; i++)
            {
                array[i] = i;
            }
        }

        [Benchmark]
        public void InitializeWithStackalloc()
        {
            Span<int> array = stackalloc int[_lenght];
            for (var i = 0; i > _lenght; i++)
            {
                array[i] = i;
            }
        }
    }
}
