using PipeWire.Spa;
using Xunit;

namespace PipeWire.Tests;

public unsafe class PipeWireLoopTests
{
    public PipeWireLoopTests() => PipeWireLibrary.Init();

    [Fact]
    public void ACreatedLoopOwnsItsHandle()
    {
        using PipeWireLoop loop = PipeWireLoop.Create();

        Assert.True(loop.IsOwned);
        Assert.True(loop.Handle is not null);
        Assert.True(loop.Fd >= 0);
    }

    [Fact]
    public void ACreatedLoopTakesProperties()
    {
        using PipeWireLoop loop = PipeWireLoop.Create(new SpaDictionaryEntry("loop.name", "embedded"));

        Assert.True(loop.IsOwned);
        Assert.True(loop.Handle is not null);
    }

    [Fact]
    public void ACreatedLoopCanBeDrivenByHand()
    {
        using PipeWireLoop loop = PipeWireLoop.Create();

        loop.SetName("embedded-tests");
        loop.Enter();

        try
        {
            Assert.True(loop.IsCallerOnLoop);
            Assert.True(loop.Iterate(0) >= 0);
        }
        finally
        {
            loop.Leave();
        }
    }

    [Fact]
    public void DisposingACreatedLoopTwiceIsSafe()
    {
        PipeWireLoop loop = PipeWireLoop.Create();

        loop.Dispose();
        loop.Dispose();

        Assert.True(loop.Handle is null);
    }

    [Fact]
    public void ALoopBorrowedFromAMainLoopIsNotDisposed()
    {
        using var main = new PipeWireMainLoop();
        PipeWireLoop loop = main.Loop;

        Assert.False(loop.IsOwned);

        loop.Dispose();

        Assert.True(loop.Handle is not null);
        Assert.True(loop.Fd >= 0);
    }
}
