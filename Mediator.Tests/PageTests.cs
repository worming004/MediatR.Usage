namespace Mediator.Tests;

public class PageTests
{
    [Fact]
    public async Task WhenSearchingAfterText_FoundCountIsSet()
    {
        var pageMediator = new PageMediator();
        var document = new Document("Hello Joe, how are you Joe?", pageMediator);
        var textArea = new TextArea(pageMediator);
        var foundCount = new FoundCount(pageMediator);

        textArea.Text = "Joe";

        // Wait for the debouncer to finish
        await Task.Delay(150);
        Assert.Equal(2, foundCount.FoundCountValue);
    }
}
