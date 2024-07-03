namespace Mediator.Tests;

public class PageTests
{
    [Fact]
    public async Task WhenSearchingAfterText_CounterDisplayIsSet()
    {
        var pageMediator = new PageMediator();
        var document = new Document("Hello Joe, how are you Joe?", pageMediator);
        var textInput = new TextInput(pageMediator);
        var foundCountDisplay = new CounterDisplay(pageMediator);

        textInput.Text = "Joe";

        // Wait for the debouncer to finish
        await Task.Delay(150);
        Assert.Equal(2, foundCountDisplay.CounterValue);
    }
}
