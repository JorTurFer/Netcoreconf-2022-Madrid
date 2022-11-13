using BenchmarkDotNet.Attributes;
using System.Runtime.CompilerServices;

namespace AwesomeBenchmarks.Benchmarks;

[MemoryDiagnoser]
public class MethodsBenchmarks
{

    [Benchmark(Baseline = true)]
    public void MultipleMethods()
    {
        var value = Multiple1(2);
    }

    [Benchmark]
    public void AggresiveInline()
    {
        var value = Inline1(2);
    }

    #region Code

    [MethodImpl(MethodImplOptions.NoInlining)]
    public int Multiple1(int a)
    {
        return Multiple2(a * a);
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    public int Multiple2(int a)
    {
        return Multiple3(a * a);
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    public int Multiple3(int a)
    {
        return Multiple4(a * a);
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    public int Multiple4(int a)
    {
        return a * a;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public int Inline1(int a)
    {
        return Inline2(a * a);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public int Inline2(int a)
    {
        return Inline3(a * a);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public int Inline3(int a)
    {
        return Inline4(a * a);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public int Inline4(int a)
    {
        return a * a;
    }

    #endregion
}