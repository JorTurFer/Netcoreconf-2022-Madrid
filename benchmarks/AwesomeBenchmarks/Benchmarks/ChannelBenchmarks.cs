using System.Collections.Concurrent;

namespace AwesomeBenchmarks.Benchmarks;

using BenchmarkDotNet.Attributes;
using System.Threading.Channels;
using System.Threading.Tasks;

[MemoryDiagnoser]
public class ChannelBenchmarks
{
    private readonly Channel<int> _channel = Channel.CreateUnbounded<int>();
    private readonly BlockingCollection<int> _blockingCollection = new();


    [Benchmark]
    public async Task Channel_ReadThenWrite()
    {
        _channel.Writer.TryWrite(0);
        await _channel.Reader.ReadAsync();
    }

    [Benchmark(Baseline = true)]
    public void BufferBlock_ReadThenWrite()
    {
        _blockingCollection.Add(0);
        _blockingCollection.Take();
    }
}