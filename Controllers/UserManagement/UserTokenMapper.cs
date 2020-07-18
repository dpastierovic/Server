using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Controllers.UserManagement
{
    public static class UserTokenMapper
    {
        private static readonly Dictionary<string, StravaToken> UserTokens = new Dictionary<string, StravaToken>();
        private static DateTimeOffset _nextCleanup = DateTimeOffset.Now.AddHours(1);

        public static async Task<StravaToken> LogInUser(HttpResponseMessage tokenRequestResponse)
        {
            var content = await tokenRequestResponse.Content.ReadAsStringAsync();
            var contentDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(content);

            if (!contentDictionary.ContainsKey("access_token"))
            {
                return null;
            }

            var token = new StravaToken(
                "todo",
                (string) contentDictionary["access_token"],
                (string) contentDictionary["refresh_token"],
                DateTimeOffset.FromUnixTimeSeconds((long) contentDictionary["expires_at"]));
            UpdateToken(token);
            return token;
        }

        private static void UpdateToken(StravaToken token)
        {
            CleanupExpiredTokens();

            if (UserTokens.ContainsKey(token.AccessToken))
            {
                UserTokens[token.AccessToken] = token;
            }
            else
            {
                UserTokens.Add(token.AccessToken, token);
            }
        }

        private static void CleanupExpiredTokens()
        {
            if (DateTimeOffset.Now <= _nextCleanup)
            {
                return;
            }

            var expiredTokenIds = UserTokens
                .Where(login => login.Value.ExpiresAt < _nextCleanup)
                .Select(login => login.Key);

            foreach (var tokenId in expiredTokenIds)
            {
                UserTokens.Remove(tokenId);
            }

            _nextCleanup = DateTimeOffset.Now.AddHours(1);
        }
    }
}