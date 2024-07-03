namespace Mediator;

public interface IMediator
{
    void Notify(object input);
    void Register(object compoment);
}

public class PageMediator : IMediator
{
    private Document? document;
    private TextArea? textArea;
    private FoundCount? foundCount;
    private Debouncer debouncerForTextArea = new Debouncer(100);
    public void Notify(object input)
    {
        GuardReady();
        if (input is Document.OccurenceFoundCount search)
        {
            foundCount.FoundCountValue = search.count;
        }
        else if (input is TextArea.TextAreaChanged text)
        {
            debouncerForTextArea.Debounce(() => document.FindOccurence(text.text));
        }
    }

    public void Register(object compoment)
    {
        var typeOfComponent = compoment.GetType();
        if (typeOfComponent == typeof(Document))
        {
            document = (Document)compoment;
        }
        else if (typeOfComponent == typeof(TextArea))
        {
            textArea = (TextArea)compoment;
        }
        else if (typeOfComponent == typeof(FoundCount))
        {
            foundCount = (FoundCount)compoment;
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

public class TextArea
{
    public record TextAreaChanged(string text);
    private string text;
    private IMediator mediator;

    public TextArea(IMediator mediator)
    {
        this.mediator = mediator;
        mediator.Register(this);
    }

    public string Text
    {
        get => text; set
        {
            text = value;
            mediator.Notify(new TextAreaChanged(text));
        }
    }
}

public class FoundCount
{
    public int FoundCountValue { get; set; }
    public FoundCount(IMediator mediator)
    {
        mediator.Register(this);
    }
}
