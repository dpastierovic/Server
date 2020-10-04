using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Threading.Tasks;

namespace Controllers.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class ActivityController : ControllerBase
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly IStravaRequestFactory _stravaRequestFactory;

        public ActivityController(IHttpClientFactory clientFactory,
            IStravaRequestFactory stravaRequestFactory)
        {
            _clientFactory = clientFactory;
            _stravaRequestFactory = stravaRequestFactory;
        }

        [HttpGet]
        [Route("/api/[controller]/activities")]
        public async Task<IActionResult> GetActivities([FromHeader] string token, [FromHeader] int page, [FromHeader] int perPage)
        {
            var client = _clientFactory.CreateClient();

            var activityRequest = _stravaRequestFactory.GetActivityListRequest(token, page, perPage);
            var response = await client.SendAsync(activityRequest);

            return Ok(await response.Content.ReadAsStreamAsync());
        }
    }
}