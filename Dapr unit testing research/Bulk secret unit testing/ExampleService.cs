using Dapr.Client;

namespace BulkSecretUnitTesting
{
    internal class ExampleService
    {
        private DaprClient daprClient;

        public ExampleService(DaprClient daprClient)
        {
            this.daprClient = daprClient;
        }

        internal async Task<Dictionary<string, Dictionary<string, string>>> GetBulkSecrets(string storeName)
        {
            return await daprClient.GetBulkSecretAsync(storeName);
        }
    }
}