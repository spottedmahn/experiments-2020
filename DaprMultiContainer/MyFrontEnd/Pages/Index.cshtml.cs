using Dapr.Client;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MyFrontEnd.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly DaprClient daprClient;

        public IndexModel(ILogger<IndexModel> logger, DaprClient daprClient)
        {
            _logger = logger;
            this.daprClient = daprClient;
        }

        public async Task OnGet()
        {
            var forcasts = await daprClient.InvokeMethodAsync<
                IEnumerable<WeatherForecast>>(HttpMethod.Get,
                "MyBackEnd",
                "weatherforecast");
            ViewData["WeatherForecastData"] = forcasts;
        }
    }
}