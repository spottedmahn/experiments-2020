using Dapr.Client;

namespace DaprUnitTestPrj1
{
    internal class ExampleService
    {
        private DaprClient daprClient;

        public ExampleService(DaprClient daprClient)
        {
            this.daprClient = daprClient;
        }

        internal async Task<bool> IsTheCustomerNameMikeD()
        {
            var customer = await daprClient.InvokeMethodAsync<Customer>(HttpMethod.Get,
                "my-cool-app", "customer");
            return customer?.Name == "Mike D.";
        }
    }
}