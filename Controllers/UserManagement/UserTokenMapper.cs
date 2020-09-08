using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Controllers.UserManagement
{
    public static class UserTokenMapper
    {
        public static async Task<AuthenticatedAthlete> LogInUser(HttpResponseMessage tokenRequestResponse)
        {
            var content = await tokenRequestResponse.Content.ReadAsStringAsync();
            var contentDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(content);

            if (!contentDictionary.ContainsKey("access_token") ||
                !contentDictionary.ContainsKey("refresh_token") ||
                !contentDictionary.ContainsKey("expires_at") ||
                !contentDictionary.ContainsKey("athlete"))
            {
                return null;
            }

            var athlete = (JObject)contentDictionary["athlete"];

            var token = new AuthenticatedAthlete((string) athlete["id"],
                (string) contentDictionary["access_token"],
                (string) contentDictionary["refresh_token"],
                DateTimeOffset.FromUnixTimeSeconds((long) contentDictionary["expires_at"]),
                (string) athlete["firstname"],
                (string) athlete["lastname"]);

            return token;
        }
    }
}