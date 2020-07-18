﻿using Controllers.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Controllers.UserManagement;

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

            var client = _clientFactory.CreateClient();

            var tokenRequest = new HttpRequestMessage(HttpMethod.Post, _configuration[ConfigurationKeys.StravaTokenUrl])
            {
                Content = new FormUrlEncodedContent(new Dictionary<string, string>
                {
                    {"client_id", _configuration[ConfigurationKeys.ClientId]},
                    {"client_secret", _configuration[ConfigurationKeys.ClientSecret]},
                    {"code", code},
                    {"grant_type", "authorization_code"}
                })
            };
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

            return Ok($"{{\"access_token\": \"{token.AccessToken}\"}}");
        }
    }
}