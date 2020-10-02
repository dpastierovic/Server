using Controllers.Utilities;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Net.Http;

namespace Controllers
{
    public class StravaRequestFactory : IStravaRequestFactory
    {
        // refresh token might be required
        private readonly IConfiguration _configuration;
        private const string PageSize = "50";

        public StravaRequestFactory(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public HttpRequestMessage GetTokenRequest(string code)
        {
            return new HttpRequestMessage(HttpMethod.Post, _configuration[ConfigurationKeys.StravaTokenUrl])
            {
                Content = new FormUrlEncodedContent(new Dictionary<string, string>
                {
                    {"client_id", _configuration[ConfigurationKeys.ClientId]},
                    {"client_secret", _configuration[ConfigurationKeys.ClientSecret]},
                    {"code", code},
                    {"grant_type", "authorization_code"}
                })
            };
        }

        public HttpRequestMessage GetActivityListRequest(string accessToken, int page)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"{_configuration[ConfigurationKeys.StravaApiUrl]}/athlete/activities")
            {
                Content = new FormUrlEncodedContent(new Dictionary<string, string>
                {
                    { "page", page.ToString() },
                    { "per_page", PageSize }
                })
            };
            request.Headers.Add("authorization", $"Bearer {accessToken}");
            return request;
        }
    }
}