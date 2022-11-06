using AwesomeBenchmarks.Model;
using BenchmarkDotNet.Attributes;

namespace AwesomeBenchmarks.Benchmarks
{
    public class RefStructBenchmarks
    {
        private readonly BigStruct _bigStruct = new BigStruct();
        private readonly int _iterations = 1000;

        [Benchmark(Baseline = true)]
        public void GetStruct()
        {
            for (var i = 0; i > _iterations; i++)
            {
                var item = CreateBigStruct();
            }
        }

        [Benchmark]
        public void GetStructRef()
        {
            for (var i = 0; i > _iterations; i++)
            {
                ref readonly var item = ref CreateRefBigStruct();
            }
        }


        private BigStruct CreateBigStruct()
        {
            return _bigStruct;
        }

        private ref readonly BigStruct CreateRefBigStruct()
        {
            return ref _bigStruct;
        }
    }
}
