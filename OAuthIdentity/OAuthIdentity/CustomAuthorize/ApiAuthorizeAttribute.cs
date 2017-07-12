namespace OAuthIdentity.CustomAuthorize
{
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;
    using System.Web.Http.Controllers;

    public class ApiAuthorizeAttribute: AuthorizeAttribute
    {
        
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            //actionContext.Response = new HttpResponseMessage(HttpStatusCode.Unauthorized);
            base.OnAuthorization(actionContext);
        }
    }
}