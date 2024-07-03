namespace Mediator.Tests;

public class DebouncerTests
{
    int count = 0;
    Debouncer debouncer = new Debouncer(50);

    [Fact]
    public async Task TwoCallInInterval_ActOnce()
    {
        debouncer.Debounce(() => count++);
        debouncer.Debounce(() => count++);

        await Task.Delay(100);
        Assert.Equal(1, count);
    }


    [Fact]
    public async Task TwoCallOutInterval_ActTwice()
    {
        debouncer.Debounce(() => count++);
        await Task.Delay(100);
        debouncer.Debounce(() => count++);

        await Task.Delay(100);
        Assert.Equal(2, count);
    }
}
