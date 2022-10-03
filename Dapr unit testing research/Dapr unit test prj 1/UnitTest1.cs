using Dapr.Client;
using FluentAssertions;
using Moq;

namespace DaprUnitTestPrj1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod("mock dapr service invocation my style")]
        public async Task TestMethod1()
        {
            //arrange
            var daprClient = new Mock<DaprClient>();
            var exampleService = new ExampleService(daprClient.Object);

            var serviceInvocationRequest = new HttpRequestMessage();

            //normal setup: when called, return
            //this mocked resopnse;
            //BUT we have to make sure it's
            //the same request we're mocking
            //next in the CreateInvokeMethodRequest
            //setup.
            daprClient.Setup(d => d.InvokeMethodAsync<Customer>(serviceInvocationRequest
                , It.IsAny<CancellationToken>()))
                .ReturnsAsync(new Customer
                {
                    Name = "Mike D."
                });

            //b/c the client follows a pattern like
            //this: calling the abstract CreateInvokeMethodRequest
            //then calling the abstract InvokeMethodAsync
            //when call the more specific
            //flavors of InvokeMethodAsync,
            //we can use the knowledge of the internal
            //working of the daprClient (which
            //seems like code smell to me)
            //to indirectly verify the args
            //we used to call invoke the
            //the other service.
            //public Task InvokeMethodAsync(
            //    string appId,
            //    string methodName,
            //    CancellationToken cancellationToken = default)
            //{
            //    var request = CreateInvokeMethodRequest(appId, methodName);
            //    return InvokeMethodAsync(request, cancellationToken);
            //}
            daprClient.Setup(d => d.CreateInvokeMethodRequest(
                HttpMethod.Get, "my-cool-app", "customer"))
                .Returns(serviceInvocationRequest);

            var actual = await exampleService.IsTheCustomerNameMikeD();

            //assert
            //can't do it the idiomatic way
            //unfortunately.  the assert
            //ends up getting mixed in
            //with the arrange 😢
            //daprClient.Verify()
            actual.Should().BeTrue();
        }

        [TestMethod("w/o mocking CreateInvokeMethodRequest \n" +
            "the InvokeMethodAsync mock works \n" +
            "BUT we can't verify the service invocation \n" +
            "parameters like app-id, method, etc.")]
        public async Task TestMethod2()
        {
            //arrange
            var daprClient = new Mock<DaprClient>();
            var exampleService = new ExampleService(daprClient.Object);

            //normal setup
            //when called, return
            //this resopnse
            daprClient.Setup(d => d.InvokeMethodAsync<Customer>(It.IsAny<HttpRequestMessage>()
                , It.IsAny<CancellationToken>()))
                .ReturnsAsync(new Customer
                {
                    Name = "No service invocation arg verification"
                });

            //act
            var actual = await exampleService.IsTheCustomerNameMikeD();

            //assert
            actual.Should().BeFalse();
        }

        [TestMethod("the way it should be")]
        public async Task TestMethod3()
        {
            //runtime exception:
            //Test method DaprUnitTestPrj1.UnitTest1.TestMethod3
            //threw exception:
            //System.NotSupportedException: Unsupported
            //expression: d => d.InvokeMethodAsync<Customer>(HttpMethod.Get
            //, "my-cool-app", "customer", It.IsAny<CancellationToken>())
            //Non - overridable members(here: DaprClient.InvokeMethodAsync)
            //may not be used in setup / verification expressions.

            //arrange
            var daprClient = new Mock<DaprClient>();
            var exampleService = new ExampleService(daprClient.Object);

            daprClient.Setup(d => d.InvokeMethodAsync<Customer>(HttpMethod.Get
                , "my-cool-app", "customer", It.IsAny<CancellationToken>()))
                .ReturnsAsync(new Customer
                {
                    Name = "Mike D."
                });

            //act
            var actual = await exampleService.IsTheCustomerNameMikeD();

            //assert
            actual.Should().BeTrue();
        }
    }
}