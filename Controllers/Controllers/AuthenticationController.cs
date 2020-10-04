using Controllers.UserManagement;
using GpsAppDB.Repositories;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
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
        private readonly IHttpClientFactory _clientFactory;
        private readonly IStravaRequestFactory _stravaRequestFactory;
        private readonly IAuthenticatedAthleteFactory _authenticatedAthleteFactory;
        private readonly AthleteRepository _athleteRepository;

        public AuthenticationController(IHttpClientFactory clientFactory,
            IStravaRequestFactory stravaRequestFactory,
            IAuthenticatedAthleteFactory authenticatedAthleteFactory,
            AthleteRepository athleteRepository)
        {
            _clientFactory = clientFactory;
            _stravaRequestFactory = stravaRequestFactory;
            _authenticatedAthleteFactory = authenticatedAthleteFactory;
            _athleteRepository = athleteRepository;
        }

        [HttpPut]
        [Route("/api/[controller]/code")]
        [Consumes("text/plain")]
        public async Task<IActionResult> PutCode()
        {
            using var reader = new StreamReader(Request.Body, Encoding.UTF8);
            var code = await reader.ReadToEndAsync();

            if (string.IsNullOrEmpty(code))
            {
                return BadRequest("Code is null or empty string.");
            }

            var tokenRequest = _stravaRequestFactory.GetTokenRequest(code);
            var client = _clientFactory.CreateClient();
            var response = await client.SendAsync(tokenRequest);

            if (!response.IsSuccessStatusCode) return StatusCode((int) response.StatusCode);

            var athlete = await _authenticatedAthleteFactory.Create(response);

            if (athlete == null) return BadRequest("Strava did not return token");

            if (!_athleteRepository.IsStored(athlete.AthleteId))
            {
                _athleteRepository.Add(athlete.AthleteId, athlete.FirstName, athlete.LastName);
            }

            return Ok(JsonConvert.SerializeObject(athlete));
        }

        [HttpGet]
        [Route("/api/[controller]/stats/{id}")]
        public async Task<IActionResult> GetStats([FromHeader] string token, string id)
        {
            var client = _clientFactory.CreateClient();
            var request = _stravaRequestFactory.GetAthleteStats(token, id);
            var response = await client.SendAsync(request);
            
            return StatusCode((int) response.StatusCode, await response.Content.ReadAsStringAsync());
        }
    }
}