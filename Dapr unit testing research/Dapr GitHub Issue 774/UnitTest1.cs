using Dapr.Client;
using FluentAssertions;
using Moq;

namespace DaprGitHubIssue774
{
    [TestClass]
    public class UnitTest1
    {
        /// <summary>
        /// I took the respone from @halspang
        /// source: https://github.com/dapr/dotnet-sdk/issues/774#issuecomment-966721169
        /// and made the minimum amount of changes
        /// to get the test passing.
        /// To mock and verify a service invocation,
        /// one has to mock the lower level calls
        /// in the DaprClient.  Specifically, the abstract
        /// versions of InvokeMethodAsync
        /// AND CreateInvokeMethodRequest.
        /// </summary>
        /// <returns></returns>
        [TestMethod("refactored sample from GitHub issue 774 by @halspang")]
        //https://github.com/dapr/dotnet-sdk/issues/774#issuecomment-966721169
        public async Task Test()
        {
            //arrange
            var daprClient = new Mock<DaprClient>();
            //I don't we need this
            //+ if it stays, it can be a
            //single object not a list of objects.
            //additional comments below in the assertion
            //section.
            var requestCaptureHelper = new List<HttpRequestMessage>();
            var requestToOtherApp = new HttpRequestMessage
            {
                //this isn't json, why did halspang
                //use JsonContent?  the assertion
                //the bottom is wrong as apparently
                //it serializes to ""Hello 1""
                //Content = JsonContent.Create("Hello 1")
                //changed to used StringContent
                Content = new StringContent("Hello 1")
            };

            //mock service invocation
            daprClient.Setup(m => m.InvokeMethodAsync<string>(It.IsAny<HttpRequestMessage>(), It.IsAny<CancellationToken>()))
                .Callback<HttpRequestMessage, CancellationToken>(
                    (r, _) => requestCaptureHelper.Add(r))
                .ReturnsAsync("mocked response from service invocation");

            //this step is NOT optionally.
            //you have to mock the call to
            //CreateInvokeMethodRequest.

            //you can 'verify' my using specific
            //values instead of It.IsAny<T>().

            //I don't like verifying this way b/c
            //it is indirect and you lose some powers
            //that come w/ doing it 'properly' through
            //Verfiy()

            //verify args of the service invocation
            //for example verify I called this service/appId
            //this endpoint w/ the data, etc.

            //you can't actually set them to variables
            //interesting, why not?
            //var httpMethod = It.IsAny<HttpMethod>();
            //var appId = It.IsAny<string>();
            //var methodName = It.IsAny<string>();

            //don't care about the specific values
            //used when calling CreateInvokeMethodRequest
            //daprClient.Setup(m => m.CreateInvokeMethodRequest(It.IsAny<HttpMethod>(),
            //    It.IsAny<string>(), It.IsAny<string>()))

            //other way approach, 'verify' args
            //like appId, http method, etc.
            //verify I called GET /my-endpoint on my-cool-app
            daprClient.Setup(m => m.CreateInvokeMethodRequest(HttpMethod.Get,
                "my-cool-app", "my-endpoint"))
                //some temp debug code I was using
                //for setting a breakpoint so I could
                //inspect objects in memory.
                //this is how I learned you
                //MUST mock CreateInvokeMethodRequest
                //as my Setup call wasn't right
                //and I could never it the assert
                //on the invocation result to pass
                //.Callback(() =>
                //{
                //    var mike = "1";
                //})
                .Returns(requestToOtherApp);

            var service = new TestService(daprClient.Object);

            //act
            //TestClientAsync isn't using the input
            //parameter and should be removed
            //for a minimal test example.
            var actual = await service.TestClientAsync("Hello 2");

            //assert
            actual.Should().Be("mocked response from service invocation");

            //get mocked request body
            //as an extra check.  is that really needed?
            //doesn't feel like it.  it doesn't hurt
            //but don't think it's neccessary.

            //more thoughts...
            //we're telling CreateInvokeMethodRequest
            //to use our request then verifying
            //the internals of the DaprClient
            //to verify it is using it.  Seems out
            //of scope for our testing

            //more thoughts
            //why use a list when there's only one?
            requestCaptureHelper.Should().HaveCount(1);
            var requestBody = await requestCaptureHelper![0].Content!.ReadAsStringAsync();
            requestBody.Should().Be("Hello 1");
        }
    }
}