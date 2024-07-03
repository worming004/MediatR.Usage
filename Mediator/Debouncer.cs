namespace Mediator;

// Naive implementation of a debouncer
public class Debouncer
{
    private int counter;
    private int intervalMs;

    public Debouncer(int intervalMs)
    {
        this.intervalMs = intervalMs;
    }

    public void Debounce(Action act)
    {
        lock (this)
        {
            counter++;
        }
        var thisCount = counter;

        new Timer((_) =>
        {
            var doAction = false;
            lock (this)
            {
                if (counter == thisCount)
                {
                    doAction = true;
                }
            }
            if (doAction)
            {
                act();
            }
        }, null, intervalMs, Timeout.Infinite);
    }
}
