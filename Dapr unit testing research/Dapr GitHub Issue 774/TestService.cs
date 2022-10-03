using Dapr.Client;

namespace DaprGitHubIssue774
{
    internal class TestService
    {
        private readonly DaprClient daprClient;

        public TestService(DaprClient daprClient)
        {
            this.daprClient = daprClient;
        }

        internal async Task<string> TestClientAsync(string v)
        {
            return await daprClient.InvokeMethodAsync<string>(HttpMethod.Get, "my-cool-app", "my-endpoint");
        }
    }
}