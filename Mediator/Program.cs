using Mediator;

var pageMediator = new PageMediator();
var document = new Document("Hello Joe, how are you Joe?", pageMediator);
var textInput = new TextInput(pageMediator);
var foundCount = new CounterDisplay(pageMediator);

// This trigger a real search in document after 100ms. Orchestrated by PageMediator.
textInput.Text = "Joe";
// Wait for the debouncer to finish
await Task.Delay(150);
System.Console.WriteLine(foundCount.CounterValue);

