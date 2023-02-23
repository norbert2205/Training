using System.Web.Http;
using Serilog;

namespace Training
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.AppSettings()
                .CreateLogger();

            GlobalConfiguration.Configure(WebApiConfig.Register);
            // new LoggerConfiguration().WriteTo.File("log-.txt", rollingInterval: RollingInterval.Day).CreateLogger();
        }
    }
}
