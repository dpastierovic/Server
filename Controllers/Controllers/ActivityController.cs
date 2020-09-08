using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using System.Threading.Tasks;

namespace Controllers.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class ActivityController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpClientFactory _clientFactory;
        private readonly IStravaRequestFactory _stravaRequestFactory;

        public ActivityController(IConfiguration configuration, IHttpClientFactory clientFactory, IStravaRequestFactory stravaRequestFactory)
        {
            _configuration = configuration;
            _clientFactory = clientFactory;
            _stravaRequestFactory = stravaRequestFactory;
        }

        [HttpGet]
        [Route("/api/[controller]/activities")]
        public async Task<IActionResult> GetActivities([FromHeader] string token)
        {
            var accessToken = Request.Headers["access_token"];

            var client = _clientFactory.CreateClient();

            var activityRequest = _stravaRequestFactory.GetActivityListRequest(accessToken);

            var response = await client.SendAsync(activityRequest);

            return Ok(await response.Content.ReadAsStringAsync());
        }
    }
}