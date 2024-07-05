namespace Mediator;

public interface IMediator
{
    void Notify(object input);
    void Register(object compoment);
}

// PageMediator act like a container component in react
public class PageMediator : IMediator
{
    private Document? document;
    private TextInput? textArea;
    private CounterDisplay? foundCount;
    private Debouncer debouncerForTextArea = new Debouncer(100);
    public void Notify(object input)
    {
        GuardReady();
        // in MediatR, each command or event match with a handler. This is replicated here with if conditions.
        if (input is Document.OccurenceFoundCount search)
        {
            foundCount.CounterValue = search.count;
        }
        else if (input is TextInput.TextInputChanged text)
        {
            debouncerForTextArea.Debounce(() => document.FindOccurence(text.text));
        }
    }

    // Mediator have to know which component to interact with
    public void Register(object compoment)
    {
        var typeOfComponent = compoment.GetType();
        if (typeOfComponent == typeof(Document))
        {
            document = (Document)compoment;
        }
        else if (typeOfComponent == typeof(TextInput))
        {
            textArea = (TextInput)compoment;
        }
        else if (typeOfComponent == typeof(CounterDisplay))
        {
            foundCount = (CounterDisplay)compoment;
        }
        else
        {
            throw new ArgumentException($"Unknown component type {typeOfComponent}");
        }
    }

    private void GuardReady()
    {
        if (document is null || textArea is null || foundCount is null)
        {
            throw new InvalidOperationException("Mediator not ready");
        }
    }

}

// Document becomes usable in tons of cases as there is no understanding of context.
public class Document
{
    public record OccurenceFoundCount(int count);
    private string text;
    private IMediator mediator;

    public Document(string texte, IMediator mediator)
    {
        this.text = texte;
        this.mediator = mediator;
        mediator.Register(this);
    }

    public void FindOccurence(string search)
    {
        var count = text.Split(search).Length - 1;
        mediator.Notify(new OccurenceFoundCount(count = count));
    }
}

// TextArea becomes usable in tons of cases as there is no understanding of context.
public class TextInput
{
    public record TextInputChanged(string text);
    private string text;
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

// CounterDisplay becomes usable in tons of cases as there is no understanding of context.
public class CounterDisplay
{
    public int CounterValue { get; set; }
    public CounterDisplay(IMediator mediator)
    {
        mediator.Register(this);
    }
}
