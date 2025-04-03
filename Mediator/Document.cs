namespace Mediator;

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
        mediator.Notify(new OccurenceFoundCount(count));
    }
}
