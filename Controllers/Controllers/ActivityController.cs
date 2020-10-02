using GpsAppDB.Entities;
using GpsAppDB.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
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
        private readonly ActivityRepository _activityRepository;

        public ActivityController(IConfiguration configuration, IHttpClientFactory clientFactory,
            IStravaRequestFactory stravaRequestFactory, ActivityRepository activityRepository)
        {
            _configuration = configuration;
            _clientFactory = clientFactory;
            _stravaRequestFactory = stravaRequestFactory;
            _activityRepository = activityRepository;
        }

        [HttpGet]
        [Route("/api/[controller]/activities")]
        public async Task<IActionResult> GetActivities([FromHeader] string token)
        {
            var client = _clientFactory.CreateClient();

            var page = 0;
            IEnumerable<Dictionary<string, object>> contentDictionary;
            do
            {
                page++;
                var activityRequest = _stravaRequestFactory.GetActivityListRequest(token, page);
                var response = await client.SendAsync(activityRequest);
                var content = await response.Content.ReadAsStringAsync();
                contentDictionary = JsonConvert.DeserializeObject<IEnumerable<Dictionary<string, object>>>(content);

            } while (!ProcessPage(contentDictionary));

            return Ok();
        }

        private bool ProcessPage(IEnumerable<IDictionary<string, object>> content)
        {
            var allPresent = true;

            foreach (var activity in content)
            {
                var activityEntity = CreateActivity(activity);

                if (activityEntity == null || _activityRepository.IsPresent(activityEntity.ActivityId))
                {
                    continue;
                }

                allPresent = false;

                _activityRepository.Add(activityEntity.AthleteId, activityEntity.ActivityId, activityEntity.Name,
                    activityEntity.Polyline);
            }

            return allPresent;
        }

        private static Activity CreateActivity(IDictionary<string, object> content)
        {
            if (!content.ContainsKey("name") ||
                !content.ContainsKey("id") ||
                !content.ContainsKey("athlete") ||
                !content.ContainsKey("map"))
            {
                return null;
            }

            return new Activity
            {
                Name = content["name"].ToString(),
                ActivityId = content["id"].ToString(),
                AthleteId = ((JObject) content["athlete"])["id"].ToString(),
                Polyline = ((JObject) content["map"])["summary_polyline"].ToString()
            };
        }
    }
}