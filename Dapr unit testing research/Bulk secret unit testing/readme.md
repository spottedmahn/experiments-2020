https://stackoverflow.com/questions/68362431
&
https://github.com/dapr/dotnet-sdk/issues/774#issuecomment-1266216355

>How do you do unit testing with Dapr?  
in this particular instance, I'm using the 
DaprClient.GetBulkSecretAsync method but I'd like
a solution that I can use elsewhere, if possible.

```csharp
[TestMethod("How to mock GetBulkSecretAsync - 68362431")]
public async Task TestMethod1()
{
	//arrange
	var daprClient = new Mock<DaprClient>();
	var exampleService = new ExampleService(daprClient.Object);

	daprClient.Setup(d => d.GetBulkSecretAsync("my-store",
		It.IsAny<IReadOnlyDictionary<string, string>>(),
		It.IsAny<CancellationToken>()))
		.ReturnsAsync(new Dictionary<string, Dictionary<string, string>>
		{
			{
				"example",
				new Dictionary<string, string>
				{
					{ "i don't understand the builk API (yet)", "some value" }
				}
			}
		});

	//act
	var actual = await exampleService.GetBulkSecrets("my-store");

	//assert
	actual.Should().BeEquivalentTo(new Dictionary<string, Dictionary<string, string>>
	{
		{
			"example",
			new Dictionary<string, string>
			{
				{ "i don't understand the builk API (yet)", "some value" }
			}
		}
	});
}
```
