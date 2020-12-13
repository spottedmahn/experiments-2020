using System.Web;
using System.Web.Mvc;

namespace Serilog_Client_Info_Enricher_Debug
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
