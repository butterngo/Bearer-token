namespace OAuthIdentity.Controllers
{
    using Microsoft.AspNet.Identity.Owin;
    using OAuthIdentity.Models;
    using OAuthIdentity.Repository;
    using System;
    using System.Text;
    using System.Threading.Tasks;
    using System.Web.Http;

    [RoutePrefix("api/clients")]
    public class ClientsController : ApiBase
    {
        private readonly IClientRepository _clientRepository;

        private readonly IClientUserRepository _clientUserRepository;

        public ClientsController()
        {
            _clientRepository = OwinContext.Get<IClientRepository>();
            _clientUserRepository = OwinContext.Get<IClientUserRepository>();
        }

        [HttpPost]
        [Route("")]
        [AllowAnonymous]
        public async Task<IHttpActionResult> Post([FromBody] Client dto)
        {
            return Ok(await _clientRepository.AddAsync(dto));
        }

        [HttpPost]
        [Route("clientuser")]
        [AllowAnonymous]
        public async Task<IHttpActionResult> CreateClientUser([FromBody] ClientUser dto)
        {
            return Ok(await _clientUserRepository.AddAsync(dto));
        }

        [HttpGet]
        [Route("generate/{clientId}")]
        [AllowAnonymous]
        public async Task<IHttpActionResult> GenerateBasicToken(string clientId)
        {
            var client = await _clientRepository.FindByAsync(clientId);

            string basicToken = Convert.ToBase64String(Encoding.ASCII.GetBytes(client.Id + ":" + client.Secret));

            return Ok(basicToken);
        }
    }
}
