namespace Mediator;

// CounterDisplay becomes usable in tons of cases as there is no understanding of context.
public class CounterDisplay
{
    public int CounterValue { get; set; }
    public CounterDisplay(IMediator mediator)
    {
        mediator.Register(this);
    }
}
