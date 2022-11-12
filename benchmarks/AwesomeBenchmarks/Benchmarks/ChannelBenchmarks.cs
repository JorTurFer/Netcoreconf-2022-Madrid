using System.Collections.Concurrent;
using System.Threading.Tasks.Dataflow;

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
        for (var i = 1; i < 10_000_000; i++)
        {
            _channel.Writer.TryWrite(i);
            await _channel.Reader.ReadAsync();
        }
    }

    [Benchmark]
    public void BufferBlock_ReadThenWrite()
    {
        for (var i = 1; i < 10_000_000; i++)
        {
            _blockingCollection.Add(i);
            _blockingCollection.Take();
        }
    }
}