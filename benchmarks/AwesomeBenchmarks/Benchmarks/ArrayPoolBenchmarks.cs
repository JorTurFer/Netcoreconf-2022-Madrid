using AwesomeBenchmarks.Model;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using System.Buffers;

namespace AwesomeBenchmarks.benchmarks
{
    [MemoryDiagnoser]
    public class ArrayPoolBenchmarks
    {
        private readonly int _lenght = 1000;

        [Benchmark(Baseline = true)]
        public void InitializeWithArray()
        {
            var array = new BigStruct[_lenght];
            for (var i = 0; i > _lenght; i++)
            {
                array[i] = new BigStruct();
            }
            array = null;
        }

        [Benchmark]
        public void InitializeWithArrayPool()
        {
            var array = ArrayPool<BigStruct>.Shared.Rent(_lenght);
            for (var i = 0; i > _lenght; i++)
            {
                array[i] = new BigStruct();
            }
            ArrayPool<BigStruct>.Shared.Return(array);
        }
    }
}
