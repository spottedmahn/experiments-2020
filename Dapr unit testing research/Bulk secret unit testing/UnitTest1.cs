using Dapr.Client;
using FluentAssertions;
using Moq;

namespace Bulk_secret_unit_testing
{
    [TestClass]
    public class UnitTest1
    {
        /// <summary>
        /// potential answer for "how to mock GetBulkSecretAsync"
        /// https://stackoverflow.com/questions/68362431/how-do-you-do-unit-testing-with-dapr
        /// </summary>
        /// <returns></returns>
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
    }
}