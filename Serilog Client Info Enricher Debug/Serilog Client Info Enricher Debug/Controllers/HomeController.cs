using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Serilog_Client_Info_Enricher_Debug.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> logger;

        public HomeController(ILogger<HomeController> logger)
        {
            this.logger = logger;
        }

        public ActionResult Index()
        {
            using (logger.BeginScope(new Dictionary<string, object>
            {
                ["test1"] = "did this work?"
            }))
            {
                logger.LogInformation("Hello world");
            }
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}