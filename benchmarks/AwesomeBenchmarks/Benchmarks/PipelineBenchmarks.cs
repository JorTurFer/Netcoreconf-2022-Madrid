using System;
using System.Buffers;
using System.IO;
using System.IO.Pipelines;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;

namespace AwesomeBenchmarks.Benchmarks;

[MemoryDiagnoser]
public class PipelineBenchmarks
{
    private static ReadOnlySpan<byte> NewLine => new[] { (byte)'\r', (byte)'\n' };
    private Stream _stream;
    
    [GlobalSetup]
    public void GlobalSetup()
    {
        _stream = GetMemoryStreamWithLines(1000);
    }

    [GlobalCleanup]
    public void GlobalCleanup()
    {
        _stream.Dispose();
    }
    
    [Benchmark(Baseline = true)]
    public async Task ReadLineUsingStringReaderAsync()
    {
        _stream.Seek(0, SeekOrigin.Begin);

        var sr = new StreamReader(_stream, Encoding.UTF8);
        while (await sr.ReadLineAsync() is not null)
        {
        }
    }

    [Benchmark]
    public async Task ReadLineUsingPipelineAsync()
    {
        _stream.Seek(0, SeekOrigin.Begin);

        var reader = PipeReader.Create(_stream, new StreamPipeReaderOptions(leaveOpen: true));
        while (true)
        {
            var result = await reader.ReadAsync();
            var buffer = result.Buffer;

            while (ReadLine(ref buffer) is not null)
            {
            }

            reader.AdvanceTo(buffer.Start, buffer.End);

            if (result.IsCompleted) break;
        }

        await reader.CompleteAsync();
    }
    
    private MemoryStream GetMemoryStreamWithLines(int numberOfLines)
    {
        var stream = new MemoryStream();

        using var writer = new StreamWriter(stream, Encoding.UTF8, leaveOpen: true);
        foreach (var no in Enumerable.Range(1, numberOfLines))
        { 
            writer.WriteLine("This is an example");
        }
        writer.Flush();

        return stream;
    }

    private string? ReadLine(ref ReadOnlySequence<byte> buffer)
    {
        var reader = new SequenceReader<byte>(buffer);

        if (reader.TryReadTo(out ReadOnlySpan<byte> line, NewLine))
        {
            buffer = buffer.Slice(reader.Position);
            return Encoding.UTF8.GetString(line);
        }
        
        return default;
    }
}