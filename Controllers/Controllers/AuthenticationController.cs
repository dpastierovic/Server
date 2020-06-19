using Microsoft.AspNetCore.Mvc;
using Controllers.Utilities;
using Microsoft.Extensions.Configuration;

namespace Controllers.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public AuthenticationController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public string Get()
        {
            var urlBuilder = new UrlBuilder(_configuration["StravaApiUrl"]);
            urlBuilder.AddQueryParameter("response_type", "code");
            urlBuilder.AddQueryParameter("client_id", _configuration["ClientId"]);
            urlBuilder.AddQueryParameter("redirect_uri", _configuration["ServerUrl"]);
            urlBuilder.AddQueryParameter("scope", "activity:write,read");
            urlBuilder.AddQueryParameter("approval_prompt", "auto");
            return urlBuilder.Build();
        }
    }
}
