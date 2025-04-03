namespace Mediator;


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



