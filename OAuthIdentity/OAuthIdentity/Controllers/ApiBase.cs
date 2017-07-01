namespace OAuthIdentity.Controllers
{
    using Microsoft.Owin;
    using System.Web;
    using System.Web.Http;

    [Authorize]
    public class ApiBase : ApiController
    {
        public IOwinContext OwinContext
        {
            get
            {
                return HttpContext.Current.GetOwinContext();
            }
        }
    }
}
