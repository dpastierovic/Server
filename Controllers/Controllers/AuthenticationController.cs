using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Controllers.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpClientFactory _clientFactory;

        public AuthenticationController(IConfiguration configuration, IHttpClientFactory clientFactory)
        {
            _configuration = configuration;
            _clientFactory = clientFactory;
        }

        [HttpPut]
        [Consumes("text/plain")]
        [Route("/api/[controller]/code")]
        public async Task<IActionResult> PutToken()
        {
            using var reader = new StreamReader(Request.Body, Encoding.UTF8);
            var code = await reader.ReadToEndAsync();

            var client = _clientFactory.CreateClient();

            var authRequest = new HttpRequestMessage(HttpMethod.Post, _configuration["StravaOauthTokenUrl"])
            {
                Content = new FormUrlEncodedContent(new Dictionary<string, string>
                {
                    {"client_id", _configuration["ClientId"]},
                    {"client_secret", _configuration["ClientSecret"]},
                    {"code", code},
                    {"grant_type", "authorization_code"}
                })
            };
            var response = JsonConvert.DeserializeObject<Dictionary<string, object>>(await (await client.SendAsync(authRequest)).Content
                .ReadAsStringAsync());

            return Ok($"{{\"access_token\": \"{response["access_token"]}\"}}");
        }
    }
}
