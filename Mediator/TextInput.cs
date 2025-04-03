namespace Mediator;

// TextArea becomes usable in tons of cases as there is no understanding of context.
public class TextInput
{
    public record TextInputChanged(string text);
    private string text = string.Empty;
    private IMediator mediator;

    public TextInput(IMediator mediator)
    {
        this.mediator = mediator;
        mediator.Register(this);
    }

    public string Text
    {
        get => text; set
        {
            text = value;
            mediator.Notify(new TextInputChanged(text));
        }
    }
}
