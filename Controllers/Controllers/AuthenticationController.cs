using Controllers.UserManagement;
using GpsAppDB.Repositories;
using Microsoft.AspNetCore.Mvc;
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
        private readonly AthleteRepository _athleteRepository;

        public AuthenticationController(IHttpClientFactory clientFactory,
            IStravaRequestFactory stravaRequestFactory,
            AthleteRepository athleteRepository)
        {
            _clientFactory = clientFactory;
            _stravaRequestFactory = stravaRequestFactory;
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

            if (!response.IsSuccessStatusCode)
            {
                return StatusCode((int) response.StatusCode);
            }

            var token = await UserTokenMapper.LogInUser(response);

            if (token == null)
            {
                return BadRequest("Strava did not return token");
            }

            if (!_athleteRepository.IsStored(token.AthleteId))
            {
                _athleteRepository.Add(token.AthleteId, token.FirstName, token.LastName);
            }

            return Ok($"{{\"access_token\": \"{token.AccessToken}\"}}");
        }
    }
}