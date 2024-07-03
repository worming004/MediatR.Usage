namespace Mediator.Tests;

public class DebouncerTests
{
    const int interval50Ms = 50;
    int count = 0;
    Debouncer debouncer = new Debouncer(interval50Ms);

    [Fact]
    public async Task TwoCallInInterval_ActOnce()
    {
        debouncer.Debounce(() => count++);
        // Have to be < than interval50Ms
        await Task.Delay(20);
        debouncer.Debounce(() => count++);

        // Have to be > than interval50Ms
        await Task.Delay(100);
        Assert.Equal(1, count);
    }


    [Fact]
    public async Task TwoCallOutInterval_ActTwice()
    {
        debouncer.Debounce(() => count++);
        // Have to be > than interval50Ms
        await Task.Delay(100);
        debouncer.Debounce(() => count++);

        // Have to be > than interval50Ms
        await Task.Delay(100);
        Assert.Equal(2, count);
    }
}
