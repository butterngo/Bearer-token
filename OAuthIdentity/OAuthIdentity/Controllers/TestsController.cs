namespace OAuthIdentity.Controllers
{
    using Microsoft.AspNet.Identity;
    using OAuthIdentity.CustomAuthorize;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Threading.Tasks;
    using System.Web.Http;

    [ApiAuthorizeAttribute]
    [RoutePrefix("api1/test")]
    public class TestsController : ApiController
    {
        [HttpGet]
        [Route("")]
        public async Task<IHttpActionResult> Get()
        {
            return Ok(new { firstName= "aa", lastName="bb" });
        }


        [HttpPost]
        [Route("")]
        public async Task<IHttpActionResult> Post([FromBody] IDictionary<string,string> dic)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:50048/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await client.PostAsync("token1", new FormUrlEncodedContent(new Dictionary<string, string>
            {
                { "userId", User.Identity.GetUserId() },
                { "endpoint", "api1/test" }
            }));

            return Ok(new { firstName = "aa", lastName = "bb" });
        }
    }
}
