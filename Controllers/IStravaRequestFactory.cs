using System.Net.Http;

namespace Controllers
{
    public interface IStravaRequestFactory
    {
        /// <summary>
        /// Returns request for user and refresh token
        /// </summary>
        HttpRequestMessage GetTokenRequest(string code);
        
        /// <summary>
        /// Returns list of recent activities done by the authenticated user
        /// </summary>
        HttpRequestMessage GetActivityListRequest(string accessToken, int page, int perPage);

        HttpRequestMessage GetAthleteStats(string accessToken, string id);
    }
}