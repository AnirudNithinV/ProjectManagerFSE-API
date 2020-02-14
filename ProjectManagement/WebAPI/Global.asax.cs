using System.Web.Http;

namespace WebAPI
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        /// <summary>
        /// Application start event
        /// </summary>
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}
