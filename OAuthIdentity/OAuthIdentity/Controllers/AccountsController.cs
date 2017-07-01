namespace OAuthIdentity.Controllers
{
    using Microsoft.AspNet.Identity.Owin;
    using OAuthIdentity.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Web;
    using System.Web.Http;
    using Microsoft.AspNet.Identity;

    [RoutePrefix("api/accounts")]
    public class AccountsController : ApiBase
    {
        private readonly OAuthIdentityUserManager _userManager;

        public AccountsController ()
        {
            _userManager = OwinContext.GetUserManager<OAuthIdentityUserManager>();
        }

        [HttpGet]
        [Route("")]
        public async Task<IHttpActionResult> GetAll()
        {
            return Ok(await _userManager.FindByIdAsync(User.Identity.GetUserId()));
        }

        [HttpPost]
        [Route("regiter")]
        [AllowAnonymous]
        public async Task<IHttpActionResult> Regiter([FromBody] IDictionary<string,string> dic)
        {
            return Ok(await _userManager.CreateAsync(new Models.User
            {
                UserName = dic["email"],
                Email = dic["email"],
            }, dic["password"]));
        }
    }    
}
