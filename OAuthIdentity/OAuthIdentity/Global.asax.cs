namespace OAuthIdentity
{
    using System.Web;
    using System.Web.Http;

    public class Global : HttpApplication
    {

        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}